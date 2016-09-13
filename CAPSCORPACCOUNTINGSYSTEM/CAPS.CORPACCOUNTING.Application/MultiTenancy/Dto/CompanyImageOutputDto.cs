using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.MultiTenancy.Dto
{
    public class CompanyImageOutputDto
    {
        public string CompanyLogo { get; set; }
        public Guid? CompanyLogoId { get; set; }

        public long AddressId { get; set; }

        public int TenantExtendedId { get; set; }

    }
}
