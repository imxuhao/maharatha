using System;


namespace CAPS.CORPACCOUNTING.Helpers
{
    /// <summary>
    /// Validating Uploaded Data
    /// </summary>
    public class DataValidator
    {

        public static string CheckLength(int propertyLength, int maxLength, string columnName)
        {
            if (propertyLength > maxLength)
                return columnName + " Length should be less than " + maxLength;
            return string.Empty;
        }

        /// <summary>
        /// Required field validation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string RequiredValidataion<T>(T property, string columnName)
        {
            Type t = property.GetType();
            string errorMessage = string.Empty;
            switch (t.Name)
            {
                case "Int32":
                case "Int64":
                case "Int16":
                    if (Convert.ToInt16(property) == 0)
                        errorMessage = columnName + " is Required";
                    break;
                case "String":
                    if (string.IsNullOrEmpty(property.ToString()))
                        errorMessage = columnName + " is Required";
                    break;
            }
            return errorMessage;


        }

    }
}
