using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters;
using Abp.AutoMapper;
using Abp.Runtime.Validation;

namespace CAPS.CORPACCOUNTING.Configuration.ConnectionString.Dto
{
    [AutoMapTo(typeof(ConnectionStringUnit))]
    public class ConnectionStringInput : IInputDto
    {
        [StringLength(ConnectionStringUnit.MaxNameLength)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets ConnectionString
        /// The ConnectionString format shold be like "Server=XXX; Data Source=XXX; Initial Catalog=XXX; Persist Security Info=True;"OR
        /// "Server=XXX; Data Source=XXX; Initial Catalog=XXX; Persist Security Info=False; User ID=xxx;Password=xxx ;"
        /// 
        /// </summary>
        [Required]
        public string ConnectionString { get; set; }

        //[Required]
        //public string ServerName { get; set; }
        //public string InstanceName { get; set; }
        //[Required]
        //public string Database { get; set; }
        //[Required]
        //public bool TrustedConnection { get; set; }

        //public string UserName { get; set; }
        //public string Password { get; set; }

        //public void AddValidationErrors(List<ValidationResult> results)
        //{
        //    if (!TrustedConnection)
        //    {
        //        if (string.IsNullOrEmpty(UserName))
        //        {
        //            results.Add(new ValidationResult("Please provide UserName!"));
        //        }
        //        if (string.IsNullOrEmpty(Password))
        //        {
        //            results.Add(new ValidationResult("Please provide Password!"));

        //        }
        //    }

        //}
    }
}
