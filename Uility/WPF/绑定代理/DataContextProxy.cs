// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataContextProxy.cs" company="">
//   
// </copyright>
// <summary>
//   The data context proxy.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uility.WPF.绑定代理
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// The data context proxy.
    /// </summary>
    public class DataContextProxy : FrameworkElement
    {
        #region Constants and Fields

        /// <summary>
        /// The data source property.
        /// </summary>
        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register(
            "DataSource", typeof(Object), typeof(DataContextProxy), null);

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextProxy"/> class.
        /// </summary>
        public DataContextProxy()
        {
            this.Loaded += new RoutedEventHandler(this.DataContextProxy_Loaded);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets BindingMode.
        /// </summary>
        public BindingMode BindingMode { get; set; }

        /// <summary>
        /// Gets or sets BindingPropertyName.
        /// </summary>
        public string BindingPropertyName { get; set; }

        /// <summary>
        /// Gets or sets DataSource.
        /// </summary>
        public object DataSource
        {
            get
            {
                return (Object)this.GetValue(DataSourceProperty);
            }

            set
            {
                this.SetValue(DataSourceProperty, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The data context proxy_ loaded.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void DataContextProxy_Loaded(object sender, RoutedEventArgs e)
        {
            Binding binding = new Binding();

            if (!string.IsNullOrEmpty(this.BindingPropertyName))
            {
                binding.Path = new PropertyPath(this.BindingPropertyName);
            }

            binding.Source = this.DataContext;

            binding.Mode = this.BindingMode;

            this.SetBinding(DataContextProxy.DataSourceProperty, binding);
        }

        #endregion
    }
    

}