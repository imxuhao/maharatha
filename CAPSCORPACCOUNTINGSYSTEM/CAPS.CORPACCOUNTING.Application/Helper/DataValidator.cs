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

        public static void RequiredValidataion(string property, string columnName,UploadErrorMessagesOutputDto uploadErrorMessages)
        {
            if (string.IsNullOrEmpty(property))
            {
                uploadErrorMessages.ErrorMessage = uploadErrorMessages.ErrorMessage + ", " + columnName + " is Required";
            }
        }

    }
}
