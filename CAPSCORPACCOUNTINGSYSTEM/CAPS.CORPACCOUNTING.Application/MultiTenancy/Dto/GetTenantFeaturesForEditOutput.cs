using System.Collections.Generic;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Editions.Dto;

namespace CAPS.CORPACCOUNTING.MultiTenancy.Dto
{
    public class GetTenantFeaturesForEditOutput : IOutputDto
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}