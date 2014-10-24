// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RowValidate.cs" company="">
//   
// </copyright>
// <summary>
//   The row validate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uility.WPF.Validate
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// The row validate.
    /// </summary>
    public class RowValidate : ValidationRule
    {
        #region Public Methods

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="cultureInfo">
        /// The culture info.
        /// </param>
        /// <returns>
        /// </returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
             
            var group = (BindingGroup)value;

            StringBuilder error = null;
            foreach (object item in group.Items)
            {
                // aggregate errors
                var info = item as IDataErrorInfo;
                if (info != null)
                {
                    if (!string.IsNullOrEmpty(info.Error))
                    {
                        if (error == null)
                        {
                            error = new StringBuilder();
                        }

                        error.Append((error.Length != 0 ? ", " : string.Empty) + info.Error);
                    }
                }
            }

            if (error != null)
            {
                return new ValidationResult(false, error.ToString());
            }

            return ValidationResult.ValidResult;
        }

        #endregion
    }
}