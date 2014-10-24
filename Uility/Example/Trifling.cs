using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Instrumentation;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Principal;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Linq;
namespace Uility.Example
{
    public abstract class Trifling
    {
        
        
        private NameValueCollection collection = new NameValueCollection();
        private string timeStamp = DateTime.UtcNow.ToString(CultureInfo.CurrentCulture);
         
        private string machineName = Environment.MachineName;
        private string windowId = WindowsIdentity.GetCurrent().Name;


        public Comparison<object> Comparison { get; set; }

        private WeakReference weakReference;
        private Nullable<DateTime> dateTime;
        private DateTimeOffset offset;

        public int Compare(Comparison<string> comparison )
        {
            return comparison("d","c");
        }

        public abstract T GetLog<T>() where T : class;
        [MethodImpl]
        void SendEvent()
        {
             NetworkChange.NetworkAddressChanged += new
    NetworkAddressChangedEventHandler(NetworkChange_NetworkAddressChanged);
            TimeZoneInfo timeZone = TimeZoneInfo.Local;
            
            Instrumentation.Fire("向事件管理器发送事件");
        }

        private void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {

            ConcurrentDictionary<string, object> concurrentDictionary = new ConcurrentDictionary<string, object>();
            ConcurrentQueue<string> concurrentQueue = new ConcurrentQueue<string>();
            ConcurrentStack<string> concurrentStack = new ConcurrentStack<string>();


            //ConcurrentBag<T> 是一种线程安全的包实现，已经针对相同线程生成和使用包中存储数据的情况进行了优化。

            ConcurrentBag<string> concurrentBag = new ConcurrentBag<string>();
        }

        bool  CompareType(object  type)
        {

            return Type.GetTypeCode(type.GetType()) == TypeCode.Boolean;
        }

        void ThrowException()
        {
            throw new DirectoryNotFoundException();
            throw new AuthenticationException();
            throw new BadImageFormatException();
            throw new NotSupportedException();
            throw new ArgumentException();
            throw new ArgumentNullException();
            throw new ArgumentOutOfRangeException();
            throw new InvalidOperationException();
        }

        public void ChangeExpression()
        {
            Func<int, bool> deleg = i => i < 5;
            // Invoke the delegate and display the output.

            Console.WriteLine("deleg(4) = {0}", deleg(4));

            // Lambda expression as data in the form of an expression tree.

            System.Linq.Expressions.Expression<Func<int, bool>> expr = i => i < 5;
            // Compile the expression tree into executable code.

            Func<int, bool> deleg2 = expr.Compile();
            // Invoke the method and print the output.

            Console.WriteLine("deleg2(4) = {0}", deleg2(4));

            /*  This code produces the following output:

              deleg(4) = True
              deleg2(4) = True
          */
        }


        public void SendEmails(string server,int porter,string userName,string passWord)
        {
            MailMessage message = new MailMessage();
            message.Body = "sssss";
            MailAddress address = new MailAddress("http://");
            message.To.Add(address);
            message.From = address;
            message.BodyEncoding = Encoding.UTF8;
            SmtpClient smtpClient = new SmtpClient(server,porter);
          
           // smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
            smtpClient.Credentials = new NetworkCredential(userName,passWord);
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
        }

        public Trifling()
        {
              weakReference = new WeakReference(Comparison);
        }

        public void GetProperty()
        {
            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
        }


        public void ProcessXml()
       {
           TextWriter writer = new StringWriter();

           XmlTextWriter textWriter = new XmlTextWriter(writer);
           textWriter.Formatting = Formatting.Indented;
           textWriter.WriteStartElement("ss");
           textWriter.WriteElementString("d","ss");
           textWriter.WriteEndAttribute();
           textWriter.Close();

           //XElement serializedItems = new XElement("DesignerItems",
           //                           from item in designerItems
           //                           let contentXaml = XamlWriter.Save(((DesignerItem)item).Content)
           //                           select new XElement("DesignerItem",
           //                                      new XElement("Left", Canvas.GetLeft(item)),
           //                                      new XElement("Top", Canvas.GetTop(item)),
           //                                      new XElement("Width", item.Width),
           //                                      new XElement("Height", item.Height),
           //                                      new XElement("ID", item.ID),
           //                                      new XElement("zIndex", Canvas.GetZIndex(item)),
           //                                      new XElement("IsGroup", item.IsGroup),
           //                                      new XElement("ParentID", item.ParentID),
           //                                      new XElement("Content", contentXaml)
           //                                  )
           //                       );
       }

       public void GetComparison()
       {
           if (weakReference.Target !=null)
           {
            
           }
       }

         private static bool IsInDesignMode(DependencyObject element)
        {
            // Due to a known issue in Cider, GetIsInDesignMode attached property value is not enough to know if it's in design mode.
            return DesignerProperties.GetIsInDesignMode(element) || Application.Current == null
                   || Application.Current.GetType() == typeof(Application);
        }


        private IEnumerable<string> dd = new List<string>();
        public void Test()
        {
            dd.OfType<string>();
        }

    }


    internal class WeakDelegatesManager
    {
        private readonly List<DelegateReference> listeners = new List<DelegateReference>();

        public void AddListener(Delegate listener)
        {
            this.listeners.Add(new DelegateReference(listener, false));
        }

        public void RemoveListener(Delegate listener)
        {
            this.listeners.RemoveAll(reference =>
            {
                //Remove the listener, and prune collected listeners
                Delegate target = reference.Target;
                return listener.Equals(target) || target == null;
            });
        }

        public void Raise(params object[] args)
        {
            this.listeners.RemoveAll(listener => listener.Target == null);

            foreach (Delegate handler in this.listeners.ToList().Select(listener => listener.Target).Where(listener => listener != null))
            {
                handler.DynamicInvoke(args);
            
            }
        }

        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("", "propertyExpression");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("", "propertyExpression");
            }

            var getMethod = property.GetGetMethod(true);
            if (getMethod.IsStatic)
            {
               
                throw new ArgumentException("", "propertyExpression");
            }

            return memberExpression.Member.Name;
        }
  
    }
    public class DelegateReference 
    {
        private readonly Delegate _delegate;
        private readonly WeakReference _weakReference;
        private readonly MethodInfo _method;
        private readonly Type _delegateType;

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateReference"/>.
        /// </summary>
        /// <param name="delegate">The original <see cref="Delegate"/> to create a reference for.</param>
        /// <param name="keepReferenceAlive">If <see langword="false" /> the class will create a weak reference to the delegate, allowing it to be garbage collected. Otherwise it will keep a strong reference to the target.</param>
        /// <exception cref="ArgumentNullException">If the passed <paramref name="delegate"/> is not assignable to <see cref="Delegate"/>.</exception>
        public DelegateReference(Delegate @delegate, bool keepReferenceAlive)
        {
            if (@delegate == null)
                throw new ArgumentNullException("delegate");

            if (keepReferenceAlive)
            {
                this._delegate = @delegate;
            }
            else
            {
                _weakReference = new WeakReference(@delegate.Target);
                _method = @delegate.Method;
                _delegateType = @delegate.GetType();
            }
        }

        /// <summary>
        /// Gets the <see cref="Delegate" /> (the target) referenced by the current <see cref="DelegateReference"/> object.
        /// </summary>
        /// <value><see langword="null"/> if the object referenced by the current <see cref="DelegateReference"/> object has been garbage collected; otherwise, a reference to the <see cref="Delegate"/> referenced by the current <see cref="DelegateReference"/> object.</value>
        public Delegate Target
        {
            get
            {
                if (_delegate != null)
                {
                    return _delegate;
                }
                else
                {
                    return TryGetDelegate();
                }
            }
        }

        private Delegate TryGetDelegate()
        {
            if (_method.IsStatic)
            {
                return Delegate.CreateDelegate(_delegateType, null, _method);
            }
            object target = _weakReference.Target;
            if (target != null)
            {
                return Delegate.CreateDelegate(_delegateType, target, _method);
            }
            return null;
        }
    }
}
