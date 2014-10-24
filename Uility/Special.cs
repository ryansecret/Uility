// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Special.cs" company="">
//   
// </copyright>
// <summary>
//   The special.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Security.Permissions;
using System.Threading;

namespace Uility
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// The special.
    /// </summary>
    public class Special
    {
        #region Constants and Fields

        /// <summary>
        ///   The my pictur path.
        /// </summary>
        public string MyPicturPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        /// <summary>
        ///   The return value.
        /// </summary>
        public Tuple<int, string, string> ReturnValue;

        /// <summary>
        ///   The contion.
        /// </summary>
        public Predicate<string> contion;

        // note:程序运行时的一个或多个错误
        /// <summary>
        /// The ae.
        /// </summary>
        private AggregateException ae = new AggregateException();

        /// <summary>
        /// The list.
        /// </summary>
        private List<Person> list = new List<Person>();

        /// <summary>
        ///   The nums.
        /// </summary>
        private int[] nums = Enumerable.Range(0, 1000000).ToArray();

        /// <summary>
        ///   The stop watch.
        /// </summary>
        private Stopwatch stopWatch = new Stopwatch();

        #endregion

        // public async Task<int> SumPageSizesAsync(IList<Uri> uris) {
        // int total = 0;
        // foreach (var uri in uris) {
        // statusText.Text = string.Format("Found {0} bytes ...", total);
        // var data = await new WebClient().DownloadDataAsync(uri);
        // total += data.Length;
        // }
        // statusText.Text = string.Format("Found {0} bytes total", total);
        // return total;
        // }
        #region Public Methods

        /// <summary>
        /// The excute.
        /// </summary>
        
        public void Excute()
        {

            Thread thread = new Thread(new ThreadStart(FileProcess));
            thread.Start();
            thread.Join();
        }

        /// <summary>
        /// The file process.
        /// </summary>
        public void FileProcess()
        {
            var errorlines = from file in Directory.EnumerateFiles(@"C:\logs", "*.log")
                             from line in File.ReadLines(file)
                             where line.StartsWith("Error:", StringComparison.OrdinalIgnoreCase)
                             select string.Format("File={0}, Line={1}", file, line);
            File.WriteAllLines(@"C:\errorlines.log", errorlines);
        }

        /// <summary>
        /// The serch.
        /// </summary>
       
        public void Serch()
        {   
            
            List<string> dd = new List<string>();
            List<string> cc = new List<string>();
 
            this.list.Add(new Person() { name = "s", Sex = "ss" });
            this.list.Add(new Person() { name = "dd", Sex = "ss" });
            //seed是累加器的初始化值
            this.list.Aggregate("<peoples>", (string current, Person next) => current + next.Sex);
           
            SortedSet<Person> set = new SortedSet<Person>();
            List<Person> preList=new List<Person>();
            preList.Sort();
        }
        
        #endregion
    }

    internal class MyComparater : Comparer<string> 
    {
        public override int Compare(string x, string y)
        {
            
        }
    }

    /// <summary>
    /// The person.
    /// </summary>
    internal class Person:IComparable<Person>,IEqualityComparer<Person>
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Sex.
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string name { get; set; }

        #endregion

        public int CompareTo(Person other)
        {
            return this.Sex.CompareTo(other.Sex);
        }

        public bool Equals(Person x, Person y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(Person obj)
        {
            throw new NotImplementedException();
        }
    }
}