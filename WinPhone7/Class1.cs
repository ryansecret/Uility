using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WinPhone7
{
    public class Class1:IComparable
    {
        private static string GetAppAttribute(string attributeName)
        {
            
            string appManifestName = "WMAppManifest.xml";
            string appNodeName = "App";

            var settings = new XmlReaderSettings();
            settings.XmlResolver = new XmlXapResolver();
            
            using (XmlReader rdr = XmlReader.Create(appManifestName, settings))
            {
                rdr.ReadToDescendant(appNodeName);
                if (!rdr.IsStartElement())
                {
                    throw new System.FormatException(appManifestName + " is missing " + appNodeName);
                }
               System.Threading.SynchronizationContext.SetSynchronizationContext();
                return rdr.GetAttribute(attributeName);
            }
        }

        public int CompareTo(object obj)
        {
            return 0;
        }
    }

    public class Comparer:IComparer<Class1>
    {
        public int Compare(Class1 x, Class1 y)
        {
            return 0;
        }
    }
}
