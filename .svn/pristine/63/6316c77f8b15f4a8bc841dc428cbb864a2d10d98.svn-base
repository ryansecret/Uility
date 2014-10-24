using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mef
{
    using System.ComponentModel.Composition;
    using System.Windows.Controls;

    public  class MothedExport
    {
         [Export(typeof(Action<string>))]
         public void PrintBookName(string name)
         {
             Console.WriteLine(name);
         }

    }

    public   class MethodExportImport 
    {
        [Import(typeof(Action<string>))]
        public Action<string> PrintBookName { get; set; }

        public MethodExportImport()
        {
            PrintBookName("《MEF程序设计指南》");
        }
    }

}
