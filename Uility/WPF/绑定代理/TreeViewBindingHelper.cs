using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uility.WPF.绑定代理
{
    using System.Windows;
    using System.Windows.Controls;

    public  class TreeViewBindingHelper
    {

        #region IsWitty

        /// <summary>
        /// IsWitty Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty IsSelectedItemEnableProperty =
            DependencyProperty.RegisterAttached("IsSelectedItemEnable", typeof(bool), typeof(TreeViewBindingHelper),
                new UIPropertyMetadata((bool)false,
                    
                    new PropertyChangedCallback(OnIsWittyChanged)));

        /// <summary>
        /// Gets the IsWitty property. This dependency property 
        /// indicates ....
        /// </summary>
        public static bool GetIsSelectedItemEnable(DependencyObject d)
        {
            return (bool)d.GetValue(IsSelectedItemEnableProperty);
        }

        /// <summary>
        /// Sets the IsWitty property. This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetIsSelectedItemEnable(DependencyObject d, bool value)
        {
            d.SetValue(IsSelectedItemEnableProperty, value);
        }

        /// <summary>
        /// Handles changes to the IsWitty property.
        /// </summary>
        private static void OnIsWittyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TreeView tv = d as TreeView;
            if (tv!=null)
            {
                tv.SelectedItemChanged -= tv_SelectedItemChanged;
                if ((bool)e.NewValue)
                {
                    tv.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(tv_SelectedItemChanged);
                }
                
            }
        }

        static void tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
             TreeView tv = sender as TreeView;
             if (tv != null)
             {
                 SetSelectedItem(tv, tv.SelectedItem);
             }
        }

        #endregion

        #region IsDirty

        /// <summary>
        /// IsDirty Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.RegisterAttached("SelectedItem", typeof(object), typeof(TreeViewBindingHelper),
                new FrameworkPropertyMetadata(null,
                    new PropertyChangedCallback(OnIsDirtyChanged)));

        /// <summary>
        /// Gets the IsDirty property. This dependency property 
        /// indicates ....
        /// </summary>
        public static object GetSelectedItem(DependencyObject d)
        {
            return (object)d.GetValue(SelectedItemProperty);
        }

        /// <summary>
        /// Sets the IsDirty property. This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetSelectedItem(DependencyObject d, object value)
        {
            d.SetValue(SelectedItemProperty, value);
        }

        /// <summary>
        /// Handles changes to the IsDirty property.
        /// </summary>
        private static void OnIsDirtyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
             
        }

        #endregion

        
        
    }
}
