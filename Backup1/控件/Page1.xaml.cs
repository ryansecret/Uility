using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Windows.Interactivity;
namespace Silverlight.控件
{
    public partial class Page1 : Page
    {
       
        public Page1()
        {
            InitializeComponent();
            this.BindingValidationError += new EventHandler<ValidationErrorEventArgs>(Page1_BindingValidationError);
         
        }

        void Page1_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            
            Encoding myEncoding = Encoding.GetEncoding("gb2312");
            List<Customer> list = new List<Customer>();

            int[] odds = {1, 4, 5, 6};
            int[] evens = {4,5,7,9};

            var values =
                odds.SelectMany(even => evens, (oddnumber, even) => new {oddnumber, even}).Where(
                    pair => pair.oddnumber > pair.even);

            
            
        }

  
        
        // 当用户导航到此页面时执行。
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
        }



    }
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
