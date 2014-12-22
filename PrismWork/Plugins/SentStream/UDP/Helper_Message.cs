using System;
using System.ComponentModel.Composition;

namespace JI
{
    /// <summary>
    /// ClassMsg 的摘要说明。
    /// </summary>
    [Serializable]
    [Export(typeof(Helper_Message))]
    public class Helper_Message
    {
        /// <summary>
        /// 发送方名称
        /// </summary>
        public String SName { get; set; }
        /// <summary>
        /// 发送方IP
        /// </summary>
        public String SIP { get; set; }
        /// <summary>
        /// 发送方端口号
        /// </summary>
        public String SPort { get; set; }
        /// <summary>
        /// 接收方编号
        /// </summary>
        public String RName = "";
        /// <summary>
        /// 接收方IP
        /// </summary>
        public String RIP = "";
        /// <summary>
        /// 接收方端口号
        /// </summary>
        public String RPort = "";
        /// <summary>
        /// 发送消息类型，默认为无类型
        /// </summary>
        public SendKind sendKind = SendKind.SendNone;
        /// <summary>
        /// 消息命令
        /// </summary>
        public MsgCommand msgCommand = MsgCommand.None;
        /// <summary>
        /// 消息发送状态
        /// </summary>
        public SendState sendState = SendState.None;
        /// <summary>
        /// 消息ID，GUID
        /// </summary>
        public String msgID = "";
        /// <summary>
        /// 消息数据
        /// </summary>
        public byte[] Data;
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime dateTime = DateTime.Now;
        /// <summary>
        /// 消息ID，GUID
        /// </summary>
        public String msgFileName = "";
        /// <summary>
        /// 消息ID，GUID
        /// </summary>
        public String msgFileLeiXing = "";
        /// <summary>
        /// 是否关闭
        /// </summary>
        public bool IsColose = false;


        public Helper_Message()
        {


        }
    }

    /// <summary>
    /// 消息命令
    /// </summary>
    public enum MsgCommand
    {
        None,
        Login,//用户登录结束,上线
        SendToOne,//发送单用户
        SendToAll,//发送所有用户
        UserList,//用户列表
        UpdateState,//更新用户状态
        VideoOpen,//打开视频
        Videoing,//正在视频
        VideoClose,//关闭视频
        Close//下线
    }

    /// <summary>
    /// 发送类型
    /// </summary>
    public enum SendKind
    {
        /// <summary>
        /// 无类型
        /// </summary>
        SendNone,
        /// <summary>
        /// 发送命令
        /// </summary>
        SendCommand,
        /// <summary>
        /// 发送消息
        /// </summary>
        SendMsg,
        /// <summary>
        /// 发送文件
        /// </summary>
        SendFile,

        SendLogin,
        SendLeave
    }

    /// <summary>
    /// 发送状态
    /// </summary>
    public enum SendState
    {
        /// <summary>
        /// 无状态
        /// </summary>
        None,
        /// <summary>
        /// 发送单消息或文件
        /// </summary>
        MessageSingle,
        /// <summary>
        /// 发送开始生成文件
        /// </summary>
        Start,
        /// <summary>
        /// 正在发送中，写入文件
        /// </summary>
        Sending,
        /// <summary>
        /// 发送结束
        /// </summary>
        End
    }
}
