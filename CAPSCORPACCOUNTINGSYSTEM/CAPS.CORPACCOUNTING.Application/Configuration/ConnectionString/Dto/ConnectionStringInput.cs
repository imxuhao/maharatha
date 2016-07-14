using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters;
using Abp.AutoMapper;
using Abp.Runtime.Validation;

namespace CAPS.CORPACCOUNTING.Configuration.ConnectionString.Dto
{
    [AutoMapTo(typeof(ConnectionStringUnit))]
    public class ConnectionStringInput : IInputDto,ICustomValidate
    {
        [StringLength(ConnectionStringUnit.MaxNameLength)]
        [Required]
        public string Name { get; set; }
        ///// <summary>Gets or sets the Name field. </summary>
        //[StringLength(ConnectionStringUnit.MaxConnectionStringLength)]
        //[Required]
        //public string ConnectionString { get; set; }

        [Required]
        public string ServerName { get; set; }

        [Required]
        public string InstanceName { get; set; }
        [Required]
        public string Database { get; set; }
        [Required]
        public bool TrustedConnection { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public void AddValidationErrors(List<ValidationResult> results)
        {
            if (!TrustedConnection)
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    results.Add(new ValidationResult("Please provide UserName!"));
                }
                if (string.IsNullOrEmpty(Password))
                {
                    results.Add(new ValidationResult("Please provide Password!"));

                }
            }

        }
    }
}
