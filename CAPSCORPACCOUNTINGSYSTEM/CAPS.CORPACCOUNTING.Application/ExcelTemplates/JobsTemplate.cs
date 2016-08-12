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
using System.Collections.Generic;
using OfficeOpenXml.DataValidation;

namespace CAPS.CORPACCOUNTING.ExcelTemplates
{
    public class JobsTemplate : EpPlusExcelExporterBase, ITemplate
    {

        private readonly IJobUnitAppService _jobUnitAppService;
        private readonly IAccountUnitAppService _accountUnitAppService;

        private readonly int startRowIndex = 2;
        private readonly int endRowIndex = 3000;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobUnitAppService"></param>
        /// <param name="accountUnitAppService"></param>
        public JobsTemplate(IJobUnitAppService jobUnitAppService, IAccountUnitAppService accountUnitAppService)
        {
            _jobUnitAppService = jobUnitAppService;
            _accountUnitAppService = accountUnitAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<FileDto> DownLoadTemplate(int coaId)
        {

            var projectTypelist = EnumList.GetProjectTypeList();
            var statuslist = EnumList.GetProjectStatusList();
            var currencylist = await _accountUnitAppService.GetTypeOfCurrencyList();
            var jobNumberIsNumeric = false;

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
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("JobTemplate"));
                    var listDataSheet = excelPackage.Workbook.Worksheets.Add(L("DropDownListInformation"));
                    listDataSheet.Hidden = OfficeOpenXml.eWorkSheetHidden.Hidden;

                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, projectTypelist, L("ProjectType"), "A");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, statuslist, L("Status"), "B");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, currencylist, L("Currency"), "C");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, rollupAccountList, L("RollUpAccount"), "D");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, rollupDivisionList, L("RollUpDivision"), "E");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, budgetformatList, L("BudgetFormat"), "F");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, taxCreditList, L("TaxCredit"), "G");

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


                    //reference list columns to Excel Sheet
                    ExcelHelper.AddMaxLengthValidationtoSheet(sheet,
                        new ExcelProperites
                        {
                            ExcelFormula = ExcelHelper.GetMultiValidationString(
                                new List<string>() {
                                    ExcelHelper.GetMaxLengthFormula("A2", AccountUnit.MaxAccountSize),
                                    ExcelHelper.GetAllowNumberFormula("A2",jobNumberIsNumeric) ,
                                    }
                                       ),
                            ShowErrorMessage = true,
                            Error = ExcelHelper.ApplyPlaceHolderValues((jobNumberIsNumeric ? L("AllowNumbers") + ", " : "") + L("AllowMaxLength"),
                            new Dictionary<string, string>() { { "{length}", JobUnit.MaxJobNumberLength.ToString() },
                            { "{type}", "Charcters" }}),
                            ErrorTitle = L("ValidationMessage"),
                            ErrorStyle = ExcelDataValidationWarningStyle.stop
                        }
                            , startRowIndex, endRowIndex, 1);

                    ExcelHelper.AddMaxLengthValidationtoSheet(sheet,
                      new ExcelProperites
                      {
                          ExcelFormula = ExcelHelper.GetMultiValidationString(
                              new List<string>() {
                                    ExcelHelper.GetMaxLengthFormula("B2", AccountUnit.MaxAccountSize) }),
                          ShowErrorMessage = true,
                          Error = ExcelHelper.ApplyPlaceHolderValues(L("AllowMaxLength"), new Dictionary<string, string>() { { "{length}", JobUnit.MaxCaptionLength.ToString() },
                            { "{type}", "Charcters" }}),
                          ErrorTitle = L("ValidationMessage"),
                          ErrorStyle = ExcelDataValidationWarningStyle.stop
                      }
                      , startRowIndex, endRowIndex, 2);
                    ExcelHelper.AddDropDownValidationToSheet(sheet, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "A", 2, projectTypelist.Count + 1), startRowIndex, endRowIndex, 3);
                    ExcelHelper.AddDropDownValidationToSheet(sheet, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "F", 2, budgetformatList.Count + 1), startRowIndex, endRowIndex, 4);

                    if (rollupAccountList.Count > 0)
                        ExcelHelper.AddDropDownValidationToSheet(sheet, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "D", 2, rollupAccountList.Count + 1), startRowIndex, endRowIndex, 5);

                    if (rollupDivisionList.Count > 0)
                        ExcelHelper.AddDropDownValidationToSheet(sheet, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "E", 2, rollupDivisionList.Count + 1), startRowIndex, endRowIndex, 6);

                    if (taxCreditList.Count > 0)
                        ExcelHelper.AddDropDownValidationToSheet(sheet, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "G", 2, taxCreditList.Count + 1), startRowIndex, endRowIndex, 7);
                    ExcelHelper.AddDropDownValidationToSheet(sheet, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "C", 2, currencylist.Count + 1), startRowIndex, endRowIndex, 8);

                    if (statuslist.Count > 0)
                        ExcelHelper.AddDropDownValidationToSheet(sheet, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "B", 2, statuslist.Count + 1), startRowIndex, endRowIndex, 9);




                });
        }
    }
}
