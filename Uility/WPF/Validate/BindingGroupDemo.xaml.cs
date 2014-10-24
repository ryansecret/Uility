using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Uility.WPF.Validate
{
    /// <summary>
    /// BindingGroupDemo.xaml 的交互逻辑
    /// </summary>
    public partial class BindingGroupDemo : UserControl
    {
        public BindingGroupDemo()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (stackPanel1.BindingGroup.CommitEdit())
            {
                MessageBox.Show("Item submitted");
                stackPanel1.BindingGroup.BeginEdit();
            }


        }

        // This event occurs when a ValidationRule in the BindingGroup
        // or in a Binding fails.
        private void ItemError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                MessageBox.Show(e.Error.ErrorContent.ToString());

            }
        }

        void stackPanel1_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the DataContext to a PurchaseItem object.
            // The BindingGroup and Binding objects use this as
            // the source.
            stackPanel1.DataContext = new PurchaseItem();

            // Begin an edit transaction that enables
            // the object to accept or roll back changes.
            stackPanel1.BindingGroup.BeginEdit();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Cancel the pending changes and begin a new edit transaction.
            stackPanel1.BindingGroup.CancelEdit();
            stackPanel1.BindingGroup.BeginEdit();
        }
    }
}

public class ValidateDateAndPrice : ValidationRule
{
    // Ensure that an item over $100 is available for at least 7 days.
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        BindingGroup bg = value as BindingGroup;

        // Get the source object.
        PurchaseItem item = bg.Items[0] as PurchaseItem;

        object doubleValue;
        object dateTimeValue;

        // Get the proposed values for Price and OfferExpires.
        bool priceResult = bg.TryGetValue(item, "Price", out doubleValue);
        bool dateResult = bg.TryGetValue(item, "OfferExpires", out dateTimeValue);

        if (!priceResult || !dateResult)
        {
            return new ValidationResult(false, "Properties not found");
        }

        double price = (double)doubleValue;
        DateTime offerExpires = (DateTime)dateTimeValue;

        // Check that an item over $100 is available for at least 7 days.
        if (price > 100)
        {
            if (offerExpires < DateTime.Today + new TimeSpan(7, 0, 0, 0))
            {
                return new ValidationResult(false, "Items over $100 must be available for at least 7 days.");
            }
        }

        return ValidationResult.ValidResult;

    }
}

public class PurchaseItem
{
    public double Price { get; set; }
    public double OfferExpires { get; set; }
}
