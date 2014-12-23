using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace WorkCommon.Manager
{
    /// <summary>
    /// 
    /// </summary>
    class CSLogger : ICSLog
    {
        /// <summary>
        /// 输出事件,提供给输出窗口注册,
        /// 不再走全局的消息分析逻辑
        /// </summary>
        public event Action<string> Output = delegate { };
        /// <summary>
        /// Log4Net代理器
        /// </summary>
        private static ILog Log
        {
            get { return Log4Wrap.Logger; }
        }
        /// <summary>
        /// 是否输出
        /// </summary>
        private bool isOutput;

        /// <summary>
        /// 内部构造函数,
        /// 该类无法在外部创建
        /// </summary>
        /// <param name="log"></param>
        internal CSLogger(bool isOutput)
        {
            this.isOutput = true;
        }

        #region 方法
        /// <summary>
        /// 触发输出事件
        /// </summary>
        /// <param name="message"></param>
        private void RaiseOutput(object message)
        {
            if (isOutput)
            {
                Output(message.ToString());
            }
        }

        public void Debug(object message)
        {
            RaiseOutput(message);
            Log.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            RaiseOutput(message);
            Log.Debug(message, exception);
        }

        public void Error(object message)
        {
            RaiseOutput(message);
            Log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            RaiseOutput(message);
            Log.Error(message, exception);
        }

        public void Info(object message)
        {
            RaiseOutput(message);
            //Log.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            RaiseOutput(message);
            //Log.Info(message, exception);
        }
        #endregion
    }
}
