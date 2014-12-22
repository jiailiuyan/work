using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkCommon.Events;

namespace WorkCommon.Manager
{
    public class GlobalEvent
    {
        /// <summary> 事件处理器 </summary>
        public IEventAggregator EventAggregator { get; private set; }

        static GlobalEvent instance;

        /// <summary> 获取当前对象实例 </summary>
        /// <returns></returns>
        public static GlobalEvent Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalEvent() { EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>() };
                }
                return instance;
            }
        }

        /// <summary> 通知项目切换事件 </summary>
        /// <param name="args">切换的工程参数</param>
        public void RaisePluginChange(PluginsEventArgs args)
        {
            this.EventAggregator.GetEvent<PluginsEvent>().Publish(args);
        }

        public void RaiseProjectChange(ProjectEventArgs args)
        {
            this.EventAggregator.GetEvent<ProjectEvent>().Publish(args);
        }


    }
}
