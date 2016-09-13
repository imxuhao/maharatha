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
    /// <summary>
    /// 
    /// </summary>
    public class ProjectsTemplate : EpPlusExcelExporterBase, ITemplate
    {

        private readonly IJobUnitAppService _jobUnitAppService;
        private readonly IAccountUnitAppService _accountUnitAppService;

        private readonly int startRowIndex = 2;
        private readonly int endRowIndex = 50000;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobUnitAppService"></param>
        /// <param name="accountUnitAppService"></param>
        public ProjectsTemplate(IJobUnitAppService jobUnitAppService, IAccountUnitAppService accountUnitAppService)
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

            var rollupDivisionList = (await _jobUnitAppService.GetDivisionList(new AutoSearchInput() { Value = true })).ConvertAll(u => new NameValueDto()
            {
                Value = u.JobId.ToString(),
                Name = u.Caption
            });

            var budgetformatList = await (_jobUnitAppService.GetProjectCoaList(new AutoSearchInput() { }));
            var taxCreditList = (await _jobUnitAppService.GetTaxCreditList(new AutoSearchInput() { })).ConvertAll(u => new NameValueDto()
            {
                Value = u.Value,
                Name = u.Name
            });
            return CreateExcelPackage(
                "ProjectTemplate_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ProjectTemplate"));
                    sheet.Protection.IsProtected = true;
                    var listDataSheet = excelPackage.Workbook.Worksheets.Add(L("DropDownListInformation"));
                    listDataSheet.Hidden = OfficeOpenXml.eWorkSheetHidden.Hidden;

                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, projectTypelist, new KeyValuePair<string, string>(L("ProjectTypeNames"), L("ProjectTypeIds")), new KeyValuePair<string, string>("A", "B"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, statuslist, new KeyValuePair<string, string>(L("StatusNames"), L("StatusIds")), new KeyValuePair<string, string>("C", "D"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, currencylist, new KeyValuePair<string, string>(L("CurrencyNames"), L("CurrencyIds")), new KeyValuePair<string, string>("E", "F"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, rollupAccountList, new KeyValuePair<string, string>(L("RollUpAccountNames"), L("RollUpAccountIds")), new KeyValuePair<string, string>("G", "H"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, rollupDivisionList, new KeyValuePair<string, string>(L("RollUpDivisionNames"), L("RollUpDivisionIds")), new KeyValuePair<string, string>("I", "J"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, budgetformatList, new KeyValuePair<string, string>(L("BudgetFormatNames"), L("BudgetFormatIds")), new KeyValuePair<string, string>("K", "L"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, taxCreditList, new KeyValuePair<string, string>(L("TaxCreditNames"), L("TaxCreditIds")), new KeyValuePair<string, string>("M", "N"));


                    //reference list columns to Excel Sheet
                    ExcelHelper.AddValidationtoSheet(L("JobNumber"), sheet,
                        new Excelproperties
                        {
                            ExcelFormula = ExcelHelper.GetMultiValidationString(
                                new List<string>() {
                                    ExcelHelper.GetMaxLengthFormula("A2", JobUnit.MaxJobNumberLength),
                                    ExcelHelper.GetAllowNumberFormula("A2",jobNumberIsNumeric) ,
                                     ExcelHelper.GetDuplicateCellFormula("A",startRowIndex,endRowIndex)
                                    }
                                       ),
                            ShowErrorMessage = true,
                            Error = ExcelHelper.ApplyPlaceHolderValues(L("AllowDuplicateVaues") + ", " + (jobNumberIsNumeric ? L("AllowNumbers") + ", " : "") + L("AllowMaxLength"),
                            new Dictionary<string, string>() { { "{length}", JobUnit.MaxJobNumberLength.ToString() },
                            { "{type}", "Charcters" }}),
                            ErrorTitle = L("ValidationMessage"),
                            ErrorStyle = ExcelDataValidationWarningStyle.stop
                        }, startRowIndex, endRowIndex, 1, "A");

                    ExcelHelper.AddValidationtoSheet(L("JobName"), sheet,
                      new Excelproperties
                      {
                          ExcelFormula = ExcelHelper.GetMultiValidationString(
                              new List<string>() {
                                    ExcelHelper.GetMaxLengthFormula("B2", JobUnit.MaxCaptionLength),
                               ExcelHelper.GetDuplicateCellFormula("B",startRowIndex,endRowIndex)}),
                          ShowErrorMessage = true,
                          Error = ExcelHelper.ApplyPlaceHolderValues(L("AllowDuplicateVaues") + ", " + L("AllowMaxLength"), new Dictionary<string, string>() { { "{length}", JobUnit.MaxCaptionLength.ToString() },
                            { "{type}", "Charcters" }}),
                          ErrorTitle = L("ValidationMessage"),
                          ErrorStyle = ExcelDataValidationWarningStyle.stop
                      }, startRowIndex, endRowIndex, 2, "B");

                    var excelDdlErrorMsgSettings = new Excelproperties
                    {
                        ShowErrorMessage = true,
                        Error = L("ExcelDropDownListErrorMsg"),
                        ErrorTitle = L("ValidationMessage"),
                        ErrorStyle = ExcelDataValidationWarningStyle.stop
                    };

                    ExcelHelper.AddDropDownValidationToSheet(L("ProjectType"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "A", 2, projectTypelist.Count + 1), startRowIndex, endRowIndex, 3, "C", listcount: 1);

                    ExcelHelper.AddDropDownValidationToSheet(L("BudgetFormat"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "K", 2, budgetformatList.Count + 1), startRowIndex, endRowIndex, 4, "D", listcount: 1);

                    ExcelHelper.AddDropDownValidationToSheet(L("RollUpAccount"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "G", 2, rollupAccountList.Count + 1), startRowIndex, endRowIndex, 5, "E", listcount: rollupAccountList.Count);

                    ExcelHelper.AddDropDownValidationToSheet(L("RollUpDivision"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "I", 2, rollupDivisionList.Count + 1), startRowIndex, endRowIndex, 6, "F", listcount: rollupDivisionList.Count);

                    ExcelHelper.AddDropDownValidationToSheet(L("TaxCredit"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "M", 2, taxCreditList.Count + 1), startRowIndex, endRowIndex, 7, "G", listcount: taxCreditList.Count);

                    ExcelHelper.AddDropDownValidationToSheet(L("Currency"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "E", 2, currencylist.Count + 1), startRowIndex, endRowIndex, 8, "H", listcount: 1);

                    ExcelHelper.AddDropDownValidationToSheet(L("Status"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "C", 2, statuslist.Count + 1), startRowIndex, endRowIndex, 9, "I", listcount: statuslist.Count);



                    #region dropdown list values add to sheet
                    ExcelHelper.AddFormulaToSheet(L("ProjectTypeValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "C2", new string[] { "A", "B" }, 2, projectTypelist.Count + 1),
                    startRowIndex, endRowIndex, true, 12, "L", true);

                    ExcelHelper.AddFormulaToSheet(L("BudgetFormatValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "D2", new string[] { "K", "L" }, 2, budgetformatList.Count + 1),
                    startRowIndex, endRowIndex, true, 13, "M", locked: true);

                    ExcelHelper.AddFormulaToSheet(L("RollUpAccountValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "E2", new string[] { "G", "H" }, 2, rollupAccountList.Count + 1), startRowIndex, endRowIndex, true, 14, "N", locked: true);

                    ExcelHelper.AddFormulaToSheet(L("RollUpDivisionValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "F2", new string[] { "I", "J" }, 2, rollupDivisionList.Count + 1), startRowIndex, endRowIndex, true, 15, "O", locked: true);

                    ExcelHelper.AddFormulaToSheet(L("TaxCreditValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "G2", new string[] { "M", "N" }, 2, taxCreditList.Count + 1), startRowIndex, endRowIndex, true, 16, "P", locked: true);

                    ExcelHelper.AddFormulaToSheet(L("CurrencyValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "H2", new string[] { "E", "F" }, 2, currencylist.Count + 1), startRowIndex, endRowIndex, true, 17, "Q", locked: true);

                    ExcelHelper.AddFormulaToSheet(L("StatusValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "I2", new string[] { "C", "D" }, 2, statuslist.Count + 1), startRowIndex, endRowIndex, true, 18, "R", locked: true);
                    #endregion

                });
        }
    }
}
