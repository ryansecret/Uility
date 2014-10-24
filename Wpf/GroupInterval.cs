using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Linq;
using System.Xml.Linq;

namespace Wpf
{
    public class PriceRangeProductGrouper : IValueConverter
    {
        public int GroupInterval { get; set; }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
                              CultureInfo culture)
        {
          
           
            var price = (decimal) value;
            if (price < GroupInterval)
            {
                return String.Format(culture, "Less than {0:C}", GroupInterval);
            }
            else
            {
                int interval = (int) price/GroupInterval;
                int lowerLimit = interval*GroupInterval;
                int upperLimit = (interval + 1)*GroupInterval;
                return String.Format(culture, "{0:C} to {1:C}", lowerLimit, upperLimit);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  CultureInfo culture)
        {
            List<string> list = new List<string>();
            dynamic s = new ExpandoObject();
            s.xx = "ss";
          
            throw new NotSupportedException("This converter is for grouping only.");
        }

        #endregion
    }

    public class DriveInfomation
    {
        public void GetInfo()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            dynamic dy=new ExpandoObject();
            List<string> list = new List<string>();
            
            foreach (DriveInfo d in allDrives)
            {
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  File type: {0}", d.DriveType);
                if (d.IsReady == true)
                {
                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine(
                        "  Available space to current user:{0, 15} bytes",
                        d.AvailableFreeSpace);

                    Console.WriteLine(
                        "  Total available space:          {0, 15} bytes",
                        d.TotalFreeSpace);

                    Console.WriteLine(
                        "  Total size of drive:            {0, 15} bytes ",
                        d.TotalSize);
                }
            }
        }


    }
}