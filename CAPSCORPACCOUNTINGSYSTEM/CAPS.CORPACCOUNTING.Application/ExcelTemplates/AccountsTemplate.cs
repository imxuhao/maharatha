using CAPS.CORPACCOUNTING.DataExporting.Excel.EpPlus;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using CAPS.CORPACCOUNTING.Dto;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Accounts;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.ExcelTemplates
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountsTemplate : EpPlusExcelExporterBase, ITemplate
    {
        private readonly ListAppService _listAppService;
        private readonly IAccountUnitAppService _AccountUnitAppService;
        private readonly int startRowIndex = 2;
        private readonly int endRowIndex = 3000;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listAppService"></param>
        /// <param name="accountUnitAppService"></param>
        public AccountsTemplate(
             ListAppService listAppService,
             AccountUnitAppService accountUnitAppService)
        {
            _listAppService = listAppService;
            _AccountUnitAppService = accountUnitAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<FileDto> DownLoadTemplate()
        {

            var classificationlist = await _AccountUnitAppService.GetTypeOfAccountList();
            var currencylist = await _AccountUnitAppService.GetTypeOfCurrencyList();
            var consolidationList = EnumList.GetTypeofConsolidationList();
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
                            new ExcelFields(L("NewAccount"),iseditable:true),
                            new ExcelFields(L("Multi-CurrencyReval"),type:"dropdown",list:ExcelHelper.GetBooleanList()),
                            new ExcelFields(L("RateTypeOverride"),type:"dropdown",list:ExcelHelper.GetBooleanList()),
                            new ExcelFields(L("EliminationAccount"),type:"dropdown",list:ExcelHelper.GetBooleanList()),
                            new ExcelFields(L("RollUpAccount"),type:"dropdown",list:ExcelHelper.GetBooleanList()),
                            new ExcelFields(L("JournalsAllowed"),type:"dropdown",list:ExcelHelper.GetBooleanList())
                           }

                    );


                });
        }
    }
}
