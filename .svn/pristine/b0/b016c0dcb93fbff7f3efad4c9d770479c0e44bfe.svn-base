// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserControl1.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   UserControl1.xaml 的交互逻辑
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uility
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        #region Constants and Fields

        /// <summary>
        /// The move page cursor.
        /// </summary>
        private static Cursor MovePageCursor =
            new Cursor(
                System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    "WpfDragAndDropSmorgasbord.Images.MovePage.cur"));

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserControl1"/> class.
        /// </summary>
        public UserControl1()
        {
            this.InitializeComponent();
            ComboBox comboBox = this.Template.FindName("PART_ComboBoxFilter", this) as ComboBox;
            ToolTip toolTip = Application.Current.Resources["dd"] as ToolTip;
            
        }

        #endregion
    }
}