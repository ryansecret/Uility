// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PasswordBoxBindingHelper.cs" company="">
//   
// </copyright>
// <summary>
//   The password box binding helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uility.WPF.绑定代理
{
    using System;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// The password box binding helper.
    /// </summary>
    public static class PasswordBoxBindingHelper
    {
        #region Constants and Fields

        /// <summary>
        /// The binded password property.
        /// </summary>
        public static readonly DependencyProperty BindedPasswordProperty =
            DependencyProperty.RegisterAttached(
                "BindedPassword", 
                typeof(string), 
                typeof(PasswordBoxBindingHelper), 
                new UIPropertyMetadata(string.Empty, OnBindedPasswordChanged));

        /// <summary>
        /// The is password binding enabled property.
        /// </summary>
        public static readonly DependencyProperty IsPasswordBindingEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsPasswordBindingEnabled", 
                typeof(bool), 
                typeof(PasswordBoxBindingHelper), 
                new UIPropertyMetadata(false, OnIsPasswordBindingEnabledChanged));

        #endregion

        #region Public Methods

        /// <summary>
        /// The get binded password.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The get binded password.
        /// </returns>
        public static string GetBindedPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(BindedPasswordProperty);
        }

        /// <summary>
        /// The get is password binding enabled.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The get is password binding enabled.
        /// </returns>
        public static bool GetIsPasswordBindingEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPasswordBindingEnabledProperty);
        }

        /// <summary>
        /// The set binded password.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void SetBindedPassword(DependencyObject obj, string value)
        {
            obj.SetValue(BindedPasswordProperty, value);
        }

        /// <summary>
        /// The set is password binding enabled.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void SetIsPasswordBindingEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPasswordBindingEnabledProperty, value);
        }

        #endregion

        // when the buffer changed, upate the passwordBox's password
        #region Methods

        /// <summary>
        /// The on binded password changed.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void OnBindedPasswordChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = obj as PasswordBox;
            if (passwordBox != null)
            {
                passwordBox.Password = e.NewValue == null ? string.Empty : e.NewValue.ToString();
            }
        }

        /// <summary>
        /// The on is password binding enabled changed.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void OnIsPasswordBindingEnabledChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = obj as PasswordBox;

            if (passwordBox != null)
            {
                passwordBox.PasswordChanged -= PasswordBoxPasswordChanged;

                if ((bool)e.NewValue)
                {
                    passwordBox.PasswordChanged += PasswordBoxPasswordChanged;
                }
            }
        }

        // when the passwordBox's password changed, update the buffer
        /// <summary>
        /// The password box password changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;

            if (!string.Equals(GetBindedPassword(passwordBox), passwordBox.Password))
            {
                SetBindedPassword(passwordBox, passwordBox.Password);
                int length = passwordBox.Password.Length;
                SetPasswordBoxSelection(passwordBox, length, length);
            }
        }
         
        #endregion

        private static void SetPasswordBoxSelection(PasswordBox passwordBox, int start, int length)
        {
            var select = passwordBox.GetType().GetMethod("Select",
                            BindingFlags.Instance | BindingFlags.NonPublic);

            select.Invoke(passwordBox, new object[] { start, length });
        }


    }
}