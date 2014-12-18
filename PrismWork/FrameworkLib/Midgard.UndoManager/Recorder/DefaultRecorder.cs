using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Work.UndoManager.Recorder
{
    /// <summary>
    /// 默认记录器,
    /// 自动扫描当前对象的属性,
    /// </summary>
    public class DefaultRecorder : BaseRecorder
    {
        #region 字段
        /// <summary>
        /// 代表一个空值,静态常量字段
        /// </summary>
        internal readonly static object[] EmptyArray = new object[0];
        /// <summary>
        /// 存储的旧的值,同时作为判断当前对象有效属性的集合.
        /// 所有需要监视的属性,都在当前集合中
        /// </summary>
        private readonly Dictionary<string, object> oldValueList = new Dictionary<string, object>();
        /// <summary>
        /// 临时任务堆栈,在执行组合任务时使用,用于确保集合操作的顺序性
        /// </summary>
        private List<UndoTask> tempTaskList;
        /// <summary>
        /// 单属性值更改的缓存任务列表,在进行组合任务时,缓存使用
        /// </summary>
        private Dictionary<string, PropertyUndoTask> tempPropertyTaskDictionary;
        #endregion

        #region 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="objectItem">指定要监视的对象</param>
        /// <param name="taskGroupName">是否需要压入到单独的栈中</param>
        public DefaultRecorder(INotifyStateChanged objectItem, string taskGroupName = null)
            : base(objectItem, taskGroupName)
        {
            Init();
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            AnalyzeObject();
        }
        /// <summary>
        /// 解析当前对象
        /// </summary>
        private void AnalyzeObject()
        {
            bool valueAdded = false;
            var properties = objectItem.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.GetCustomAttributes(typeof(UndoPropertyAttribute), true).Length <= 0)
                    continue;
                if (prop.CanRead && prop.CanWrite)
                {
                    var oldValue = prop.GetValue(objectItem, EmptyArray);
                    oldValueList[prop.Name] = oldValue;
                    if (oldValue is INotifyCollectionChanged)
                        ((INotifyCollectionChanged)oldValue).CollectionChanged += ObjectItem_CollectionChanged;
                    valueAdded = true;
                }
            }
            if (valueAdded)
            {
                objectItem.PropertyChanged += ObjectItem_PropertyChanged;
                objectItem.StateChanged += ObjectItem_StateChanged;
            }
        }
        /// <summary>
        /// 判断当前属性更改,是否可以忽略
        /// </summary>
        /// <returns></returns>
        private bool CanIgnore()
        {
            return IsUndoing || IsAutoRecord == false;
        }

        void ObjectItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                //如果关闭自动记录,则不再同步刷新存储的旧属性值
                if (IsAutoRecord == false || oldValueList.ContainsKey(e.PropertyName) == false)
                    return;
                PropertyChangedHandle(sender, e);
            }
            catch (System.Exception ex)
            {
                Debug.Assert(false, "ObjectItem_PropertyChanged exception " + ex.Message);
            }
        }

        void ObjectItem_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                if (CanIgnore())
                    return;
                CollectionChangedHandle(sender, e);
            }
            catch (System.Exception ex)
            {
                Debug.Assert(false, "CollectionChangedHandle exception " + ex.Message);
            }
        }

        void ObjectItem_StateChanged(object sender, StateChangedEventArgs e)
        {
            try
            {
                if (CanIgnore() || oldValueList.ContainsKey(e.PropertyName) == false)
                    return;
                StateChangedHandle(sender, e);
            }
            catch (System.Exception ex)
            {
                Debug.Assert(false, "PropertyChangedHandle exception " + ex.Message);
            }
        }
        /// <summary>
        /// 在属性更改时,刷新存储的旧属性值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyChangedHandle(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            object newValue = null;
            PropertyInfo prop = null;
            if (IsCollectionProperty(sender, e.PropertyName, out prop, out newValue))
            {
                UpdateCollectionChangedRegister(e.PropertyName, newValue as INotifyCollectionChanged);
            }
            else
                oldValueList[e.PropertyName] = newValue;
        }
        /// <summary>
        /// 集合更改处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollectionChangedHandle(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsList(sender))
            {
                RegisterIListChanges(sender, e);
            }
            else if (IsCollection(sender))
            {
                RegisterICollectionChanges(sender, e);
            }
            else
            {
                Debug.Assert(false, "The Class that implements INotifyCollectionChanged does not Implement ICollection<T>, IList or IList<T>");
            }
        }
        /// <summary>
        /// 属性更改栈处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StateChangedHandle(object sender, StateChangedEventArgs e)
        {
            if (e.IsProvideValue && IsMergePropertyChange)
                throw new InvalidOperationException("when auto merge perperty change, can't provide custom new value and old value.");

            object newValue = null;
            PropertyInfo property = null;
            if (IsCollectionProperty(sender, e.PropertyName, out property, out newValue))
            {
                UpdateCollectionChangedRegister(e.PropertyName, newValue as INotifyCollectionChanged);
                return;
            }

            if (IsMergePropertyChange)
            {
                UpdateCachedPropertyChange(e, property, newValue);
            }
            else
            {
                var undoTask = CreatePropertyUndoTask(e, property, newValue);
                AddTask(undoTask);
            }
        }
        /// <summary>
        /// 判断给定的属性是否是集合属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="propertyName"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private bool IsCollectionProperty(object sender, string propertyName, out System.Reflection.PropertyInfo property, out object newValue)
        {
            property = sender.GetType().GetProperty(propertyName);
            newValue = property.GetValue(sender, EmptyArray);
            if (newValue is INotifyCollectionChanged)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 在集合属性更改时,同步更新CollectionChanged注册事件
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="newValue"></param>
        private void UpdateCollectionChangedRegister(string propertyName, INotifyCollectionChanged newValue)
        {
            var temp = oldValueList[propertyName];
            var oldValue = temp as INotifyCollectionChanged;
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= ObjectItem_CollectionChanged;
            }
            if (newValue != null)
            {
                newValue.CollectionChanged += ObjectItem_CollectionChanged;
            }
            this.oldValueList[propertyName] = newValue;
        }

        private void RegisterICollectionChanges(object sender, NotifyCollectionChangedEventArgs e)
        {
            var add = sender.GetType().GetMethod("Add");
            var remove = sender.GetType().GetMethod("Remove");
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var commandAdd = new UndoTask(this, EnumUndoAction.AddObject, param =>
                    {
                        foreach (var item in e.NewItems)
                        {
                            add.Invoke(sender, new object[] { item });
                        }
                    }
                    , param =>
                    {
                        foreach (var item in e.NewItems)
                        {
                            remove.Invoke(sender, new Object[] { item });
                        }
                    });
                    AddTask(commandAdd);
                    break;
                case NotifyCollectionChangedAction.Move:
                    throw new NotSupportedException("You can only Move object in IList or IList<T> not Collection<T>");
                case NotifyCollectionChangedAction.Remove:
                    var commandRemove = new UndoTask(this, EnumUndoAction.MoveObject, param =>
                    {
                        foreach (var item in e.OldItems)
                        {
                            remove.Invoke(sender, new Object[] { item });
                        }
                    }
                    , param =>
                    {

                        foreach (var item in e.OldItems)
                        {
                            add.Invoke(sender, new object[] { item });
                        }
                    }
                    );
                    AddTask(commandRemove);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    var commandReplace = new UndoTask(this, EnumUndoAction.ReplaceObject, param =>
                    {
                        foreach (var item in e.OldItems)
                        {
                            remove.Invoke(sender, new object[] { item });
                        }
                        foreach (var item in e.NewItems)
                        {
                            add.Invoke(sender, new object[] { item });
                        }
                    }
                        , param =>
                        {

                            foreach (var item in e.NewItems)
                            {
                                remove.Invoke(sender, new object[] { item });
                            }
                            foreach (var item in e.OldItems)
                            {
                                add.Invoke(sender, new object[] { item });
                            }

                        });
                    AddTask(commandReplace);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Debug.Assert(true, "No NotifyCollectionChangedAction.Reset shuld exist");
                    break;
                default:
                    Debug.Assert(true, "No default shuld exist");
                    break;
            }

        }

        private void RegisterIListChanges(object sender, NotifyCollectionChangedEventArgs e)
        {
            var indexer = sender.GetType().GetProperty("Item");
            var insert = sender.GetType().GetMethod("Insert");
            var removeAt = sender.GetType().GetMethod("RemoveAt");
            switch (e.Action)
            {

                case NotifyCollectionChangedAction.Add:
                    var commandAdd = new UndoTask(this, EnumUndoAction.AddObject, param =>
                    {
                        for (int i = e.NewItems.Count - 1; i >= 0; i--)
                        {
                            insert.Invoke(sender, new object[] { e.NewStartingIndex, e.NewItems[i] });
                        }
                    }
                    , param =>
                    {
                        for (int i = 0; i < e.NewItems.Count; i++)
                        {
                            if (indexer.GetValue(sender, new object[] { e.NewStartingIndex }) != e.NewItems[i])
                            {
                                Debugger.Break();
                            }
                            Debug.Assert(indexer.GetValue(sender, new object[] { e.NewStartingIndex }) == e.NewItems[i]);

                            removeAt.Invoke(sender, new Object[] { e.NewStartingIndex });
                        }
                    });
                    AddTask(commandAdd);
                    break;
                case NotifyCollectionChangedAction.Move:
                    var commandMove = new UndoTask(this, EnumUndoAction.MoveObject, param =>
                    {
                        for (int i = 0; i < e.OldItems.Count; i++)
                        {
                            Debug.Assert(indexer.GetValue(sender, new object[] { e.OldStartingIndex }) == e.OldItems[i]);

                            removeAt.Invoke(sender, new Object[] { e.OldStartingIndex });
                        }
                        for (int i = e.NewItems.Count - 1; i >= 0; i--)
                        {
                            insert.Invoke(sender, new object[] { e.NewStartingIndex, e.NewItems[i] });
                        }
                    }
                    , param =>
                    {
                        for (int i = 0; i < e.NewItems.Count; i++)
                        {
                            Debug.Assert(indexer.GetValue(sender, new object[] { e.NewStartingIndex }) == e.NewItems[i]);

                            removeAt.Invoke(sender, new Object[] { e.NewStartingIndex });
                        }
                        for (int i = e.NewItems.Count - 1; i >= 0; i--)
                        {
                            insert.Invoke(sender, new object[] { e.OldStartingIndex, e.OldItems[i] });
                        }
                    }
                    );
                    AddTask(commandMove);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var commandRemove = new UndoTask(this, EnumUndoAction.RemoveObject, param =>
                    {
                        for (int i = 0; i < e.OldItems.Count; i++)
                        {
                            Debug.Assert(indexer.GetValue(sender, new object[] { e.OldStartingIndex }) == e.OldItems[i]);

                            removeAt.Invoke(sender, new Object[] { e.OldStartingIndex });
                        }
                    }
                    , param =>
                    {
                        for (int i = e.OldItems.Count - 1; i >= 0; i--)
                        {
                            insert.Invoke(sender, new object[] { e.OldStartingIndex, e.OldItems[i] });
                        }
                    }
                    );
                    AddTask(commandRemove);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    var commandReplace = new UndoTask(this, EnumUndoAction.ReplaceObject, param =>
                    {
                        for (int i = e.NewItems.Count - 1; i >= 0; i--)
                        {
                            indexer.SetValue(sender, e.NewItems[i], new object[] { i + e.NewStartingIndex });
                        }
                    }
                        , param =>
                        {
                            for (int i = 0; i < e.NewItems.Count; i++)
                            {
                                indexer.SetValue(sender, e.OldItems[i], new object[] { i + e.NewStartingIndex });
                            }
                        });
                    AddTask(commandReplace);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    {
                        Debug.Assert(false, "No NotifyCollectionChangedAction.Reset");
                        break;
                    }
                default:
                    {
                        Debug.Assert(false, "No default shuld exist");
                        break;
                    }
            }
        }

        private static bool IsList(object sender)
        {
            if (sender is IList)
                return true;
            Queue<Type> queue = new Queue<Type>(sender.GetType().GetInterfaces());
            while (queue.Count != 0)
            {
                var inter = queue.Dequeue();
                foreach (var item in inter.GetInterfaces())
                    queue.Enqueue(item);

                if (!inter.IsGenericType)
                    continue;
                var listGenType = typeof(IList<object>).GetGenericTypeDefinition();

                var gentType = inter.GetGenericTypeDefinition();
                if (listGenType == gentType)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsCollection(object sender)
        {
            Queue<Type> queue = new Queue<Type>(sender.GetType().GetInterfaces());
            while (queue.Count != 0)
            {
                var inter = queue.Dequeue();
                foreach (var item in inter.GetInterfaces())
                    queue.Enqueue(item);

                if (!inter.IsGenericType)
                    continue;
                var listGenType = typeof(ICollection<object>).GetGenericTypeDefinition();

                var gentType = inter.GetGenericTypeDefinition();
                if (listGenType == gentType)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 从StateChanged事件参数,创建PropertyUndoTask
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected PropertyUndoTask CreatePropertyUndoTask(StateChangedEventArgs e, PropertyInfo property, object currentValue)
        {
            if (e.IsProvideValue)//使用提供的值,生成属性更改记录,并刷新记录的OldValue
            {
                oldValueList[e.PropertyName] = e.NewValue;
                return new PropertyUndoTask(this, objectItem, property, e.OldValue, e.NewValue);
            }
            else // 直接生成属性更改记录,不需要更新OldValue,OldValue在PropertyChanged事件中进行更新
            {
                var oldValue = oldValueList[e.PropertyName];
                return new PropertyUndoTask(this, objectItem, property, oldValue, currentValue);
            }
        }
        /// <summary>
        /// 添加方法记录,覆盖父类的实现
        /// 主要提供给列表操作,添加记录时使用,直接缓存记录,
        /// 在启用属性合并时,PropertyUndoTask记录只会调用该方法一次
        /// </summary>
        /// <param name="undoTask">要添加的任务</param>
        private void AddTask(UndoTask undoTask)
        {
            if (IsMergePropertyChange)
            {
                tempTaskList.Add(undoTask);
            }
            else
            {
                base.AddRecord(undoTask);
            }
        }

        #region 组合任务,自动属性去重
        /// <summary>
        /// 更新缓存的属性更改记录
        /// </summary>
        /// <param name="e"></param>
        /// <param name="property"></param>
        /// <param name="currentValue">当前的属性值</param>
        private void UpdateCachedPropertyChange(StateChangedEventArgs e, PropertyInfo property, object currentValue)
        {
            PropertyUndoTask undoTask = null;
            if (tempPropertyTaskDictionary.TryGetValue(e.PropertyName, out undoTask))
            {
                undoTask.NewValue = currentValue;
            }
            else //如果还没有该属性的记录,则调用一次
            {
                undoTask = CreatePropertyUndoTask(e, property, currentValue);
                tempPropertyTaskDictionary.Add(e.PropertyName, undoTask);
                AddTask(undoTask);
            }
        }
        /// <summary>
        /// 在开启打包处理时,初始化列表
        /// </summary>
        protected override void OnBeginedCompositeTask(UndoTask undoTask)
        {
            tempTaskList = new List<UndoTask>();
            tempPropertyTaskDictionary = new Dictionary<string, PropertyUndoTask>();

            //在第一次上报任务到CompositeTask时,如果需要执行合并任务,则CompositeTask会反向回调该方法
            //同时传递回在AddRecord方法中注入的UndoTask任务,
            //这时需要初始化临时任务列表
            tempTaskList.Add(undoTask);
            var propertyUndo = undoTask as PropertyUndoTask;
            if (propertyUndo != null)
            {
                tempPropertyTaskDictionary.Add(propertyUndo.PropertyName, propertyUndo);
            }
        }
        /// <summary>
        /// 结束打包时,根据缓存的任务,创建任务列表
        /// </summary>
        /// <returns></returns>
        protected override List<UndoTask> OnEndingCompositeTask()
        {
            //释放临时列表内存
            var temp = tempTaskList;
            tempTaskList = null;
            tempPropertyTaskDictionary = null;
            //返回缓存的任务列表
            return temp;
        }
        #endregion

        #region 监视器开关处理
        /// <summary>
        /// 判断两个值是否相等
        /// </summary>
        /// <param name="newValue">新的值</param>
        /// <param name="oldValue">旧的值</param>
        /// <returns></returns>
        private bool IsValueEquals(object newValue, object oldValue)
        {
            if (newValue != null)
                return newValue.Equals(oldValue);
            if (oldValue == null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 搜集更改的属性
        /// </summary>
        /// <param name="isCreateRecorder">是否创建历史记录</param>
        /// <returns></returns>
        protected List<UndoTask> CollectChangedProperty(bool isCreateRecorder)
        {
            List<UndoTask> list = new List<UndoTask>();
            Dictionary<string, object> changedValue = new Dictionary<string, object>();
            Type objectItemType = objectItem.GetType();
            foreach (var item in oldValueList)
            {
                var prop = objectItemType.GetProperty(item.Key);
                var currentValue = prop.GetValue(objectItem, EmptyArray);
                if (IsValueEquals(currentValue, item.Value) == false)
                {
                    if (currentValue is INotifyCollectionChanged)
                    {
                        UpdateCollectionChangedRegister(item.Key, currentValue as INotifyCollectionChanged);
                    }
                    else if (isCreateRecorder)
                    {
                        list.Add(new PropertyUndoTask(this, objectItem, prop, item.Value, currentValue));
                    }
                    changedValue.Add(item.Key, currentValue);
                }
            }
            foreach (var item in changedValue)
            {
                oldValueList[item.Key] = item.Value;
            }
            return list;
        }
        /// <summary>
        /// 在打开监视器时,重新注册属性更改事件
        /// </summary>
        /// <param name="isCreateRecorder">在重新开启时,是否自动生成记录</param>
        protected override void OnStart(bool isCreateRecorder)
        {
            this.objectItem.PropertyChanged += this.ObjectItem_PropertyChanged;
            //搜集更改的属性,并刷新存储的OldValues
            var list = CollectChangedProperty(isCreateRecorder);
            foreach (var item in list)
            {
                base.AddRecord(item);
            }
        }
        /// <summary>
        /// 重载关闭处理,提供自动生成属性记录的实现
        /// </summary>
        protected override void OnStop()
        {
            //在关闭监视器时,取消对属性更改事件的注册
            this.objectItem.PropertyChanged -= this.ObjectItem_PropertyChanged;
        }
        #endregion
        #endregion
    }
}
