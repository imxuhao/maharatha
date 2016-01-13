using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Editions.Dto;
using CAPS.CORPACCOUNTING.Web.Areas.Mpa.Models.Common;

namespace CAPS.CORPACCOUNTING.Web.Areas.Mpa.Models.Editions
{
    [AutoMapFrom(typeof(GetEditionForEditOutput))]
    public class CreateOrEditEditionModalViewModel : GetEditionForEditOutput, IFeatureEditViewModel
    {
        public bool IsEditMode
        {
            get { return Edition.Id.HasValue; }
        }

        public CreateOrEditEditionModalViewModel(GetEditionForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}