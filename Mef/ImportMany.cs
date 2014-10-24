using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mef
{
    using System.ComponentModel.Composition;
    using System.Windows.Controls;

    using Microsoft.Windows.Controls;

    public partial class ImportMany  
    {
        [ImportMany]
        public IEnumerable<ILogger> Loggers { get; set; }

        public ImportMany()
        {
           
          
            if (Loggers == null)
            {
                foreach (var logger in Loggers)
                {
                    logger.WriteLog("Hello World");
                }
            }

            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var parent = new CompositionContainer(catalog);

            var filteredCat = new FilteredCatalog(catalog,
                def => def.Metadata.ContainsKey("scope") &&
                def.Metadata["scope"].ToString() == "webrequest");

            var perRequest = new CompositionContainer(filteredCat, parent);
           
            
        }

      
    }

    public interface ILogger
    {
        void WriteLog(string message);
    }

    [Export(typeof(ILogger))]
    public class TXTLogger : ILogger
    {
        public void WriteLog(string message)
        {
            MessageBox.Show("TXTLogger>>>>>" + message);
        }
    }

    [Export(typeof(ILogger))]
    public class DBLogger : ILogger
    {
        public void WriteLog(string message)
        {
            MessageBox.Show("DBLogger>>>>>" + message);
        }
    }



}
