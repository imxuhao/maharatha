using System.Collections.Generic;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Editions.Dto;

namespace CAPS.CORPACCOUNTING.Web.Areas.Mpa.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}