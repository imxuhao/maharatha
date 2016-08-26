using CAPS.CORPACCOUNTING.DataExporting.Excel.EpPlus;
using CAPS.CORPACCOUNTING.Dto;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Accounts;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Collections.Generic;
using OfficeOpenXml.DataValidation;
using CAPS.CORPACCOUNTING.JobCosting;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using OfficeOpenXml;

namespace CAPS.CORPACCOUNTING.ExcelTemplates
{
    public class LinesTemplate : EpPlusExcelExporterBase, ITemplate
    {

        private readonly IAccountUnitAppService _accountUnitAppService;
        private readonly IJobUnitAppService _jobUnitAppService;
        private readonly int startRowIndex = 2;
        private readonly int endRowIndex = 50000;
        private readonly IRepository<CoaUnit, int> _coaUnitRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountUnitAppService"></param>
        /// <param name="jobUnitAppService"></param>
        /// <param name="coaUnitRepository"></param>
        public LinesTemplate(
             AccountUnitAppService accountUnitAppService,
             IJobUnitAppService jobUnitAppService,
               IRepository<CoaUnit, int> coaUnitRepository
             )
        {
            _accountUnitAppService = accountUnitAppService;
            _jobUnitAppService = jobUnitAppService;
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
            var divisionList = (await _jobUnitAppService.GetDivisionList(new AutoSearchInput() { })).ConvertAll(u => new NameValueDto()
            {
                Value = u.JobId.ToString(),
                Name = u.JobNumber
            });
            var rollUpAccountList = (await _accountUnitAppService.GetRollupAccountsList(new AutoSearchInput() { Id = coaId })).ConvertAll(u => new NameValueDto()
            {
                Value = u.AccountId.ToString(),
                Name = u.AccountNumber
            });
            var booleanList = ExcelHelper.GetBooleanList();
            var accountNumberIsNumeric = (await _coaUnitRepository.FirstOrDefaultAsync(coaId)).IsNumeric;


            return CreateExcelPackage(
                "LinesTemplate_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("LinesTemplate"));
                    var listDataSheet = excelPackage.Workbook.Worksheets.Add(L("DropDownListInformation"));
                    listDataSheet.Hidden = OfficeOpenXml.eWorkSheetHidden.Hidden;

                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, classificationlist, new KeyValuePair<string, string>(L("ClassificationNames"), L("ClassificationIds")), new KeyValuePair<string, string>("A", "B"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, currencylist, new KeyValuePair<string, string>(L("CurrencyNames"), L("CurrencyIds")), new KeyValuePair<string, string>("C", "D"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, consolidationList, new KeyValuePair<string, string>(L("ConsolidationNames"), L("ConsolidationIds")), new KeyValuePair<string, string>("E", "F"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, divisionList, new KeyValuePair<string, string>(L("RollUpDivisionNames"), L("RollUpDivisionIds")), new KeyValuePair<string, string>("G", "H"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, rollUpAccountList, new KeyValuePair<string, string>(L("RollUpAccountNames"), L("RollUpAccountIds")), new KeyValuePair<string, string>("I", "J"));
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, booleanList, new KeyValuePair<string, string>(L("FlagsNames"), L("FlagsIds")), new KeyValuePair<string, string>("K", "L"));

                    //Create Header Row
                    AddHeader(
                        sheet,
                        L("LineNumber"),
                        L("Caption"),
                        L("Classification"),
                          L("ClassificationValue"),
                        L("Consolidation"),
                        L("ConsolidationValue"),
                        L("RollUpAccount"),
                        L("RollUpAccountValue"),
                        L("RollUpDivision"),
                        L("RollUpDivisionValue"),
                        L("Currency"),
                        L("CurrencyValue"),
                        L("JournalsAllowed")
                    );


                    //reference list columns to Excel Sheet
                    ExcelHelper.AddValidationtoSheet(L("LineNumber"), sheet,
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
                            , startRowIndex, endRowIndex, 1);

                    ExcelHelper.AddValidationtoSheet(L("Caption"), sheet,
                      new Excelproperties
                      {
                          ExcelFormula = ExcelHelper.GetMultiValidationString(
                              new List<string>() {
                                    ExcelHelper.GetMaxLengthFormula("B2", AccountUnit.MaxDisplayNameLength),
                                    ExcelHelper.GetDuplicateCellFormula("B",startRowIndex,endRowIndex)
                              }),
                          ShowErrorMessage = true,
                          Error = ExcelHelper.ApplyPlaceHolderValues(L("AllowDuplicateVaues") + ", " + L("AllowMaxLength"), new Dictionary<string, string>() { { "{length}", AccountUnit.MaxDisplayNameLength.ToString() },
                            { "{type}", "Charcters" }}),
                          ErrorTitle = L("ValidationMessage"),
                          ErrorStyle = ExcelDataValidationWarningStyle.stop
                      }
                      , startRowIndex, endRowIndex, 2);


                    var excelDdlErrorMsgSettings = new Excelproperties
                    {
                        ShowErrorMessage = true,
                        Error = L("AllowMaxLength"),
                        ErrorTitle = L("ValidationMessage"),
                        ErrorStyle = ExcelDataValidationWarningStyle.stop
                    };


                    if (classificationlist.Count > 0)
                        ExcelHelper.AddDropDownValidationToSheet(L("Classification"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "A", 2, classificationlist.Count + 1), startRowIndex, endRowIndex, 3);
                    ExcelHelper.AddFormulaToSheet(L("ClassificationValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "C2", new string[] { "A", "B" }, 2, classificationlist.Count + 1), startRowIndex, endRowIndex, true, 4);

                    ExcelHelper.AddDropDownValidationToSheet(L("Consolidation"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "E", 2, consolidationList.Count + 1), startRowIndex, endRowIndex, 5);
                    ExcelHelper.AddFormulaToSheet(L("ConsolidationValue"), sheet,
                    ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "E2", new string[] { "E", "F" }, 2, consolidationList.Count + 1), startRowIndex, endRowIndex, true, 6);

                    if (rollUpAccountList.Count > 0)
                        ExcelHelper.AddDropDownValidationToSheet(L("RollUpAccount"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "I", 2, rollUpAccountList.Count + 1), startRowIndex, endRowIndex, 7);
                    ExcelHelper.AddFormulaToSheet(L("RollUpAccountValue"), sheet,
                      ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "G2", new string[] { "I", "J" }, 2, rollUpAccountList.Count + 1), startRowIndex, endRowIndex, true, 8);


                    if (rollUpAccountList.Count > 0)
                        ExcelHelper.AddDropDownValidationToSheet(L("RollUpDivision"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "G", 2, divisionList.Count + 1), startRowIndex, endRowIndex, 9);
                    ExcelHelper.AddFormulaToSheet(L("RollUpDivisionValue"), sheet,
                         ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "I2", new string[] { "G", "H" }, 2, divisionList.Count + 1), startRowIndex, endRowIndex, true, 10);

                    ExcelHelper.AddDropDownValidationToSheet(L("Currency"), sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "C", 2, currencylist.Count + 1), startRowIndex, endRowIndex, 11);
                    ExcelHelper.AddFormulaToSheet(L("CurrencyValue"), sheet,
                         ExcelHelper.GetVLOOKUPFormula(L("DropDownListInformation"), "K2", new string[] { "C", "D" }, 2, currencylist.Count + 1), startRowIndex, endRowIndex, true, 12);


                    ExcelHelper.AddDropDownValidationToSheet(L("JournalsAllowed"),sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "K", 2, booleanList.Count + 1), startRowIndex, endRowIndex, 13);

                });
        }
    }
}
