using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uility
{
    using System.Diagnostics;

    /// <summary>
    ///  通过进程执行操作
    /// </summary>
    public class ProcessOper
    {
        public static string RunCmd(string FileName, string command)
        {

            Process p = new Process();
            string ResultStr;

            p.StartInfo.FileName = FileName;
            p.StartInfo.Arguments = " " + command;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.ErrorDialog = false;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            try
            {
                p.Start();
                ResultStr = p.StandardError.ReadToEnd();
                p.Close();
                return ResultStr;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        } 
    }
}
