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
    public class DivisionTemplate : EpPlusExcelExporterBase, ITemplate
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
        public DivisionTemplate(IJobUnitAppService jobUnitAppService, IAccountUnitAppService accountUnitAppService)
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
            var currencylist = await _accountUnitAppService.GetTypeOfCurrencyList();
            var jobNumberIsNumeric = false;
            var booleanList = ExcelHelper.GetBooleanList();

            return CreateExcelPackage(
                "DivisionTemplate_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("DivisionTemplate"));
                    sheet.Protection.IsProtected = true;
                    var listDataSheet = excelPackage.Workbook.Worksheets.Add(L("DropDownListInformation"));
                    listDataSheet.Hidden = OfficeOpenXml.eWorkSheetHidden.Hidden;

                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, currencylist, new KeyValuePair<string, string>(L("CurrencyNames"), L("CurrencyIds")), new KeyValuePair<string, string>("C", "D"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, booleanList, new KeyValuePair<string, string>(L("FlagsNames"), L("FlagsIds")), new KeyValuePair<string, string>("E", "F"));


                    //reference list columns to Excel Sheet
                    ExcelHelper.AddValidationtoSheet(L("Number"),sheet,
                        new Excelproperties
                        {
                            ExcelFormula = ExcelHelper.GetMultiValidationString(
                                new List<string>() {
                                    ExcelHelper.GetMaxLengthFormula("A2",  JobUnit.MaxJobNumberLength),
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
                        }
                            , startRowIndex, endRowIndex, 1,"A");

                    ExcelHelper.AddValidationtoSheet(L("Description"), sheet,
                      new Excelproperties
                      {
                          ExcelFormula = ExcelHelper.GetMultiValidationString(
                              new List<string>() {
                                    ExcelHelper.GetMaxLengthFormula("B2",JobUnit.MaxCaptionLength),
                               ExcelHelper.GetDuplicateCellFormula("B",startRowIndex,endRowIndex)}),
                          ShowErrorMessage = true,
                          Error = ExcelHelper.ApplyPlaceHolderValues(L("AllowDuplicateVaues") + ", " + L("AllowMaxLength"), new Dictionary<string, string>() { { "{length}", JobUnit.MaxCaptionLength.ToString() },
                            { "{type}", "Charcters" }}),
                          ErrorTitle = L("ValidationMessage"),
                          ErrorStyle = ExcelDataValidationWarningStyle.stop
                      }
                      , startRowIndex, endRowIndex, 2,"B");

                    var excelDdlErrorMsgSettings = new Excelproperties
                    {
                        ShowErrorMessage = true,
                        Error = L("ExcelDropDownListErrorMsg"),
                        ErrorTitle = L("ValidationMessage"),
                        ErrorStyle = ExcelDataValidationWarningStyle.stop
                    };

                        ExcelHelper.AddDropDownValidationToSheet(L("Currency"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "C", 2, currencylist.Count + 1), startRowIndex, endRowIndex, 3,"C",listcount:1);
                       
                        ExcelHelper.AddDropDownValidationToSheet(L("Active"),sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "E", 2, booleanList.Count + 1), startRowIndex, endRowIndex, 4,"D",listcount:1);

                    #region dropdown list values add to sheet
                    ExcelHelper.AddFormulaToSheet(L("CurrencyValue"), sheet,
                       ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "C2", new string[] { "C", "D" }, 2, currencylist.Count + 1), startRowIndex, endRowIndex, true, 7,"G",locked:true);
                    #endregion
                });
        }
    }
}
