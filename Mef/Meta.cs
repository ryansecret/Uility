// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Meta.cs" company="">
//   
// </copyright>
// <summary>
//   定义元数据提供一些附加信息，便于筛选
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mef
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Microsoft.Windows.Controls;

    /// <summary>
    /// 定义元数据提供一些附加信息，便于筛选
    /// </summary>
    internal class Meta
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Meta"/> class.
        /// </summary>
        public Meta()
        {
            MessageBox.Show(this.Users.Value.UserName);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Users.
        /// </summary>
        [Import(typeof(Users))]
        public Lazy<Users, IMetadata> Users { get; set; }

        #endregion
    }

    /// <summary>
    /// The i metadata.
    /// </summary>
    public interface IMetadata
    {
        #region Public Properties

        /// <summary>
        /// Gets Name.
        /// </summary>
        string Name { get; }

        #endregion
    }

    /// <summary>
    /// The users.
    /// </summary>
    [ExportMetadata("Name", "李四")]
    [Export(typeof(Users))]
    public class Users
    {
        #region Constants and Fields

        /// <summary>
        /// The user name.
        /// </summary>
        public string UserName = "张三";

        #endregion
    }

    /// <summary>
    /// The i message sender.
    /// </summary>
    public interface IMessageSender
    {
        #region Public Methods

        /// <summary>
        /// The send.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void Send(string message);

        #endregion
    }

    /// <summary>
    /// The message sender attribute.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MessageSenderAttribute : ExportAttribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageSenderAttribute"/> class.
        /// </summary>
        public MessageSenderAttribute()
            : base(typeof(IMessageSender))
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether IsSecure.
        /// </summary>
        public bool IsSecure { get; set; }

        /// <summary>
        /// Gets or sets Transport.
        /// </summary>
        public MessageTransport Transport { get; set; }

        #endregion
    }

    /// <summary>
    /// The message transport.
    /// </summary>
    public enum MessageTransport
    {
        /// <summary>
        /// The undefined.
        /// </summary>
        Undefined, 

        /// <summary>
        /// The smtp.
        /// </summary>
        Smtp, 

        /// <summary>
        /// The phone network.
        /// </summary>
        PhoneNetwork, 

        /// <summary>
        /// The other.
        /// </summary>
        Other
    }

    /// <summary>
    /// The email sender.
    /// </summary>
    [MessageSender(Transport = MessageTransport.Smtp)]
    public class EmailSender : IMessageSender
    {
        #region Public Methods

        /// <summary>
        /// The send.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Send(string message)
        {
            Console.WriteLine(message);
        }

        #endregion
    }

    /// <summary>
    /// The secure email sender.
    /// </summary>
    [MessageSender(Transport = MessageTransport.Smtp, IsSecure = true)]
    public class SecureEmailSender : IMessageSender
    {
        #region Public Methods

        /// <summary>
        /// The send.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Send(string message)
        {
            Console.WriteLine(message);
        }

        #endregion
    }

    /// <summary>
    /// The sms sender.
    /// </summary>
    [MessageSender(Transport = MessageTransport.PhoneNetwork)]
    public class SMSSender : IMessageSender
    {
        #region Public Methods

        /// <summary>
        /// The send.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Send(string message)
        {
            Console.WriteLine(message);
        }

        #endregion
    }

    /// <summary>
    /// The i message sender capabilities.
    /// </summary>
    public interface IMessageSenderCapabilities
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether IsSecure.
        /// </summary>
        bool IsSecure { get; }

        /// <summary>
        /// Gets Transport.
        /// </summary>
        MessageTransport Transport { get; }

        #endregion
    }

    /// <summary>
    /// The http server health monitor.
    /// </summary>
    [Export]
    public partial class HttpServerHealthMonitor
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Senders.
        /// </summary>
        [ImportMany]
        public Lazy<IMessageSender, IMessageSenderCapabilities>[] Senders { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The send notification.
        /// </summary>
        public void SendNotification()
        {
            foreach (var sender in this.Senders)
            {
                if (sender.Metadata.Transport == MessageTransport.Smtp && sender.Metadata.IsSecure)
                {
                    var messageSender = sender.Value;
                    messageSender.Send("Server is fine");

                    break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// The http server health monitor new.
    /// </summary>
    [Export]
    public partial class HttpServerHealthMonitorNew
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Senders.
        /// </summary>
        [ImportMany]
        public Lazy<IMessageSender, IDictionary<string, object>>[] Senders { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The send notification.
        /// </summary>
        public void SendNotification()
        {
            foreach (var sender in this.Senders)
            {
                if (sender.Metadata.ContainsKey("Transport")
                     
                    && sender.Metadata.ContainsKey("Issecure") )
                {
                    var messageSender = sender.Value;
                    messageSender.Send("Server is fine");

                    break;
                }
            }
        }

        #endregion
    }
}