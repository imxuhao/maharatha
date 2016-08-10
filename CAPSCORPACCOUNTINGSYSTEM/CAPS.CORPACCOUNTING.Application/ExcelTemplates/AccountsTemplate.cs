using CAPS.CORPACCOUNTING.DataExporting.Excel.EpPlus;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using CAPS.CORPACCOUNTING.Dto;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Accounts;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.ExcelTemplates
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountsTemplate : EpPlusExcelExporterBase, ITemplate{
        
        private readonly IAccountUnitAppService _accountUnitAppService;
        private readonly int startRowIndex = 2;
        private readonly int endRowIndex = 3000;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountUnitAppService"></param>
        public AccountsTemplate(
             AccountUnitAppService accountUnitAppService)
        {
           
            _accountUnitAppService = accountUnitAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<FileDto> DownLoadTemplate(int coaId)
        {

            var classificationlist = await _accountUnitAppService.GetTypeOfAccountList();
            var currencylist = await _accountUnitAppService.GetTypeOfCurrencyList();
            var consolidationList = EnumList.GetTypeofConsolidationList();
            var typeOfCurrencyRateList = await _accountUnitAppService.GetTypeOfCurrencyRateList();
            var linkedAccountList = await _accountUnitAppService.GetLinkAccountListByCoaId(new AutoSearchInput() { Id = coaId });
            return CreateExcelPackage(
                "AccountTemplate.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("AccountsTemplate"));
                    AddHeader(
                        sheet,
                    L("AccountNumber"),
                    L("Description"),
                    L("Classification"),
                    L("Consolidation"),
                    L("Currency"),
                    L("NewAccount"),
                    L("Multi-CurrencyReval"),
                    L("RateTypeOverride"),
                    L("EliminationAccount"),
                    L("RollUpAccount"),
                    L("JournalsAllowed")
                    );

                    ExcelHelper.AddTemplateObjects(
                            sheet, startRowIndex, endRowIndex,
                           new ExcelFields[]
                           {
                            new ExcelFields(L("AccountNumber"),iseditable:true),
                            new ExcelFields(L("Description"),iseditable:true),
                            new ExcelFields(L("Classification"),type:"dropdown",list:classificationlist),
                            new ExcelFields(L("Consolidation"),type:"dropdown",list:consolidationList),
                            new ExcelFields(L("Currency"),type:"dropdown",list:currencylist),
                            new ExcelFields(L("NewAccount"),iseditable:true,list:linkedAccountList),
                            new ExcelFields(L("Multi-CurrencyReval"),type:"dropdown",list:ExcelHelper.GetBooleanList()),
                            new ExcelFields(L("RateTypeOverride"),type:"dropdown",list:typeOfCurrencyRateList),
                            new ExcelFields(L("EliminationAccount"),type:"dropdown",list:ExcelHelper.GetBooleanList()),
                            new ExcelFields(L("RollUpAccount"),type:"dropdown",list:ExcelHelper.GetBooleanList()),
                            new ExcelFields(L("JournalsAllowed"),type:"dropdown",list:ExcelHelper.GetBooleanList())
                           }

                    );


                });
        }
    }
}
