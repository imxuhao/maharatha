using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Configuration.ConnectionString.Dto
{
    [AutoMapTo(typeof(ConnectionStringUnit))]
    public class ConnectionStringInput : IInputDto
    {
        [StringLength(ConnectionStringUnit.MaxNameLength)]
        [Required]
        public string Name { get; set; }

        /// <summary>Gets or sets the Name field. </summary>
        [StringLength(ConnectionStringUnit.MaxConnectionStringLength)]
        [Required]
        public string ConnectionString { get; set; }
    }
}
