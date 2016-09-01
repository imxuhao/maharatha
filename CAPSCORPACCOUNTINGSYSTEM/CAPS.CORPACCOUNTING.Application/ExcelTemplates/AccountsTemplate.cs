using CAPS.CORPACCOUNTING.DataExporting.Excel.EpPlus;
using CAPS.CORPACCOUNTING.Dto;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Accounts;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Masters.Dto;
using OfficeOpenXml.DataValidation;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using OfficeOpenXml;

namespace CAPS.CORPACCOUNTING.ExcelTemplates
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountsTemplate : EpPlusExcelExporterBase, ITemplate
    {

        private readonly IAccountUnitAppService _accountUnitAppService;
        private readonly IRepository<CoaUnit, int> _coaUnitRepository;
        private readonly int startRowIndex = 2;
        private readonly int endRowIndex = 50000;
        //private readonly int endRowIndex = ExcelPackage.MaxRows;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountUnitAppService"></param>
        /// <param name="coaUnitRepository"></param>
        public AccountsTemplate(
             AccountUnitAppService accountUnitAppService,
             IRepository<CoaUnit, int> coaUnitRepository
             )
        {
            _accountUnitAppService = accountUnitAppService;
            _coaUnitRepository = coaUnitRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<FileDto> DownLoadTemplate(int coaId)
        {
            //endRowIndex = ExcelPackage.MaxRows;
            //Get all List Information
            var classificationlist = await _accountUnitAppService.GetTypeOfAccountList();
            var currencylist = await _accountUnitAppService.GetTypeOfCurrencyList();
            var consolidationList = EnumList.GetTypeofConsolidationList();
            var typeOfCurrencyRateList = await _accountUnitAppService.GetTypeOfCurrencyRateList();
            var linkedAccountList = await _accountUnitAppService.GetLinkAccountListByCoaId(new AutoSearchInput() { Id = coaId });
            var booleanList = ExcelHelper.GetBooleanList();
            var accountNumberIsNumeric = (await _coaUnitRepository.GetAsync(coaId)).IsNumeric;

            return CreateExcelPackage(
                "AccountTemplate_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("AccountsTemplate"));
                    sheet.Protection.IsProtected = true;

                    var listDataSheet = excelPackage.Workbook.Worksheets.Add(L("DropDownListInformation"));
                    listDataSheet.Hidden = OfficeOpenXml.eWorkSheetHidden.Hidden;


                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, classificationlist, new KeyValuePair<string, string>(L("ClassificationNames"), L("ClassificationIds")), new KeyValuePair<string, string>("A", "B"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, currencylist, new KeyValuePair<string, string>(L("CurrencyNames"), L("CurrencyIds")), new KeyValuePair<string, string>("C", "D"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, consolidationList, new KeyValuePair<string, string>(L("ConsolidationNames"), L("ConsolidationIds")), new KeyValuePair<string, string>("E", "F"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, typeOfCurrencyRateList, new KeyValuePair<string, string>(L("RateTypeOverrideNames"), L("RateTypeOverrideIds")), new KeyValuePair<string, string>("G", "H"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, linkedAccountList, new KeyValuePair<string, string>(L("NewAccountNames"), L("NewAccountIds")), new KeyValuePair<string, string>("I", "J"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, booleanList, new KeyValuePair<string, string>(L("FlagsNames"), L("FlagsIds")), new KeyValuePair<string, string>("K", "L"));

                    //reference list columns to Excel Sheet
                    ExcelHelper.AddValidationtoSheet(L("AccountNumber"), sheet,
                        new Excelproperties
                        {
                            ExcelFormula = ExcelHelper.GetMultiValidationString(
                                new List<string>() {
                                    ExcelHelper.GetMaxLengthFormula("A2", AccountUnit.MaxAccountSize),
                                    ExcelHelper.GetAllowNumberFormula("A2",accountNumberIsNumeric) ,
                                    ExcelHelper.GetDuplicateCellFormula("A",startRowIndex,endRowIndex)
                                    }
                                       ),
                            ShowErrorMessage = true,
                            Error = ExcelHelper.ApplyPlaceHolderValues((accountNumberIsNumeric ? L("AllowNumbers") + ", " : "") + L("AllowDuplicateVaues") + ", "
                            + L("AllowMaxLength"), new Dictionary<string, string>() { { "{length}", AccountUnit.MaxAccountSize.ToString() },
                            { "{type}", "Charcters" }}),
                            ErrorTitle = L("ValidationMessage"),
                            ErrorStyle = ExcelDataValidationWarningStyle.stop
                        }
                            , startRowIndex, endRowIndex, 1, "A");

                    ExcelHelper.AddValidationtoSheet(L("Description"), sheet,
                      new Excelproperties
                      {
                          ExcelFormula = ExcelHelper.GetMultiValidationString(
                              new List<string>() {
                                    ExcelHelper.GetMaxLengthFormula("B2", AccountUnit.MaxDisplayNameLength),
                                ExcelHelper.GetDuplicateCellFormula("B",startRowIndex,endRowIndex)}),
                          ShowErrorMessage = true,
                          Error = ExcelHelper.ApplyPlaceHolderValues(L("AllowDuplicateVaues") + ", " +
                          L("AllowMaxLength"), new Dictionary<string, string>() { { "{length}", AccountUnit.MaxDisplayNameLength.ToString() },
                            { "{type}", "Charcters" }}),
                          ErrorTitle = L("ValidationMessage"),
                          ErrorStyle = ExcelDataValidationWarningStyle.stop
                      }
                      , startRowIndex, endRowIndex, 2, "B");

                    var excelDdlErrorMsgSettings = new Excelproperties
                    {
                        ShowErrorMessage = true,
                        Error = L("ExcelDropDownListErrorMsg"),
                        ErrorTitle = L("ValidationMessage"),
                        ErrorStyle = ExcelDataValidationWarningStyle.stop
                    };

                    var vLookUpFormulaSettings = new Excelproperties
                    {
                        ShowErrorMessage = false,
                        Error = "",
                        ErrorTitle = "",
                        ErrorStyle = ExcelDataValidationWarningStyle.undefined
                    };


                    ExcelHelper.AddDropDownValidationToSheet(L("Classification"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "A", 2, classificationlist.Count + 1), startRowIndex, endRowIndex, 3, "C", listcount: classificationlist.Count);

                    ExcelHelper.AddDropDownValidationToSheet(L("Consolidation"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "E", 2, consolidationList.Count + 1), startRowIndex, endRowIndex, 4, "D",listcount: 1);

                    ExcelHelper.AddDropDownValidationToSheet(L("Currency"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "C", 2, currencylist.Count + 1), startRowIndex, endRowIndex, 5, "E", listcount: 1);

                    ExcelHelper.AddDropDownValidationToSheet(L("NewAccount"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "I", 2, linkedAccountList.Count + 1), startRowIndex, endRowIndex, 6, "F", listcount: linkedAccountList.Count, hideColumn: true);

                    ExcelHelper.AddDropDownValidationToSheet(L("Multi-CurrencyReval"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "K", 2, booleanList.Count + 1), startRowIndex, endRowIndex, 7, "G", listcount: 1, hideColumn: true);

                    ExcelHelper.AddDropDownValidationToSheet(L("RateTypeOverride"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "G", 2, typeOfCurrencyRateList.Count + 1), startRowIndex, endRowIndex, 8, "H", listcount: typeOfCurrencyRateList.Count, hideColumn: true);

                    ExcelHelper.AddDropDownValidationToSheet(L("EliminationAccount"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "K", 2, booleanList.Count + 1), startRowIndex, endRowIndex, 9, "I", listcount: 1);

                    ExcelHelper.AddDropDownValidationToSheet(L("RollUpAccount"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "K", 2, booleanList.Count + 1), startRowIndex, endRowIndex, 10, "J", listcount: 1);

                    ExcelHelper.AddDropDownValidationToSheet(L("JournalsAllowed"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "K", 2, booleanList.Count + 1), startRowIndex, endRowIndex, 11, "K", listcount: 1);


                    #region dropdown list values add to sheet


                    ExcelHelper.AddFormulaToSheet(L("ClassificationValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "C2", new string[] { "A", "B" }, 2, classificationlist.Count + 1),
                    startRowIndex, endRowIndex, true, 14, "N");

                    ExcelHelper.AddFormulaToSheet(L("ConsolidationValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "D2", new string[] { "E", "F" }, 2, consolidationList.Count + 1),
                    startRowIndex, endRowIndex, true, 15, "O");


                    ExcelHelper.AddFormulaToSheet(L("CurrencyValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "E2", new string[] { "C", "D" }, 2, currencylist.Count + 1),
                    startRowIndex, endRowIndex, true, 16, "P");

                    ExcelHelper.AddFormulaToSheet(L("NewAccountValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "F2", new string[] { "I", "J" }, 2, linkedAccountList.Count + 1),
                    startRowIndex, endRowIndex, true, 17, "Q");


                    ExcelHelper.AddFormulaToSheet(L("RateTypeOverrideValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "H2", new string[] { "G", "H" }, 2, typeOfCurrencyRateList.Count + 1),
                    startRowIndex, endRowIndex, true, 18, "R");

                    #endregion

                });
        }
    }
}
