using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Wpf
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        void test()
        {
            System.Diagnostics.Process process = new Process();
            process.Exited += new EventHandler(process_Exited);
            
        }

        void process_Exited(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
