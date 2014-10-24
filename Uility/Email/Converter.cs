using System;

namespace Uility.Email
{
    public class Converter
    {
        public static T ConvertTo<T>(object input)
        {
            object result = default(T);
            if (input == null || input == DBNull.Value) return (T) result;

            if (typeof (T) == typeof (int))
                result = Convert.ToInt32(input);
            else if (typeof (T) == typeof (long))
                result = Convert.ToInt64(input);
            else if (typeof (T) == typeof (string))
                result = Convert.ToString(input);
            else if (typeof (T) == typeof (bool))
                result = Convert.ToBoolean(input);
            else if (typeof (T) == typeof (double))
                result = Convert.ToDouble(input);
            else if (typeof (T) == typeof (DateTime))
                result = Convert.ToDateTime(input);

            return (T) result;
        }
    }
}