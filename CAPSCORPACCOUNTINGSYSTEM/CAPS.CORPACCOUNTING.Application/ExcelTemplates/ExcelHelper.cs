using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;
using OfficeOpenXml.DataValidation.Contracts;
using System.Data;
using Abp.Application.Services.Dto;
using OfficeOpenXml.DataValidation;
using System.Text;

namespace CAPS.CORPACCOUNTING.ExcelTemplates
{
    /// <summary>
    /// 
    /// </summary>
    public class ExcelFields
    {
        /// <summary>
        /// 
        /// </summary>
        public string FieldValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<NameValueDto> List { get; set; }



        public ExcelFields()
        { }
        public ExcelFields(string columnName, string type = "", bool iseditable = false, List<NameValueDto> list = null, string fieldvalue = "")
        {

            Type = type;
            IsEditable = iseditable;
            List = list;
            FieldValue = fieldvalue;
            ColumnName = columnName;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Excelproperties
    {
        /// <summary>
        /// 
        /// </summary>
        public bool ShowErrorMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ExcelDataValidationWarningStyle ErrorStyle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExcelFormula { get; set; }

    }


    /// <summary>
    /// 
    /// </summary>
    /// 
    public static class ExcelHelper
    {
        const string listDataExcelSheet = "DropDownListInformation";
        const string strPassword = @"Sumit";
       

        /// <summary>
        /// Get Boolean values as List
        /// </summary>
        /// <returns></returns>
        public static List<NameValueDto> GetBooleanList()
        {
            return (new List<NameValueDto>()
            {
                new NameValueDto(name:"TRUE",value:"1"),
                 new NameValueDto(name:"FALSE",value:"0"),
            });
        }

        /// <summary>
        /// List Data is Added into Data Excel Sheet
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="list"></param>
        /// <param name="listName"></param>
        /// <param name="cellList"></param>
        public static void AddListDataIntoWorkSheet(ExcelWorksheet sheet, List<NameValueDto> list, KeyValuePair<string, string> listName, KeyValuePair<string, string> cellList)
        {
            int j = 2;
            sheet.Cells[cellList.Key + "1"].Style.Font.Bold = true;
            sheet.Cells[cellList.Key + "1"].Value = listName.Key;

            sheet.Cells[cellList.Value + "1"].Style.Font.Bold = true;
            sheet.Cells[cellList.Value + "1"].Value = listName.Value;
            if (list.Count > 0)
            {
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    sheet.Cells[cellList.Key + (j + i)].Value = list[i].Name;
                    sheet.Cells[cellList.Value + (j + i)].Value = list[i].Value;
                }
            }
        }
       

        /// <summary>
        /// Add DropDownList into Data Sheet.
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="excelFormula"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="endRowIndex"></param>
        /// <param name="excelColumn"></param>
        ///  <param name="excelProperties"></param>
        ///  <param name="columnName"></param>
        public static void AddDropDownValidationToSheet(string columnName, ExcelWorksheet sheet, Excelproperties excelProperties, string excelFormula, int startRowIndex, int endRowIndex, int excelColumn)
        {
            var validation = sheet.Cells[startRowIndex, excelColumn, endRowIndex, excelColumn].DataValidation.AddListDataValidation();
            validation.ShowErrorMessage = excelProperties.ShowErrorMessage;
            validation.ErrorStyle = excelProperties.ErrorStyle;
            validation.ErrorTitle = excelProperties.ErrorTitle;
            validation.Error = excelProperties.Error;
            validation.Formula.ExcelFormula = excelFormula;
        }
               
        /// <summary>
        /// add validation to DataSheet
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="excelProperties"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="endRowIndex"></param>
        /// <param name="excelColumn"></param>
        /// <param name="columnName"></param>
        public static void AddValidationtoSheet(string columnName, ExcelWorksheet sheet, Excelproperties excelProperties, int startRowIndex, int endRowIndex, int excelColumn)
        {
            var validation = sheet.Cells[startRowIndex, excelColumn, endRowIndex, excelColumn].DataValidation.AddCustomDataValidation();
            
            validation.ShowErrorMessage = excelProperties.ShowErrorMessage;
            validation.ErrorStyle = excelProperties.ErrorStyle;
            validation.ErrorTitle = excelProperties.ErrorTitle;
            validation.Error = excelProperties.Error;
            validation.Formula.ExcelFormula = excelProperties.ExcelFormula;
        }

        /// <summary>
        /// add excel formula to Data Sheet
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="sheet"></param>
        /// <param name="excelFormula"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="endRowIndex"></param>
        /// <param name="excelColumn"></param>
        public static void AddFormulaToSheet(string columnName, ExcelWorksheet sheet, string excelFormula, int startRowIndex, int endRowIndex, bool hideColumn, int excelColumn)
        {
            sheet.Cells[startRowIndex, excelColumn, endRowIndex, excelColumn].Formula = excelFormula;
            sheet.Column(excelColumn).Hidden = hideColumn;
        }

        /// <summary>
        /// =Sheet2!$A$1:$A$4
        /// Refer other sheet data as Dropdown list values in Cells
        /// </summary>
        /// <param name="cellColumn"></param>
        /// <param name="lstFromRange"></param>
        /// <param name="lstEndRange"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static string GetDropDownListFormula(string sheetName, string cellColumn, int lstFromRange, int lstEndRange)
        {
            return "'" + sheetName + "'" + "!" + "$" + cellColumn + "$" + lstFromRange + ":" + "$" + cellColumn + "$" + lstEndRange;
        }

        /// <summary>
        /// LEN(A2)>5
        /// </summary>
        /// <param name="cellColumn"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string GetMaxLengthFormula(string cellColumn, int maxLength)
        {
            return "LEN(" + cellColumn + ")<" + (maxLength + 1);
        }
        /// <summary>
        /// ISNUMBER(C8)
        /// </summary>
        /// <param name="cellColumn"></param>
        ///  <param name="isNumaric"></param>
        /// <returns></returns>
        public static string GetAllowNumberFormula(string cellColumn, bool isNumaric)
        {
            if (isNumaric)
                return "ISNUMBER(" + cellColumn + ")";
            return "";
        }

        /// <summary>
        ///  =VLOOKUP(C11,'DropDown List Information'!$A$2:$B$70,2,FALSE)
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="cellColumn"></param>
        /// <param name="lstFromRange"></param>
        /// <param name="lstEndRange"></param>
        ///  <param name="dropDownCellColumns"></param>
        /// <returns></returns>
        public static string GetVLOOKUPFormula(string sheetName, string cellColumn, string[] dropDownCellColumns, int lstFromRange, int lstEndRange)
        {
            return "VLOOKUP(" + cellColumn + ",'" + sheetName + "'" + "!" + "$" + dropDownCellColumns[0] + "$" + lstFromRange + ":" + "$" + dropDownCellColumns[1] + "$" + lstEndRange + ",2,FALSE)";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellColumns"></param>
        /// <param name="requiredCellColumn"></param>
        /// <returns></returns>
        public static string GetRequiredFieldFormula(List<string> cellColumns, string requiredCellColumn)
        {
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("OR(");
            foreach (var item in cellColumns)
            {
                if (item != requiredCellColumn)
                    strBuild.Append("LEN(" + item + "2" + ")>" + 0 + ",");
            }
            return strBuild.ToString().TrimEnd(',') + ")";
        }

        /// <summary>
        /// get multi validation formula as string
        /// </summary>
        /// <param name="validationList"></param>
        /// <returns></returns>
        public static string GetMultiValidationString(List<string> validationList)
        {
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("AND(");
            foreach (var item in validationList)
            {
                if (!string.IsNullOrEmpty(item))
                    strBuild.Append(item + ",");
            }
            return strBuild.ToString().TrimEnd(',') + ")";
        }


        /// <summary>
        /// =COUNTIF($A$2:$A$20,A2)
        /// Refer other sheet data as Dropdown list values in Cells
        /// </summary>
        /// <param name="cellColumn"></param>
        /// <param name="lstFromRange"></param>
        /// <param name="lstEndRange"></param>
        /// <returns></returns>
        public static string GetDuplicateCellFormula(string cellColumn, int lstFromRange, int lstEndRange)
        {
            return "COUNTIF(" + "$" + cellColumn + "$" + lstFromRange + ":" + "$" + cellColumn + "$" + lstEndRange + "," + cellColumn + lstFromRange + ")=1";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueList"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ApplyPlaceHolderValues(string message, Dictionary<string, string> valueList)
        {
            foreach (var item in valueList)
            {
                message = message.Replace(item.Key, item.Value);
            }
            return message;
        }

        /// <summary>
        /// set formula to Excel Properties
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="excelFromula"></param>
        /// <returns></returns>
        public static Excelproperties SetExcelFromula(Excelproperties properties, string excelFromula)
        {
            properties.ExcelFormula = excelFromula;
            return properties;

        }
        /// <summary>
        /// Converting Excel file to DataTable
        /// </summary>
        /// <param name="excelPackage"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static DataTable ConvertExcelToDataTable(ExcelPackage excelPackage, string entityName)
        {
            DataTable table = new DataTable(entityName);
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];


            // Create DataTable columns based on values in first row.
            foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                table.Columns.Add(firstRowCell.Text);
            }
            table.Columns.Add("No");

            // Start reading in second row, (the row after the header).
            for (var rowNum = 2; rowNum <= worksheet.Dimension.End.Row; rowNum++)
            {
                bool isEmtpyRow = false;
                var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                var row = table.NewRow();
                row["No"] = rowNum;
                foreach (var cell in wsRow)
                {
                    try
                    {
                        if (cell.Text.Trim().Length > 0)
                        {
                            isEmtpyRow = true;
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }
                    catch
                    {
                        // The cell.Text reference above is apt to throw a null reference exception depending on how the data
                        // is stored in the Excel doc. We want to just call this a null value and move on.
                        row[cell.Start.Column - 1] = null;
                    }
                }
                if (isEmtpyRow)
                    table.Rows.Add(row);
            }

            return table;
        }
    }
}
