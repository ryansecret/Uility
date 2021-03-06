﻿using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Uility.Example
{
    public  class 查找IIS的绝对路径
    {
        private void getDirectory()
        {
           
            DirectoryEntry root = new DirectoryEntry("IIS://localhost/W3SVC");
            DirectoryEntry defaultSite = null;
            try
            {
                defaultSite = FindDefaultSite(root);
            }
            catch
            {
                
                while (true)
                {
                    string LocalhostIP="";
                        root = new DirectoryEntry("IIS://" +LocalhostIP + "/W3SVC");
                        try
                        {
                            defaultSite = FindDefaultSite(root);
                        }
                        catch
                        {
                           
                            continue;
                        }
                   
                }

            }
            DirectoryEntry siteRoot = defaultSite.Children.Find("ROOT", "IIsWebVirtualDir");
            DirectoryEntry IDSServerVDir = siteRoot.Children.Find("IDSServer", "IIsWebVirtualDir");
            
           string  ServerDirectoryPath = (string)(IDSServerVDir.Properties["Path"][0]);
        }
        private DirectoryEntry FindDefaultSite(DirectoryEntry root)
        {
            bool isFirst = false;
            DirectoryEntry defaultSite = null;
            foreach (DirectoryEntry site in root.Children)
            {
                if (site.SchemaClassName.Equals("IIsWebServer"))
                {
                    if (site.Invoke("Get", "ServerComment").Equals("默认网站"))
                    {
                        defaultSite = site;
                        break;
                    }
                    else if (!isFirst)
                    {
                        defaultSite = site;
                        isFirst = true;
                    }
                }
            }
            return defaultSite;
        }
    }
}
