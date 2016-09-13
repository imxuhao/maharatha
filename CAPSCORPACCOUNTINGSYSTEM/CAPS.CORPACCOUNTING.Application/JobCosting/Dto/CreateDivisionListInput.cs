using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    public class CreateDivisionListInput
    {
        public List<CreateJobUnitInput> DivisionList { get; set; }
    }
}
