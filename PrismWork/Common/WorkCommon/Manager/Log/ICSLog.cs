using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkCommon.Manager
{
    /// <summary>
    /// 添加独立的日志接口,屏蔽对第三方日志类库的依赖
    /// </summary>
    public interface ICSLog
    {
        /// <summary>
        /// 输出事件,提供给输出窗口注册,
        /// 不再走全局的消息分析逻辑,所有通过Output接口发送的信息,都会触发该事件
        /// </summary>
        event Action<string> Output;

        /// <summary>
        /// Debug信息
        /// </summary>
        /// <param name="message">自定义信息</param>
        void Debug(object message);
        /// <summary>
        /// Debug信息
        /// </summary>
        /// <param name="message">自定义信息</param>
        /// <param name="exception">异常信息</param>
        void Debug(object message, Exception exception);
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">自定义信息</param>
        void Error(object message);
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">自定义信息</param>
        /// <param name="exception">异常信息</param>
        void Error(object message, Exception exception);
        /// <summary>
        /// 一般提示信息输出
        /// </summary>
        /// <param name="message">自定义信息</param>
        void Info(object message);
        /// <summary>
        /// 一般提示信息输出
        /// </summary>
        /// <param name="message">自定义信息</param>
        /// <param name="exception">异常信息</param>
        void Info(object message, Exception exception);
    }
}
