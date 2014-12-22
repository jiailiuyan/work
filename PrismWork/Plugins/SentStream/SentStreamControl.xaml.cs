using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.IO.Ports;
using JI;
using System.Windows.Threading;
using System.Drawing;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using WorkCommon.Plugin;
using WorkCommon.Manager;
using WorkCommon.Events;

namespace SentStream
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    [ExportAttribute(typeof(IPluginObject))]
    public partial class SentStreamControl : UserControl, IPluginObject
    {
        public SentStreamControl()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);

         
        }
        private bool canSent = false;
        private string LocalIP = string.Empty;
        private string LocalPort = string.Empty;
        private string LocalSubNet = string.Empty;
        private string BroadcastIP = string.Empty;
        private string name = string.Empty;

        List<int> portList = new List<int>() { 5555, 5556, 5557, 5558, 5559 };

        List<NetInfoStruct> net = new List<NetInfoStruct>();
        Helper_Socket.SocketUDP udpSocket = new Helper_Socket.SocketUDP();

        #region MyRegion

        public string PluginName
        {
            get { return "即时通讯"; }
        }

        public ImageSource PluginIcon
        {
            get
            {
                return new BitmapImage(new Uri("/SentStream;component/Images/23.png", UriKind.Relative));
            }
        }

        private FrameworkElement pluginobject;
        public FrameworkElement Plugin
        {
            get
            {
                if (pluginobject == null)
                {
                    pluginobject = new SentStreamControl();
                }
                return pluginobject;
            }
        }

        public PluginType Type
        {
            get { return PluginType.Window; }
        }

        public event EventHandler Closing;
        protected void OnClosingChanged()
        {
            var d = this.GetHashCode();
            if (this.Closing != null)
            {
                this.Closing(this, EventArgs.Empty);
            }
        }

        public event EventHandler Opening;
        protected void OpeningChanged()
        {
            if (this.Opening != null)
            {
                this.Opening(this, EventArgs.Empty);
            }
        }

        public event EventHandler Hiding;
        protected void HidingChanged()
        {
            if (this.Hiding != null)
            {
                this.Hiding(this, EventArgs.Empty);
            }
        }

        public bool IsShow
        {
            get
            {
                return this.Visibility == System.Windows.Visibility.Visible;
            }
            set
            {
                if (value)
                {
                    OpeningChanged();
                }
                else
                {
                    HidingChanged();
                }
            }
        }

        #endregion

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            userName.Text = "迹";
            List<string> ip = new List<string>();
            net = Helper_Net.NetInfo();

            net.ForEach(i => { ip.Add(i.Name + " " + i.IP[0]); });
            comboBox1.ItemsSource = ip;
            comboBox1.SelectedIndex = 0;

            this.DataContext = this;


            this.Unloaded += SentStreamControl_Unloaded;

            GlobalEvent.Instance.EventAggregator.GetEvent<ProjectEvent>().Subscribe(ProjectEventChanged);
        }

        void ProjectEventChanged(ProjectEventArgs args)
        {
            if (args.Action == ProjectAction.Close)
            {
                if (udpSocket.RunningFlag)
                {
                    udpSocket.UDPClose();
                }
            }
        }

        void SentStreamControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (udpSocket.RunningFlag)
            {
                udpSocket.UDPClose();
            }
        }

        private void btSend_Click(object sender, RoutedEventArgs e)
        {

        kong: string ip = tbLocalIP.Text;
            string port = tbLocalPort.Text;
            if (ip.Trim().Length < 7 || port.Trim().Length < 3)
            {
                tbLocalIP.Text = LocalIP;
                tbLocalPort.Text = LocalPort;
                goto kong;
            }

            byte[] data = Encoding.Unicode.GetBytes(tbSendMsg.Text);

            Helper_Message msg = new Helper_Message();
            msg.SName = name;
            msg.SIP = LocalIP;
            msg.SPort = LocalPort;
            msg.RIP = ip;
            msg.RPort = port;
            msg.Data = data;
            msg.sendKind = SendKind.SendMsg;
            msg.sendState = SendState.MessageSingle;
            var dat = Helper_Serializers.Instance.SerializeBinary(msg).ToArray();
            if (canSent)
                udpSocket.UDPSend(msg);
        }

        // 开启
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            userName.IsEnabled = false;
            comboBox1.IsEnabled = false;

            name = userName.Text.Trim();
            LocalIP = comboBox1.SelectedValue.ToString().Split(' ')[1];
            net.ForEach(i => { foreach (var ip in i.IP) { if (ip == LocalIP) LocalSubNet = i.Subnet[0]; } });

            udpSocket.UDPRecived += new EventHandler(udpSocket_Recived);
            udpSocket.Message = new Helper_Message();

            UDPPort? udpPort = udpSocket.UDPStart(LocalIP, portList);

            if (udpPort != null)
            {
                LocalPort = udpPort.Value.Port.ToString();
                Helper_Message message = new Helper_Message();
                message.SName = name;
                message.sendKind = SendKind.SendLogin;
                message.SIP = LocalIP;
                message.SPort = LocalPort;
                message.RIP = Broadcast();
                message.Data = Helper.StringToByte("迹");
                portList.ForEach(i =>
                {
                    message.RPort = i.ToString();
                    udpSocket.UDPSend(message);
                });
                canSent = true;
                button6.Content = "侦听端口：" + LocalPort;
                button6.IsEnabled = false;
            }
        }

        List<byte> info = new List<byte>();

        // 接受处理事件
        void udpSocket_Recived(object sender, EventArgs e)
        {
            if (udpSocket.Message.Data != null)
            {
                switch (udpSocket.Message.sendKind)//发送类型
                {
                    #region 发送文件 SendKind.SendFile
                    case SendKind.SendFile:
                        {
                            switch (udpSocket.Message.sendState)
                            {
                                case SendState.Start:
                                    {
                                        //文件名字
                                        info.Clear();
                                        break;
                                    }
                                case SendState.Sending:
                                    {
                                        info.AddRange(udpSocket.Message.Data);
                                        break;
                                    }
                                case SendState.End:
                                    {
                                        //文件类型
                                        byte[] bt = info.ToArray();
                                        using (MemoryStream ms = new MemoryStream(bt))
                                        {
                                            BitmapDecoder decoder = BitmapDecoder.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                                            var img = decoder.Frames[0];
                                            //ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
                                            //var img = (BitmapSource)imageSourceConverter.ConvertFrom(ms);
                                            img.Freeze();
                                            Dispatcher.Invoke((Action)delegate
                                            {
                                                //Helper.SaveImageCapture((BitmapSource)imageSourceConverter.ConvertFrom(ms));
                                                image4.Source = img;
                                            });
                                        }
                                        break;
                                    }
                                default: break;
                            }
                        }
                        break;

                    #endregion
                    #region 发送消息 SendKind.SendMsg
                    case SendKind.SendMsg:
                        {
                            switch (udpSocket.Message.sendState)
                            {
                                case SendState.MessageSingle:
                                    {
                                        Dispatcher.Invoke((Action)delegate
                {
                    tbMsg.Text = Encoding.Unicode.GetString(udpSocket.Message.Data);
                });
                                        break;
                                    }
                                case SendState.Start:
                                    {
                                        info.Clear();
                                        break;
                                    }
                                case SendState.Sending:
                                    {
                                        info.AddRange(udpSocket.Message.Data);
                                        break;
                                    }
                                case SendState.End:
                                    {
                                        Dispatcher.Invoke((Action)delegate
                                        {
                                            tbMsg.Text = Encoding.Unicode.GetString(info.ToArray());
                                        });
                                        break;
                                    }
                                default: break;
                            }
                            break;
                        }
                    #endregion
                    #region 发送命令 SendKind.SendCommand
                    case SendKind.SendCommand:
                        {
                            switch (udpSocket.Message.msgCommand)
                            {
                                #region 发送视频 MsgCommand.Videoing
                                case MsgCommand.Videoing:
                                    {
                                        switch (udpSocket.Message.sendState)
                                        {
                                            case SendState.Start:
                                                {
                                                    info.Clear();
                                                    break;
                                                }
                                            case SendState.Sending:
                                                {
                                                    info.AddRange(udpSocket.Message.Data);
                                                    break;
                                                }
                                            case SendState.End:
                                                {
                                                    byte[] bt = info.ToArray();
                                                    using (MemoryStream ms = new MemoryStream(bt))
                                                    {
                                                        // pictureBox1.Image = Image.FromStream(ms);
                                                    }
                                                    break;
                                                }
                                            default: break;
                                        }
                                        break;
                                    }
                                #endregion
                                default: break;
                            }
                        }
                        break;
                    #endregion

                    case SendKind.SendLogin:
                        {
                            Dispatcher.Invoke((Action)delegate
                            {
                                var u = Users.FirstOrDefault(i => i.SIP.Equals(udpSocket.Message.SIP) && i.SPort.Equals(udpSocket.Message.SPort));
                                if (u == null)
                                {
                                    Users.Add(udpSocket.Message);

                                    Helper_Message message = new Helper_Message();
                                    message.SName = name;
                                    message.sendKind = SendKind.SendLogin;
                                    message.SIP = LocalIP;
                                    message.SPort = LocalPort;
                                    message.RIP = udpSocket.Message.SIP;
                                    message.Data = Helper.StringToByte("迹");

                                    message.RPort = udpSocket.Message.SPort;
                                    udpSocket.UDPSend(message);
                                }
                            });

                            break;
                        }

                    case SendKind.SendLeave:
                        {
                            Dispatcher.Invoke((Action)delegate
                            {
                                var u = Users.FirstOrDefault(i => i.SIP.Equals(udpSocket.Message.SIP) && i.SPort.Equals(udpSocket.Message.SPort));
                                if (u != null)
                                {
                                    Users.Remove(u);
                                }
                            });

                            break;
                        }

                    default: break;
                }
            }
        }

        private ObservableCollection<Helper_Message> users = new ObservableCollection<Helper_Message>();
        public ObservableCollection<Helper_Message> Users
        {
            get { return users; }
            set { users = value; }
        }

        // 关闭
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// 获取所选IP的广播地址
        /// </summary>
        /// <returns></returns>
        public string Broadcast()
        {
            return Helper_Net.GetBroadcast(LocalIP, LocalSubNet);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            string filepath = Helper.Search();
            if (filepath.Length > 0)
            {
            kong: string ip = tbLocalIP.Text;
                string port = tbLocalPort.Text;
                if (ip.Trim().Length < 7 || port.Trim().Length < 3)
                {
                    tbLocalIP.Text = LocalIP;
                    tbLocalPort.Text = LocalPort;
                    goto kong;
                }
                FileStream fStream = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite);
                byte[] data = new byte[fStream.Length];
                fStream.Read(data, 0, data.Length);
                fStream.Close();

                Helper_Message msg = new Helper_Message();
                msg.SName = name;
                msg.SIP = LocalIP;
                msg.SPort = LocalPort;
                msg.RIP = ip;
                msg.RPort = port;
                msg.Data = data;
                msg.sendKind = SendKind.SendFile;
                msg.sendState = SendState.MessageSingle;
                if (canSent)
                    udpSocket.UDPSend(msg);
            }
        }

        List<byte[]> clientInfoTemp = new List<byte[]>();
        List<byte> clientInfo = new List<byte>();

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.brodcast.Text = Broadcast();
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            var msg = listbox.SelectedItem as Helper_Message;
            if (msg != null)
            {
                this.tbLocalIP.Text = msg.SIP;
                this.tbLocalPort.Text = msg.SPort;
            }
        }

    }
}
