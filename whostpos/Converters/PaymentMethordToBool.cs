using System;
using System.Globalization;
using System.Windows.Data;

namespace whostpos.Converters
{
    /// <summary>
    /// This Class is for convert Payment method to Bool
    /// </summary>
    public class PaymentMethordToBool : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null)
            {
                string checkValue = value.ToString();
                string targetValue = parameter.ToString();
                if (checkValue.Equals(targetValue, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }

                return false;
            }
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}