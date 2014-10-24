 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComLib.Notifications
{
    /// <summary>
    /// Notification service
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Notification settings.
        /// </summary>
        NotificationSettings Settings { get; set; }


        /// <summary>
        /// The account services.
        /// </summary>
        INotificationAccountService AccountService { get; }


        /// <summary>
        /// Messageing services.
        /// </summary>
        INotificationMessagingService MessageService { get; }
    }
    


    /// <summary>
    /// Notification services for account based messages.
    /// </summary>
    public interface INotificationAccountService
    {
        /// <summary>
        /// Notification settings.
        /// </summary>
        NotificationSettings Settings { get; set; }


        /// <summary>
        /// Send welcome email to new user.
        /// </summary>
        /// <param name="ctx">Instance of notification context.</param>
        void WelcomeNewUser(NotificationContext ctx);


        /// <summary>
        /// Send welcome email to new user.
        /// </summary>
        /// <param name="to">E-mail to.</param>
        /// <param name="subject">E-mail subject.</param>
        /// <param name="firstname">First name of user.</param>
        /// <param name="username">Username of user.</param>
        /// <param name="password">Password of user.</param>
        void WelcomeNewUser(string to, string subject, string firstname, string username, string password);


        /// <summary>
        /// Send message to remind user of password.
        /// </summary>
        /// <param name="ctx">Instance of notification context.</param>
        void RemindUserPassword(NotificationContext ctx);


        /// <summary>
        /// Send remind account/password email to user.
        /// </summary>
        /// <param name="to">E-mail to.</param>
        /// <param name="subject">E-mail subject.</param>
        /// <param name="firstname">First name of user.</param>
        /// <param name="username">Username of user.</param>
        /// <param name="password">Password of user.</param>
        void RemindUserPassword(string to, string subject, string firstname, string username, string password);
    }



    /// <summary>
    /// Notification services for other non-account related messaging.
    /// </summary>
    public interface INotificationMessagingService
    {
        /// <summary>
        /// Notification settings.
        /// </summary>
        NotificationSettings Settings { get; set; }


        /// <summary>
        /// Send the website url to a friend.
        /// </summary>
        /// <param name="ctx">Instance of notification context.</param>
        void SendToFriend(NotificationContext ctx);


        /// <summary>
        /// Send the website url to a friend. 
        /// </summary>
        /// <param name="toEmail">E-mail to.</param>
        /// <param name="subject">E-mail subject.</param>
        /// <param name="friendName">Friend's name.</param>
        /// <param name="fromName">From name.</param>
        /// <param name="messageFromFriend">Message text.</param>
        void SendToFriend(string toEmail, string subject, string friendName, string fromName, string messageFromFriend);


        /// <summary>
        /// Send a post to a friend.
        /// </summary>
        /// <param name="ctx">Instance of notification context.</param>
        void SendToFriendPost(NotificationContext ctx);


        /// <summary>
        /// Send a post to a friend.
        /// </summary>
        /// <param name="toEmail">E-mail to.</param>
        /// <param name="subject">E-mail subject.</param>
        /// <param name="toFirstname">Name of recipient.</param>
        /// <param name="fromName">Name of sender.</param>
        /// <param name="messageToFriend">Message text.</param>
        /// <param name="postTitle">Title of post.</param>
        /// <param name="postUrl">Title of url.</param>
        void SendToFriendPost(string toEmail, string subject, string toFirstname, string fromName, string messageToFriend, string postTitle, string postUrl);


        /// <summary>
        /// User feedback.
        /// </summary>
        /// <param name="ctx">Instance of notification context.</param>
        void SubmitFeedBack(NotificationContext ctx);
    }
}
