using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Wpf.Extension
{
    public static class ArrayExtension
    {
        public static T[] CombineArray<T>(this T[] combineWith, T[] arrayToCombine)
        {
            if (combineWith != default(T[]) && arrayToCombine != default(T[]))
            {
                int initialSize = combineWith.Length;
                Array.Resize<T>(ref combineWith, initialSize + arrayToCombine.Length);
                Array.Copy(arrayToCombine, arrayToCombine.GetLowerBound(0), combineWith, initialSize, arrayToCombine.Length);
            }
            return combineWith;
        }

        public static T[] ClearAll<T>(this T[] arrayToClear)
        {
            if (arrayToClear != null)
                for (int i = arrayToClear.GetLowerBound(0); i <= arrayToClear.GetUpperBound(0); ++i)
                    arrayToClear[i] = default(T);
            return arrayToClear;
        }

        public static bool IsBetween<T>(this T value, T minValue, T maxValue, IComparer<T> comparer) where T : IComparable<T>
        {
            if (comparer == null)
                throw new ArgumentNullException("comparer");

            var minMaxCompare = comparer.Compare(minValue, maxValue);
            if (minMaxCompare < 0)
                return ((comparer.Compare(value, minValue) >= 0) && (comparer.Compare(value, maxValue) <= 0));
            //else if (minMaxCompare == 0)				// unnecessary  'else' below handles this case.
            //    return (comparer.Compare(value, minValue) == 0);
            else
                return ((comparer.Compare(value, maxValue) >= 0) && (comparer.Compare(value, minValue) <= 0));
        }


        public static T AutoInitialize<T>(this T source, object data, BindingFlags flags)
        {
            // Check if data is not null
            if (data == null) return source;
            // Check that data is the same type as source
            if (data.GetType() != source.GetType()) return source;

            // Get all the public - instace properties that contains both getter and setter.
            PropertyInfo[] properties = source.GetType().GetProperties(flags);
           
            // For each property, set the value to the source from the data.
            foreach (PropertyInfo property in properties)
            {
                
                // Identify the type of this property.
                Type propertyType = property.PropertyType;
                try
                {
                    // Retreive the value given the property name.
                    object objectValue = property.GetValue(data, null);
                    if (objectValue != null)
                    {
                        // If the object value is already of the property type
                        if (objectValue.GetType().Equals(propertyType))
                        {
                            // Set the object value to the source
                            property.SetValue(source, objectValue, null);
                        }
                        else
                        {
                            // Otherwise convert the object value using the property's converter
                            TypeConverter converter = TypeDescriptor.GetConverter(propertyType);
                            if (converter != null)
                            {
                                // Convert the object value.
                                object convertedData = converter.ConvertFrom(objectValue);
                                // Check that the converted data is of the same type as the property type
                                if (convertedData.GetType() == propertyType)
                                {
                                    // If it is, then set the converted data to the source object.
                                    property.SetValue(source, convertedData, null);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    // Exception during operations
                    Debug.WriteLine(e.Message);
                }
            }

            return source;
        }
    }
}
