using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Net;
using System.Runtime.InteropServices;
using System.Management;
using System.Net.NetworkInformation;

namespace JI
{
    public static class Helper_Net
    {
        /// <summary>
        /// 获取网卡信息
        /// </summary>
        /// <returns></returns>
        public static List<NetInfoStruct> NetInfo()
        {
            List<NetInfoStruct> netList = new List<NetInfoStruct>();
            ManagementObjectSearcher query = new
      ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {
                NetInfoStruct net = new NetInfoStruct();
                net.IP = mo["IPAddress"] as string[];
                net.Subnet = mo["IPSubnet"] as string[];
                net.Gateway = mo["DefaultIPGateway"] as string[];
                net.DNS = mo["DNSServerSearchOrder"] as string[];
                net.Description = mo["Description"] as string;
                net.Mac = mo["MACAddress"] as string;
                net.Name = GetName(mo["Description"] as string);
                netList.Add(net);
            }
            return netList;
        }

 
        [DllImport("kernel32")]
        private static extern void GetWindowsDirectory(StringBuilder WinDir, int count);
        /// <summary>
        /// 获取windows路径.
        /// </summary>
        public static string Get_windows()
        {
            const int nChars = 255;
            StringBuilder Buff = new StringBuilder(nChars);
            GetWindowsDirectory(Buff, nChars);
            return Buff.ToString();
        }

       /// <summary>
       /// 计算广播地址
       /// </summary>
       /// <param name="LocalIPAddress"></param>
       /// <param name="SubnetMaskAddress"></param>
       /// <returns></returns>
        public static string GetBroadcast(string LocalIPAddress, string SubnetMaskAddress)
        {
            string[] IPAddresses = LocalIPAddress.Split(new char[] { '.' });
            string[] subnetMaskAddresses = SubnetMaskAddress.Split(new char[] { '.' });

            byte[] IPParts = new byte[4];//IP
            byte[] maskParts = new byte[4];//子网掩码
            byte[] netParts = new byte[4];//网络地址

            for (int i = 0; i < 4; i++)
            {
                IPParts[i] = byte.Parse(IPAddresses[i]);
                maskParts[i] = byte.Parse(subnetMaskAddresses[i]);
                byte ip = IPParts[i];
                byte mask = maskParts[i];
                netParts[i] = ((byte)(ip & mask));//与运算后是网络地址
            }

            //网络号
            ulong netId = 0;
            for (int i = 0; i < 4; i++)
            {
                netId += netParts[i];
                if (i < 3)
                    netId <<= 8;
            }

            //
            ulong IPMask = 0;
            for (int i = 0; i < 4; i++)
            {
                IPMask += maskParts[i];
                if (i < 3)
                    IPMask <<= 8;
                else
                    IPMask = ~IPMask;
            }

            //算广播地址
            ulong broadcastId = IPMask | netId;
            byte[] bIPParts = new byte[4];
            for (int i = 3; i >= 0; i--)
            {
                bIPParts[i] = ((byte)(broadcastId & 255));
                if (i > 0)
                    broadcastId >>= 8;
            }
            return string.Format("{0}.{1}.{2}.{3}", bIPParts[0], bIPParts[1], bIPParts[2], bIPParts[3]);
        }

        /// <summary>
        /// 获取网卡名称
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public static string GetName(string description)
        {
            string name = string.Empty;
            if (description.Contains("Virtual"))
                name = "虚拟连接";
            else
            {
                if (description.Contains("Wireless"))
                    name += "无线连接";
                else
                    name += "本地连接";
            }
            return name;
        }

        /// <summary>
        /// 释放端口
        /// </summary>
        /// <param name="portListUse"></param>
        public static void ShiFang(List<int> portListUse)
        {
            if (portListUse.Count > 0)
            {
                portListUse.ForEach(port =>
                {
                    Helper_Cmd.StartCmd(
                        Helper_Cmd.killPID + Helper_Cmd.GetPid(
                        Helper_Cmd.StartCmd(
                        Helper_Cmd.netstat + port.ToString())));
                });
            }
        }

    }

    public struct NetInfoStruct
    {
        public string[] IP;
        public string[] Subnet;
        public string[] Gateway;
        public string[] DNS;
        public string Mac;
        public string Name;
        public string Description;
        public NetInfoStruct
            (string[] ip, string[] subnet, string[] getway, string[] dns, string mac, string name, string description)
        {
            this.IP = ip;
            this.Subnet = subnet;
            this.Gateway = getway;
            this.DNS = dns;
            this.Mac = mac;
            this.Name = name;
            this.Description = description;
        }
    }
    public struct UDPPort
    {
        public string IP;
        public int Port;
        public List<int> UsedPort;
        public UDPPort
            (string ip, int port, List<int> usedport)
        {
            this.IP = ip;
            this.Port = port;
            this.UsedPort = usedport; ;
        }
    }
}
