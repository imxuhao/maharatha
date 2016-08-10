using CAPS.CORPACCOUNTING.DataExporting.Excel.EpPlus;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using CAPS.CORPACCOUNTING.Dto;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Accounts;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.JobCosting;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.ExcelTemplates
{
    public class JobsTemplate : EpPlusExcelExporterBase, ITemplate
    {
        private readonly ListAppService _listAppService;
        private readonly IAccountUnitAppService _AccountUnitAppService;
        private readonly IJobUnitAppService _jobUnitAppService;

        private readonly int startRowIndex = 2;
        private readonly int endRowIndex = 3000;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeZoneConverter"></param>
        /// <param name="abpSession"></param>
        /// <param name="listAppService"></param>
        /// <param name="accountUnitAppService"></param>
        public JobsTemplate(
             ListAppService listAppService,
             AccountUnitAppService accountUnitAppService,
              IJobUnitAppService jobUnitAppService


              )
        {
            _listAppService = listAppService;
            _AccountUnitAppService = accountUnitAppService;
            _jobUnitAppService = jobUnitAppService;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<FileDto> DownLoadTemplate()
        {

            var projectTypelist = EnumList.GetProjectTypeList();
            var statuslist = EnumList.GetProjectStatusList();
            var currencylist = await _AccountUnitAppService.GetTypeOfCurrencyList();
            var consolidationList = EnumList.GetTypeofConsolidationList();
            var rollupAccountList = (await _jobUnitAppService.GetRollupAccountList(new AutoSearchInput() { Value = false })).ConvertAll(u => new NameValueDto()
            {
                Value = u.AccountId.ToString(),
                Name = u.AccountNumber
            });

            var rollupDivisionList = (await _jobUnitAppService.GetRollupAccountList(new AutoSearchInput() { Value = true })).ConvertAll(u => new NameValueDto()
            {
                Value = u.AccountId.ToString(),
                Name = u.AccountNumber
            });

            var budgetformatList = await (_jobUnitAppService.GetProjectCoaList(new AutoSearchInput() { }));
            var taxCreditList = (await _jobUnitAppService.GetTaxCreditList(new AutoSearchInput() { })).ConvertAll(u => new NameValueDto()
            {
                Value = u.Value,
                Name = u.Name
            }); 

            return CreateExcelPackage(
                "JobTemplate.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("AccountsTemplate"));
                    AddHeader(
                        sheet,
                    L("JobNumber"),
                    L("JobName"),
                    L("ProjectType"),
                    L("BudgetFormat"),
                    L("RollUpAccount"),
                    L("RollUpDivision"),
                    L("TaxCredit"),
                    L("Currency"),
                    L("Status")
                    );

                    ExcelHelper.AddTemplateObjects(
                            sheet, startRowIndex, endRowIndex,
                           new ExcelFields[]
                           {
                            new ExcelFields(L("JobNumber"),iseditable:true),
                            new ExcelFields(L("JobName"),iseditable:true),
                            new ExcelFields(L("ProjectType"),type:"dropdown",list:projectTypelist),
                            new ExcelFields(L("BudgetFormat"),type:"dropdown",list:budgetformatList),
                            new ExcelFields(L("RollUpAccount"),type:"dropdown",list:rollupAccountList),
                            new ExcelFields(L("RollUpDivision"),type:"dropdown",list:rollupDivisionList),
                            new ExcelFields(L("TaxCredit"),type:"dropdown",list:taxCreditList),
                            new ExcelFields(L("Currency"),type:"dropdown",list:currencylist),
                            new ExcelFields(L("Status"),type:"dropdown",list:statuslist)
                           }

                    );


                });
        }
    }
}
