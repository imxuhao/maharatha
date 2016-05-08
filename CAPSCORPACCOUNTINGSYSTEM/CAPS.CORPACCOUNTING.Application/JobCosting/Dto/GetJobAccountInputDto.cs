using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    public class GetJobAccountInputDto : IInputDto
    {
        /// <summary>Gets or sets the AccountId field. </summary>
        public int ChartofAccountId { get; set; }

        /// <summary>Gets or sets the Job field. </summary>
        public int JobId { get; set; }
    }
}
