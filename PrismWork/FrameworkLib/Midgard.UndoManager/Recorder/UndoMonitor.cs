using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Windows.Input;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections;
using CocoStudio.UndoManager;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Logging;

namespace CocoStudio.UndoManager
{
    /// <summary>
    /// 撤销操作监视器
    /// </summary>
    public class UndoMonitor
    {
        #region 字段,属性
        /// <summary>
        /// 用来辅助控制挂起事件通知的集合
        /// </summary>
        private readonly ISet<BlockChanges> blockChangesSet;
        /// <summary>
        /// 暂时忽略不再监视的属性
        /// </summary>
        private readonly HashSet<string> ignorPropertyList;
        /// <summary>
        /// 代表一个空值
        /// </summary>
        internal readonly static object[] EmptyArray = new object[0];
        /// <summary>
        /// 存储的旧的值
        /// </summary>
        internal readonly Dictionary<string, object> oldValues;
        /// <summary>
        /// 集合监听集合
        /// </summary>
        internal readonly Dictionary<string, INotifyCollectionChanged> collectionsListento;
        /// <summary>
        /// 当前监视的对象
        /// </summary>
        internal readonly INotifyStateChanged item;
        /// <summary>
        /// 控制当前监视是否可用
        /// </summary>
        public bool Enable { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// Creates an Instance of UndoManager
        /// </summary>
        /// <param name="toObserve">All Object's that shuld be Monitored.</param>
        public UndoMonitor(INotifyStateChanged item)
        {
            Contract.Requires(item != null);
            blockChangesSet = new HashSet<BlockChanges>();
            collectionsListento = new Dictionary<string, INotifyCollectionChanged>();
            oldValues = new Dictionary<string, object>();
            ignorPropertyList = new HashSet<string>();
            this.item = item;

            bool valueAdded = false;
            foreach (var prop in item.GetType().GetProperties())
            {
                if (prop.GetCustomAttributes(typeof(UndoPropertyAttribute), true).Length > 0)
                {
                    //Damit der UndoMeschanissmus funktioniert, muss die Property sowohl lesbar als auch schreibbar sein.
                    if (prop.CanRead && prop.CanWrite)
                    {
                        oldValues[prop.Name] = prop.GetValue(item, EmptyArray);
                        valueAdded = true;
                    }
                    var collectionChanged = prop.GetValue(item, new object[0]) as INotifyCollectionChanged;
                    if (collectionChanged != null)
                    {
                        collectionChanged.CollectionChanged += collectionChanged_CollectionChanged;

                        this.collectionsListento.Add(prop.Name, collectionChanged);
                    }
                }
            }
            if (valueAdded)
                item.StateChanged += observed_PropertyChanged;
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 集合更改处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollectionChangedHandle(object sender, NotifyCollectionChangedEventArgs e)
        {
            //这里作为一个监视器,防止重复事件
            if (blockChangesSet.Count > 0 || Enable == false || CompositeTaskManager.GetInstance().Enable == false)
                return;

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
                Debug.Assert(true, "The Class that implements INotifyCollectionChanged does not Implement ICollection<T>, IList or IList<T>");
            }
        }
        /// <summary>
        /// 属性更改栈处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyChangedHandle(object sender, StateChangedEventArgs e)
        {
            Contract.Requires(sender != null);
            Contract.Requires(e.PropertyName != null);

            var property = sender.GetType().GetProperty(e.PropertyName);

            object oldValue;
            //这里作为一个监视器,防止在回退时设置属性时,重复响应事件处理
            if (blockChangesSet.Count > 0
                || ignorPropertyList.Contains(e.PropertyName)
                || Enable == false
                || CompositeTaskManager.GetInstance().Enable == false)
            {
                //刷新oldValue中记录的property的值,在禁用监视时,不刷新记录的旧的值
                if (Enable && oldValues.TryGetValue(e.PropertyName, out oldValue))
                {
                    oldValues[e.PropertyName] = property.GetValue(sender, EmptyArray);
                }
                return;
            }

            string propertyName = e.PropertyName;

            if (collectionsListento.ContainsKey(propertyName))
            {
                collectionsListento[propertyName].CollectionChanged -= collectionChanged_CollectionChanged;
            }

            var collectionChanged = property.GetValue(sender, new object[0]) as INotifyCollectionChanged;
            if (collectionChanged != null)
            {
                //屏蔽有 IgnorUndoManagerAttribute 属性标识的 ObservableCollection 集合的监视绑定
                var propertycustom = property.GetCustomAttributes(false);
                var exsitIgnorUndoManagerAttribute = propertycustom.FirstOrDefault(i => i is IgnorUndoManagerAttribute);
                if (exsitIgnorUndoManagerAttribute == null)
                {
                    collectionChanged.CollectionChanged += collectionChanged_CollectionChanged;
                    this.collectionsListento.Add(propertyName, collectionChanged);
                }
            }

            if (oldValues.TryGetValue(e.PropertyName, out oldValue))
            {
                var newValue = property.GetValue(sender, EmptyArray);
                oldValues[e.PropertyName] = newValue;
                var undoCommand = new PropertyUndoTask(this, sender, property, oldValue, newValue);
                RegisterCommandUsage(undoCommand, null);
            }
        }

        void collectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                CollectionChangedHandle(sender, e);
            }
            catch (System.Exception ex)
            {
                ServiceLocator.Current.GetInstance<ILoggerFacade>().Log(ex.ToString(), Category.Exception, Priority.High);
            }
        }

        void observed_PropertyChanged(object sender, StateChangedEventArgs e)
        {
            try
            {
                PropertyChangedHandle(sender, e);
            }
            catch (System.Exception ex)
            {
                ServiceLocator.Current.GetInstance<ILoggerFacade>().Log(ex.ToString(), Category.Exception, Priority.High);
            }
        }

        /// <summary>
        /// Block the registration of all changes untill the IDisposable is disposed.
        /// </summary>
        /// <returns>The Disposable to dispose.</returns>
        internal IDisposable SuspendRegisteringChanges()
        {
            var blocker = new BlockChanges((sender) => blockChangesSet.Remove(sender));
            blockChangesSet.Add(blocker);
            return blocker;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(collectionsListento != null);
            Contract.Invariant(blockChangesSet != null);
        }

        private void RegisterICollectionChanges(object sender, NotifyCollectionChangedEventArgs e)
        {
            var add = sender.GetType().GetMethod("Add");
            var remove = sender.GetType().GetMethod("Remove");
            switch (e.Action)
            {

                case NotifyCollectionChangedAction.Add:
                    var commandAdd = new UndoTask(this, param =>
                    {
                        using (this.SuspendRegisteringChanges())
                        {
                            foreach (var item in e.NewItems)
                            {
                                add.Invoke(sender, new object[] { item });
                            }
                        }
                    }
                    , param =>
                    {
                        using (this.SuspendRegisteringChanges())
                        {
                            foreach (var item in e.NewItems)
                            {
                                remove.Invoke(sender, new Object[] { item });
                            }
                        }
                    });
                    RegisterCommandUsage(commandAdd, null);
                    break;
                case NotifyCollectionChangedAction.Move:
                    throw new NotSupportedException("You can only Move object in IList or IList<T> not Collection<T>");
                case NotifyCollectionChangedAction.Remove:
                    var commandRemove = new UndoTask(this, param =>
                    {
                        using (this.SuspendRegisteringChanges())
                        {
                            foreach (var item in e.OldItems)
                            {
                                remove.Invoke(sender, new Object[] { item });
                            }
                        }
                    }
                    , param =>
                    {
                        using (this.SuspendRegisteringChanges())
                        {
                            foreach (var item in e.OldItems)
                            {
                                add.Invoke(sender, new object[] { item });
                            }
                        }
                    }
                    );
                    RegisterCommandUsage(commandRemove, null);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    var commandReplace = new UndoTask(this, param =>
                    {
                        using (this.SuspendRegisteringChanges())
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
                    }
                        , param =>
                        {
                            using (this.SuspendRegisteringChanges())
                            {
                                foreach (var item in e.NewItems)
                                {
                                    remove.Invoke(sender, new object[] { item });
                                }
                                foreach (var item in e.OldItems)
                                {
                                    add.Invoke(sender, new object[] { item });
                                }
                            }
                        });
                    RegisterCommandUsage(commandReplace, null);
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
                    var commandAdd = new UndoTask(this, param =>
                    {
                        using (this.SuspendRegisteringChanges())
                        {
                            for (int i = e.NewItems.Count - 1; i >= 0; i--)
                            {
                                insert.Invoke(sender, new object[] { e.NewStartingIndex, e.NewItems[i] });
                            }
                        }
                    }
                    , param =>
                    {
                        using (this.SuspendRegisteringChanges())
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
                        }
                    });
                    RegisterCommandUsage(commandAdd, null);
                    break;
                case NotifyCollectionChangedAction.Move:
                    var commandMove = new UndoTask(this, param =>
                    {
                        using (this.SuspendRegisteringChanges())
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
                    }
                    , param =>
                    {
                        using (this.SuspendRegisteringChanges())
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
                    }
                    );
                    RegisterCommandUsage(commandMove, null);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var commandRemove = new UndoTask(this, param =>
                    {
                        using (this.SuspendRegisteringChanges())
                        {
                            for (int i = 0; i < e.OldItems.Count; i++)
                            {
                                Debug.Assert(indexer.GetValue(sender, new object[] { e.OldStartingIndex }) == e.OldItems[i]);

                                removeAt.Invoke(sender, new Object[] { e.OldStartingIndex });
                            }
                        }
                    }
                    , param =>
                    {
                        using (this.SuspendRegisteringChanges())
                        {
                            for (int i = e.OldItems.Count - 1; i >= 0; i--)
                            {
                                insert.Invoke(sender, new object[] { e.OldStartingIndex, e.OldItems[i] });
                            }
                        }
                    }
                    );
                    RegisterCommandUsage(commandRemove, null);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    var commandReplace = new UndoTask(this, param =>
                    {
                        using (this.SuspendRegisteringChanges())
                        {
                            for (int i = e.NewItems.Count - 1; i >= 0; i--)
                            {
                                indexer.SetValue(sender, e.NewItems[i], new object[] { i + e.NewStartingIndex });
                            }
                        }
                    }
                        , param =>
                        {
                            using (this.SuspendRegisteringChanges())
                            {
                                for (int i = 0; i < e.NewItems.Count; i++)
                                {
                                    indexer.SetValue(sender, e.OldItems[i], new object[] { i + e.NewStartingIndex });
                                }
                            }
                        });
                    RegisterCommandUsage(commandReplace, null);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Debug.Assert(true, "No NotifyCollectionChangedAction.Reset");
                    break;
                default:
                    Debug.Assert(true, "No default shuld exist");
                    break;
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
                Contract.Assume(typeof(IList<object>).IsGenericType);
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
                Contract.Assume(typeof(ICollection<object>).IsGenericType);
                var listGenType = typeof(ICollection<object>).GetGenericTypeDefinition();

                var gentType = inter.GetGenericTypeDefinition();
                if (listGenType == gentType)
                {
                    return true;
                }
            }
            return false;
        }

        internal void RegisterCommandUsage(UndoTask command, object parameter)
        {
            Contract.Requires(command != null);
            CompositeTaskManager.GetInstance().AddTask(command);
        }
        #endregion

        #region 公开方法
        /// <summary>
        /// 添加一个属性改变的操作
        /// </summary>
        /// <param name="sender">关联的对象</param>
        /// <param name="e">属性改变事件</param>
        public void AddUndoTask(object monitorObject, string propertyName, object oldValue, object newValue)
        {
            var property = monitorObject.GetType().GetProperty(propertyName);
            var undoCommand = new PropertyUndoTask(this, monitorObject, property, oldValue, newValue);
            RegisterCommandUsage(undoCommand, null);
        }

        /// <summary>
        /// 添加一个指定的属性更改,并提供新的值
        /// </summary>
        /// <param name="monitorObject"></param>
        /// <param name="propertyName"></param>
        /// <param name="newValue"></param>
        public void AddUndoTask(object monitorObject, string propertyName)
        {
            object oldValue;
            bool isOK = oldValues.TryGetValue(propertyName, out oldValue);
            if (isOK == false)
                return;
            var property = monitorObject.GetType().GetProperty(propertyName);
            object newValue = property.GetValue(monitorObject, null);
            if (oldValue.Equals(newValue))
            {
                return;
            }

            var undoCommand = new PropertyUndoTask(this, monitorObject, property, oldValue, newValue);
            oldValues[propertyName] = newValue;
            RegisterCommandUsage(undoCommand, null);
        }

        /// <summary>
        /// 设置对某个属性的监视是否可用
        /// 注:这里的可用必须是已经指明需要监视的属性,并对集合属性无效
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="isEnable">是否可用</param>
        public void SetPropertyMonitorEnable(string propertyName, bool isEnable)
        {
            if (!isEnable)
            {
                if (ignorPropertyList.Contains(propertyName) == false)
                    ignorPropertyList.Add(propertyName);
            }
            else
            {
                ignorPropertyList.Remove(propertyName);
            }
        }
        #endregion

        /// <summary>
        /// 用来辅助管理事件通知块,使得屏蔽重复触发事件的问题
        /// </summary>
        internal class BlockChanges : IDisposable
        {
            Action<BlockChanges> onDispose;
            public BlockChanges(Action<BlockChanges> onDispose)
            {
                IsDisposed = false;
                this.onDispose = onDispose;
            }

            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
                onDispose(this);
            }
        }
    }
}
