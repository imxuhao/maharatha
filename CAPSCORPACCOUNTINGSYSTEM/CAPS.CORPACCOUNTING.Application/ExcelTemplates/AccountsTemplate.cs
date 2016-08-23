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
        private readonly int endRowIndex = 3000;

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
            //Get all List Information
            var classificationlist = await _accountUnitAppService.GetTypeOfAccountList();
            var currencylist = await _accountUnitAppService.GetTypeOfCurrencyList();
            var consolidationList = EnumList.GetTypeofConsolidationList();
            var typeOfCurrencyRateList = await _accountUnitAppService.GetTypeOfCurrencyRateList();
            var linkedAccountList = await _accountUnitAppService.GetLinkAccountListByCoaId(new AutoSearchInput() { Id = coaId });
            var booleanList = ExcelHelper.GetBooleanList();
            var accountNumberIsNumeric = (await _coaUnitRepository.GetAsync(coaId)).IsNumeric;

            return CreateExcelPackage(
                "AccountTemplate.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("AccountsTemplate"));
                    var listDataSheet = excelPackage.Workbook.Worksheets.Add(L("DropDownListInformation"));
                    listDataSheet.Hidden = OfficeOpenXml.eWorkSheetHidden.Hidden;


                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, classificationlist, new KeyValuePair<string, string>(L("Classification"), L("ClassificationValue")), new KeyValuePair<string, string>("A", "B"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, currencylist, new KeyValuePair<string, string>(L("Currency"), L("CurrencyValue")), new KeyValuePair<string, string>("C", "D"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, consolidationList, new KeyValuePair<string, string>(L("Consolidation"), L("ConsolidationValue")), new KeyValuePair<string, string>("E", "F"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, typeOfCurrencyRateList, new KeyValuePair<string, string>(L("RateTypeOverride"), L("RateTypeOverrideValue")), new KeyValuePair<string, string>("G", "H"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, linkedAccountList, new KeyValuePair<string, string>(L("NewAccount"), L("NewAccountValue")), new KeyValuePair<string, string>("I", "J"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, booleanList, new KeyValuePair<string, string>(L("Flags"), L("FlagsValue")), new KeyValuePair<string, string>("K", "L"));


                    //Create Header Row
                    AddHeader(
                        sheet,
                        L("AccountNumber"),
                        L("Description"),
                        L("Classification"),
                        L("ClassificationValue"),
                        L("Consolidation"),
                        L("ConsolidationValue"),
                        L("Currency"),
                        L("CurrencyValue"),
                        L("NewAccount"),
                        L("NewAccountValue"),
                        L("Multi-CurrencyReval"),
                        L("RateTypeOverride"),
                        L("RateTypeOverrideValue"),
                        L("EliminationAccount"),
                        L("RollUpAccount"),
                        L("JournalsAllowed")
                    );


                    //reference list columns to Excel Sheet
                    ExcelHelper.AddValidationtoSheet(L("AccountNumber"), sheet,
                        new ExcelProperites
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
                            , startRowIndex, endRowIndex, 1);

                    ExcelHelper.AddValidationtoSheet(L("Description"), sheet,
                      new ExcelProperites
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
                      , startRowIndex, endRowIndex, 2);

                    var excelDdlErrorMsgSettings = new ExcelProperites
                    {
                        ShowErrorMessage = true,
                        Error = L("AllowMaxLength"),
                        ErrorTitle = L("ValidationMessage"),
                        ErrorStyle = ExcelDataValidationWarningStyle.stop
                    };

                    var vLookUpFormulaSettings = new ExcelProperites
                    {
                        ShowErrorMessage = false,
                        Error = "",
                        ErrorTitle = "",
                        ErrorStyle = ExcelDataValidationWarningStyle.undefined
                    };

                    if (classificationlist.Count > 0)
                        ExcelHelper.AddDropDownValidationToSheet(L("Classification"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "A", 2, classificationlist.Count + 1), startRowIndex, endRowIndex, 3);
                    ExcelHelper.AddFormulaToSheet(L("ClassificationValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "C2", new string[] { "A", "B" }, 2, classificationlist.Count + 1),
                    startRowIndex, endRowIndex, true, 4);

                    ExcelHelper.AddDropDownValidationToSheet(L("Consolidation"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "E", 2, consolidationList.Count + 1), startRowIndex, endRowIndex, 5);
                    ExcelHelper.AddFormulaToSheet(L("ConsolidationValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "E2", new string[] { "E", "F" }, 2, consolidationList.Count + 1),
                    startRowIndex, endRowIndex, true, 6);


                    ExcelHelper.AddDropDownValidationToSheet(L("Currency"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "C", 2, currencylist.Count + 1), startRowIndex, endRowIndex, 7);
                    ExcelHelper.AddFormulaToSheet(L("CurrencyValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "G2", new string[] { "C", "D" }, 2, currencylist.Count + 1),
                    startRowIndex, endRowIndex, true, 8);

                    if (linkedAccountList.Count > 0)
                        ExcelHelper.AddDropDownValidationToSheet(L("NewAccount"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "I", 2, linkedAccountList.Count + 1), startRowIndex, endRowIndex, 9);
                    ExcelHelper.AddFormulaToSheet(L("NewAccountValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "I2", new string[] { "I", "J" }, 2, linkedAccountList.Count + 1),
                    startRowIndex, endRowIndex, true, 10);


                    ExcelHelper.AddDropDownValidationToSheet(L("Multi-CurrencyReval"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "K", 2, booleanList.Count + 1), startRowIndex, endRowIndex, 11);

                    if (typeOfCurrencyRateList.Count > 0)
                        ExcelHelper.AddDropDownValidationToSheet(L("RateTypeOverride"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "G", 2, typeOfCurrencyRateList.Count + 1), startRowIndex, endRowIndex, 12);
                    ExcelHelper.AddFormulaToSheet(L("RateTypeOverrideValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "L2", new string[] { "G", "H" }, 2, typeOfCurrencyRateList.Count + 1),
                    startRowIndex, endRowIndex, true, 13);

                    ExcelHelper.AddDropDownValidationToSheet(L("EliminationAccount"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "K", 2, booleanList.Count + 1), startRowIndex, endRowIndex, 14);
                    ExcelHelper.AddDropDownValidationToSheet(L("RollUpAccount"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "K", 2, booleanList.Count + 1), startRowIndex, endRowIndex, 15);
                    ExcelHelper.AddDropDownValidationToSheet(L("JournalsAllowed"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "K", 2, booleanList.Count + 1), startRowIndex, endRowIndex, 16);

                });
        }
    }
}
