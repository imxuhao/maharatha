
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    public class GetJobInput :IInputDto
    {
        /// <summary>
        /// Gets or sets JobId 
        /// </summary>
       public int JobId { get; set; }
    }
}