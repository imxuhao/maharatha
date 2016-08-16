using System;
using System.Reflection;
using CAPS.CORPACCOUNTING.Uploads.Dto;

namespace CAPS.CORPACCOUNTING.Helpers
{
    /// <summary>
    /// Validating Uploaded Data
    /// </summary>
    public class DataValidator
    {
        public static void CheckLength(int propertyLength, int maxLength, string columnName, UploadErrorMessagesOutputDto uploadErrorMessages)
        {
            if (propertyLength > maxLength)
            {
                uploadErrorMessages.ErrorMessage = uploadErrorMessages.ErrorMessage + " ," + columnName + "Length should be less than "+ maxLength;
            }

        }

        public static void RequiredValidataion<T>(T property, string columnName, UploadErrorMessagesOutputDto uploadErrorMessages)
        {
            Type t = property.GetType();
            switch (t.Name)
            {
                case "Int32":
                case "Int64":
                case "Int16":
                    if (Convert.ToInt16(property)==0)
                    {
                        uploadErrorMessages.ErrorMessage = uploadErrorMessages.ErrorMessage + ", " + columnName + " is Required";
                    }
                    break;
                case "String":
                    if (string.IsNullOrEmpty(property.ToString()))
                    {
                        uploadErrorMessages.ErrorMessage = uploadErrorMessages.ErrorMessage + ", " + columnName + " is Required";
                    }
                    break;
            }
        }

    }
}
