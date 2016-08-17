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

                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, classificationlist, L("Classification"), "A");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, currencylist, L("Currency"), "B");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, consolidationList, L("Consolidation"), "C");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, typeOfCurrencyRateList, L("RateTypeOverride"), "D");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, linkedAccountList, L("NewAccount"), "E");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, booleanList, L("Flags"), "F");

                    //Create Header Row
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


                    //reference list columns to Excel Sheet
                    ExcelHelper.AddValidationtoSheet(sheet,
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

                    ExcelHelper.AddValidationtoSheet(sheet,
                      new ExcelProperites
                      {
                          ExcelFormula = ExcelHelper.GetMultiValidationString(
                              new List<string>() {
                                    ExcelHelper.GetMaxLengthFormula("B2", AccountUnit.MaxDisplayNameLength) }),
                          ShowErrorMessage = true,
                          Error = ExcelHelper.ApplyPlaceHolderValues(L("AllowMaxLength"), new Dictionary<string, string>() { { "{length}", AccountUnit.MaxDisplayNameLength.ToString() },
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

                    if (classificationlist.Count > 0)
                        ExcelHelper.AddDropDownValidationToSheet(sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "A", 2, classificationlist.Count + 1), startRowIndex, endRowIndex, 3);

                    ExcelHelper.AddDropDownValidationToSheet(sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "C", 2, consolidationList.Count + 1), startRowIndex, endRowIndex, 4);
                    ExcelHelper.AddDropDownValidationToSheet(sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "B", 2, currencylist.Count + 1), startRowIndex, endRowIndex, 5);

                    if (linkedAccountList.Count > 0)
                        ExcelHelper.AddDropDownValidationToSheet(sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "E", 2, linkedAccountList.Count + 1), startRowIndex, endRowIndex, 6);

                    ExcelHelper.AddDropDownValidationToSheet(sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "F", 2, booleanList.Count + 1), startRowIndex, endRowIndex, 7);

                    if (typeOfCurrencyRateList.Count > 0)
                        ExcelHelper.AddDropDownValidationToSheet(sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "D", 2, typeOfCurrencyRateList.Count + 1), startRowIndex, endRowIndex, 8);
                    ExcelHelper.AddDropDownValidationToSheet(sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "F", 2, booleanList.Count + 1), startRowIndex, endRowIndex, 9);
                    ExcelHelper.AddDropDownValidationToSheet(sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "F", 2, booleanList.Count + 1), startRowIndex, endRowIndex, 10);
                    ExcelHelper.AddDropDownValidationToSheet(sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "F", 2, booleanList.Count + 1), startRowIndex, endRowIndex, 11);

                });
        }
    }
}
