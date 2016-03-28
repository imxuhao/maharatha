using System.ComponentModel.DataAnnotations;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetAccountInput : SearchInputDto
    {
        /// <summary> Gets or Sets the ChartOfAccountId to Search the Accounts with ChartOfAccountId </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid ChartOfAccount")]
        public int CoaId { get; set; }
       
    }
}