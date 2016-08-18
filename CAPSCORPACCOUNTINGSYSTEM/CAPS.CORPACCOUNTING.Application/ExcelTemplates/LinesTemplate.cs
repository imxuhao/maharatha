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

namespace CAPS.CORPACCOUNTING.ExcelTemplates
{
    public class LinesTemplate : EpPlusExcelExporterBase, ITemplate
    {

        private readonly IAccountUnitAppService _accountUnitAppService;
        private readonly IJobUnitAppService _jobUnitAppService;
        private readonly int startRowIndex = 2;
        private readonly int endRowIndex = 3000;
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
                "LinesTemplate.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("LinesTemplate"));
                    var listDataSheet = excelPackage.Workbook.Worksheets.Add(L("DropDownListInformation"));
                    listDataSheet.Hidden = OfficeOpenXml.eWorkSheetHidden.Hidden;

                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, classificationlist, L("Classification"), "A");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, currencylist, L("Currency"), "B");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, consolidationList, L("Consolidation"), "C");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, divisionList, L("RollUpDivision"), "D");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, rollUpAccountList, L("RollUpAccount"), "E");
                    ExcelHelper.AddListDataIntoWorkSheet(listDataSheet, booleanList, L("Flags"), "F");


                    //Create Header Row
                    AddHeader(
                        sheet,
                    L("LineNumber"),
                    L("Caption"),
                    L("Classification"),
                    L("Consolidation"),
                    L("RollUpAccount"),
                    L("RollUpDivision"),
                    L("Currency"),
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
                                    ExcelHelper.GetMaxLengthFormula("B2", AccountUnit.MaxDisplayNameLength),
                                    ExcelHelper.GetDuplicateCellFormula("B",startRowIndex,endRowIndex)

                              }),
                          ShowErrorMessage = true,
                          Error = ExcelHelper.ApplyPlaceHolderValues(L("AllowDuplicateVaues") + ", "+L("AllowMaxLength"), new Dictionary<string, string>() { { "{length}", AccountUnit.MaxDisplayNameLength.ToString() },
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


                    if (rollUpAccountList.Count > 0)
                        ExcelHelper.AddDropDownValidationToSheet(sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "D", 2, rollUpAccountList.Count + 1), startRowIndex, endRowIndex, 5);

                    if (rollUpAccountList.Count > 0)
                        ExcelHelper.AddDropDownValidationToSheet(sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "E", 2, divisionList.Count + 1), startRowIndex, endRowIndex, 6);

                    ExcelHelper.AddDropDownValidationToSheet(sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "B", 2, currencylist.Count + 1), startRowIndex, endRowIndex, 7);
                    ExcelHelper.AddDropDownValidationToSheet(sheet, excelDdlErrorMsgSettings, ExcelHelper.GetDropDownListFormula(L("DropDownListInformation"), "F", 2, booleanList.Count + 1), startRowIndex, endRowIndex, 8);

                });
        }
    }
}
