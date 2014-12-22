using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace JI
{
    public static class Helper_Cmd
    {
        /// <summary>
        /// 执行Netstat查找PID
        /// </summary>
        public static string netstat = "netstat -ano|findstr ";
        /// <summary>
        /// 关闭PID
        /// </summary>
        public static string killPID = "tskill ";

        /// <summary>
        /// 执行CMD命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static string StartCmd(string cmd)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine(cmd);
            process.StandardInput.WriteLine("exit");
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return output;
        }

        /// <summary>
        /// 获取PID
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static string GetPid(string cmd)
        {
            int pid = 0;
            string a=string .Empty;
            bool str = false;
            cmd.ToList().ForEach(c =>
            {
                if (c == '*')
                {
                    str = true;
                    return;
                }
                if (str)
                {
                    if (c.ToString()==" ")
                        return;
                    if (int.TryParse(c.ToString(), out pid))
                    {
                        a += c.ToString();
                    }
                    else str = false;
                }
            }
            );
            return a;
        }
    }
}
