//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace Silverlight.Validate
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
  
    /// <summary>
    /// Defines a <see cref="DependencyProperty"/> where all <see cref="ValidationError"/> are stored to be used from XAML on <see cref="ToolTip"/>.
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// Collection of <see cref="ValidationError"/> ocurred on the attached element.
        /// </summary>
        public static readonly DependencyProperty ErrorsProperty = DependencyProperty.RegisterAttached(
                "Errors", typeof(ObservableCollection<ValidationError>), typeof(Validation), null);

        /// <summary>
        /// Gets the value of <see cref="ErrorsProperty"/> on <paramref name="dependencyObject"/>.
        /// </summary>
        /// <param name="dependencyObject">Element on which the <see cref="ErrorsProperty"/> property is attached.</param>
        /// <returns>Value of <see cref="ErrorsProperty"/>.</returns>
        public static ObservableCollection<ValidationError> GetErrors(DependencyObject dependencyObject)
        {
            return DependencyPropertyHelper.GetOrAddValue<ObservableCollection<ValidationError>>(dependencyObject, ErrorsProperty);
        }
    }

    /// <summary>
    /// 行为可以替代事件命令
    /// </summary>
    public class UpdateTextBindingOnPropertyChanged : Behavior<TextBox>
    {
        private BindingExpression expression;


        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
              
            base.OnAttached();

            this.expression = this.AssociatedObject.GetBindingExpression(TextBox.TextProperty);
            this.AssociatedObject.TextChanged += this.OnTextChanged;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.TextChanged -= this.OnTextChanged;
            this.expression = null;
        }

        private void OnTextChanged(object sender, EventArgs args)
        {
            this.expression.UpdateSource();
        }
    }

//    <TextBox Style="{StaticResource textBoxInError}">
//    <TextBox.Text>
//        <!--By setting ValidatesOnExceptions to True, it checks for exceptions
//        that are thrown during the update of the source property.
//        An alternative syntax is to add <ExceptionValidationRule/> within
//        the <Binding.ValidationRules> section.-->
//        <Binding Path="Age" Source="{StaticResource data}"
//                 ValidatesOnExceptions="True"
//                 UpdateSourceTrigger="PropertyChanged">
//            <Binding.ValidationRules>
//                <!--DataErrorValidationRule checks for validation 
//                    errors raised by the IDataErrorInfo object.-->
//                <!--Alternatively, you can set ValidationOnDataErrors="True" on the Binding.-->
//                <DataErrorValidationRule/>
//            </Binding.ValidationRules>
//        </Binding>
//    </TextBox.Text>
//</TextBox>

    public class Customer
    {
        // Private data members.
        private int m_IdNumber;
        private string m_FirstName;
        private string m_LastName;

        public Customer(string firstName, string lastName, int id)
        {
            this.IdNumber = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        // Public properties.
        [Display(Name = "ID Number", Description = "Enter an integer between 0 and 99999.")]
        [Range(0, 99999)]
        public int IdNumber
        {
            get { return m_IdNumber; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "IdNumber" });
                m_IdNumber = value;
            }
        }

        [Display(Name = "Name", Description = "First Name + Last Name.")]
        [Required(ErrorMessage = "First Name is required.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Numbers and special characters are not allowed in the name.")]
        public string FirstName
        {
            get { return m_FirstName; }

            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "FirstName" });
                m_FirstName = value;
            }
        }

        [Required(ErrorMessage = "Last Name is required.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Numbers and special characters are not allowed in the name.")]
        [StringLength(8, MinimumLength = 3, ErrorMessage =
            "Last name must be between 3 and 8 characters long.")]
        public string LastName
        {
            get { return m_LastName; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "LastName" });
                m_LastName = value;
            }
        }
    }
}