using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Uploads.Dto;

namespace CAPS.CORPACCOUNTING.Helpers
{
    /// <summary>
    /// Validating Uploaded Data
    /// </summary>
    public class DataValidator
    {
        public static void CheckLength(int propertyLength, int maxLength, string columnName, List<NameValueDto> uploadErrorMessages)
        {
        
            if (propertyLength > maxLength)
            {
                uploadErrorMessages.Add(new NameValueDto
                {
                    Name = columnName,
                    Value = "Length should be less than " + maxLength
                }); 
            }

        }

        public static void RequiredValidataion<T>(T property, string columnName, List<NameValueDto> uploadErrorMessages)
        {
            Type t = property.GetType();
            switch (t.Name)
            {
                case "Int32":
                case "Int64":
                case "Int16":
                    if (Convert.ToInt16(property)==0)
                    {
                        uploadErrorMessages.Add(new NameValueDto
                        {
                            Name = columnName,
                            Value = columnName + " is Required"
                        });
                    }
                    break;
                case "String":
                    if (string.IsNullOrEmpty(property.ToString()))
                    {
                        uploadErrorMessages.Add(new NameValueDto
                        {
                            Name = columnName,
                            Value = columnName + " is Required"
                        });
                    }
                    break;
            }
            
        }

    }
}
