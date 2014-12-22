using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Threading;
using System.IO;

namespace JI
{
    public class Helper_Socket
    {
        /// <summary>
        /// UDP连接
        /// </summary>
        public class SocketUDP
        {
            public SocketUDP()
            {
            }
            private Socket mySocket;
            private EndPoint RemotePoint;
            public Helper_Message Message;
            public event EventHandler UDPRecived;
            public bool RunningFlag = false;
            private System.Threading.Thread thdUdp;
            /// <summary>
            /// UDPListen事件
            /// </summary>
            protected virtual void UDPOnRecived()
            {
                var handler = UDPRecived;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
            /// <summary>
            /// 开启UDP端口
            /// </summary>
            /// <param name="LocalIP"></param>
            /// <param name="portList"></param>
            /// <returns></returns>
            public UDPPort? UDPStart(string LocalIP, List<int> portList)
            {
                UDPPort udpPort = new UDPPort();
                udpPort.UsedPort = new List<int>();
                List<int> usedport = new List<int>();
                IPAddress ip = IPAddress.Parse(LocalIP);
                IPEndPoint ipLocalPoint = new IPEndPoint(ip, 0);
                mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                mySocket.ReceiveBufferSize = 1000000000;
                mySocket.SendBufferSize = 1000000000;
                #region 绑定侦听端口
                bool turn = true;
                portList.ForEach(
                    port =>
                    {
                        if (turn)
                        {
                            ipLocalPoint.Port = port;
                            try
                            {
                                mySocket.Bind(ipLocalPoint);

                                turn = false;
                                return;
                            }
                            catch
                            {
                                usedport.Add(port);
                                return;
                            }
                        }
                    }
                    );
                #endregion

                if (turn)
                    return null;
                else
                {
                    udpPort.IP = LocalIP;
                    udpPort.Port = ipLocalPoint.Port;
                    usedport.ForEach(i => { udpPort.UsedPort.Add(i); });
                    RunningFlag = true;
                    RemotePoint = new IPEndPoint(IPAddress.Parse(udpPort.IP), udpPort.Port);
                    try
                    {
                        RunningFlag = true;
                        thdUdp = new Thread(new ThreadStart(this.UDPListen));
                        thdUdp.Start();
                    }
                    catch
                    {
                    }
                    return udpPort;
                }
            }
            /// <summary>
            /// UDP发送
            /// </summary>
            /// <param name="name"></param>
            /// <param name="ip"></param>
            /// <param name="port"></param>
            /// <param name="message"></param>
            /// <returns></returns>
            public bool UDPSend(Helper_Message message)
            {
                message.msgID = Guid.NewGuid().ToString();
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(message.RIP), int.Parse(message.RPort));

                if (message.Data.Length <= 1024)
                {
                    try
                    {
                        var data = hs.SerializeBinary(message).ToArray();
                        mySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                        mySocket.SendTo(data, data.Length, SocketFlags.None, (EndPoint)(ipep));
                        return true;
                    }
                    catch { return false; }
                }
                else
                {
                    #region start
                    Helper_Message start = new Helper_Message();
                    start.sendKind = message.sendKind;
                    start.sendState = SendState.Start;
                    start.SName = message.SName;
                    start.SIP = message.SIP;
                    start.SPort = message.SPort;
                    start.RIP = message.SIP;
                    start.RPort = message.RPort;
                    start.Data = Encoding.Unicode.GetBytes(message.msgFileName);
                    var startData = Helper_Serializers.Instance.SerializeBinary(start).ToArray();
                    mySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                    mySocket.SendTo(startData, startData.Length, SocketFlags.None, (EndPoint)(ipep));
                    #endregion

                    #region Sending
                    Helper_Message sending = new Helper_Message();
                    sending.sendKind = message.sendKind;
                    sending.sendState = SendState.Sending;
                    sending.SName = message.SName;
                    sending.SIP = message.SIP;
                    sending.SPort = message.SPort;
                    sending.RIP = message.RIP;
                    sending.RPort = message.RPort;
                    sending.Data = Encoding.Unicode.GetBytes("");

                    MemoryStream stream = new MemoryStream(message.Data);
                    int sendlen = 1024;
                    long sunlen = (stream.Length);
                    int offset = 0;
                    while (sunlen > 0)
                    {
                        sendlen = 1024;
                        if (sunlen <= sendlen)
                            sendlen = Convert.ToInt32(sunlen);
                        byte[] msgdata = new byte[sendlen];
                        stream.Read(msgdata, offset, sendlen);
                        sending.sendState = SendState.Sending;
                        sending.Data = msgdata;
                        var sendingData = Helper_Serializers.Instance.SerializeBinary(sending).ToArray();
                        mySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                        mySocket.SendTo(sendingData, sendingData.Length, SocketFlags.None, (EndPoint)(ipep));
                        sunlen = sunlen - sendlen;
                    }
                    #endregion

                    #region end
                    Helper_Message end = new Helper_Message();
                    end.sendKind = message.sendKind;
                    end.sendState = SendState.End;
                    end.SName = message.SName;
                    end.SIP = message.SIP;
                    end.SPort = message.SPort;
                    end.RIP = message.RIP;
                    end.RPort = message.RPort;
                    end.Data = Encoding.Unicode.GetBytes(message.msgFileLeiXing);
                    stream.Close();
                    var endData = Helper_Serializers.Instance.SerializeBinary(end).ToArray();
                    mySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                    mySocket.SendTo(endData, endData.Length, SocketFlags.None, (EndPoint)(ipep));
                    #endregion
                    return true;
                }
            }

            Helper_Serializers hs = Helper_Serializers.Instance;

            /// <summary>
            /// 侦听UDPListen
            /// </summary>
            private void UDPListen()
            {
                byte[] data = new byte[102400];

                while (RunningFlag)
                {
                    if (mySocket == null || mySocket.Available < 1)
                    {
                        Thread.Sleep(200);
                        continue;
                    }
                    int rlen = mySocket.ReceiveFrom(data, ref RemotePoint);

                    var d = new System.IO.MemoryStream(data);
                    Message = hs.DeSerializeBinary<Helper_Message>((d)) as Helper_Message;
                    if (Message != null)
                    {
                        if (Message.IsColose)
                            RunningFlag = false;
                        UDPOnRecived();
                    }
                }
            }
            /// <summary>
            /// 关闭UDP连接
            /// </summary>
            /// <returns></returns>
            public bool UDPClose()
            {
                try
                {
                    mySocket.Close();
                    thdUdp.Abort();
                    return true;
                }
                catch { return false; }
            }
        }
    }
}