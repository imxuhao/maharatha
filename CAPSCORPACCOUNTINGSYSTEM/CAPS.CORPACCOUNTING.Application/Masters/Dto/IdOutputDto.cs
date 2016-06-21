using Abp.Application.Services.Dto;
using Abp.Dependency;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    /// <summary>
    /// This dto contains ID which is for sending ID as output for Service
    /// </summary>
    public class IdOutputDto<T> :IOutputDto
    {
        public T Id { get; set; }
        /// <summary>
        /// Gets or sets AccountId
        /// </summary>
        public T AccountId { get; set; }

        /// <summary>
        /// Gets or sets JobId
        /// </summary>
        public T JobId { get; set; }
    }
}
