using System.IO;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
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
        public string FieldValue { get; set; }
        public string ColumnName { get; set; }
        public string Type { get; set; }

        public bool IsEditable { get; set; }

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
    public class ExcelProperites
    {
        public bool ShowErrorMessage { get; set; }
        public string ErrorTitle { get; set; }
        public ExcelDataValidationWarningStyle ErrorStyle { get; set; }
        public string Error { get; set; }
        public string ExcelFormula { get; set; }

    }


    /// <summary>
    /// 
    /// </summary>
    /// 
    public static class ExcelHelper
    {
        const string listDataExcelSheet = "DropDownListInformation";
        const string strOutPutDir = @"C:\EPPlus\Excel";
        const string strPassword = @"Sumit";



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ExcelPackage CreateTemplate(string fileName, string workSheetName)
        {
            string strDate = System.DateTime.Now.ToString();
            // change this line to contain the path to the output folder
            DirectoryInfo outputDir = new DirectoryInfo(strOutPutDir);
            if (!outputDir.Exists)
                Directory.CreateDirectory(strOutPutDir);

            //create the  FileInfo object
            FileInfo templateFile = new FileInfo(outputDir.FullName + @"\" + fileName + "_" + strDate + ".xlsx");
            if (templateFile.Exists)
            {
                templateFile.Delete();
                templateFile = new FileInfo(outputDir.FullName + @"\" + fileName + "_" + strDate + ".xlsx");
            }


            //Create the template...
            ExcelPackage package = new ExcelPackage(templateFile);

            //Lock the workbook totally
            var workbook = package.Workbook;
            //workbook.Protection.LockWindows = true;
            //workbook.Protection.LockStructure = true;
            workbook.View.SetWindowSize(150, 525, 14500, 6000);
            workbook.View.ShowHorizontalScrollBar = false;
            workbook.View.ShowVerticalScrollBar = false;
            workbook.View.ShowSheetTabs = false;

            //Set a password for the workbookprotection
            workbook.Protection.SetPassword(strPassword);

            //Encrypt with no password
            package.Encryption.IsEncrypted = true;

            var sheet = package.Workbook.Worksheets.Add(workSheetName);
            return package;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="cell"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IExcelDataValidationList GetExcelSheetDropDownList(ExcelWorksheet sheet, string cell, List<NameValueDto> list)
        {
            var excelList = sheet.Cells[cell].DataValidation.AddListDataValidation();
            foreach (var value in list)
            {
                excelList.Formula.Values.Add(value.Value);
            }
            excelList.ShowErrorMessage = true;
            return excelList;
        }

        /// <summary>
        /// Fill Dropdown List to Cells in ExcelSheet
        /// </summary>
        public static void AddTemplateObjects(ExcelWorksheet sheet, int startRowIndex, int endRowIndex, params ExcelFields[] propertySelectors)
        {

            for (int j = 0; j <= propertySelectors.Length - 1; j++)
            {
                var columnName = propertySelectors[j].ColumnName;
                if (propertySelectors[j].Type == "dropdown")
                {
                    if (propertySelectors[j].List.Count > 0)
                    {
                        var excelList = sheet.Cells[startRowIndex, j + 1, endRowIndex, j + 1].DataValidation.AddListDataValidation();

                        foreach (var value in propertySelectors[j].List)
                        {
                            excelList.Formula.Values.Add(value.Name);
                        }
                        excelList.ShowErrorMessage = true;
                    }

                }
                else if (!propertySelectors[j].IsEditable)
                {
                    sheet.Cells[startRowIndex, j + 1, endRowIndex, j + 1].Style.Locked = true;
                    sheet.Cells[startRowIndex, j + 1, endRowIndex, j + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells[startRowIndex, j + 1, endRowIndex, j + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }
                else
                {
                    sheet.Cells[startRowIndex, j + 1, endRowIndex, j + 1].Value = "";
                }
            }


        }


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
        /// List Data is Added to Excel Sheet
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="list"></param>
        /// <param name="listName"></param>
        /// <param name="cellColumn"></param>
        public static void AddListDataIntoWorkSheet(ExcelWorksheet sheet, List<NameValueDto> list, string listName, string cellColumn)
        {
            int j = 2;
            sheet.Cells[cellColumn + "1"].Style.Font.Bold = true;
            sheet.Cells[cellColumn + "1"].Value = listName;
            if (list.Count > 0)
            {
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    sheet.Cells[cellColumn + (j + i)].Value = list[i].Name;
                }
            }
        }

        /// <summary>
        ///  List Data is Added to Excel Sheet
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="list"></param>
        /// <param name="listName"></param>
        /// <param name="columnNumber"></param>
        public static void AddListDataIntoWorkSheet(ExcelWorksheet sheet, List<NameValueDto> list, string listName, int columnNumber)
        {
            int j = 2;

            sheet.Cells[1, columnNumber].Style.Font.Bold = true;
            sheet.Cells[1, columnNumber].Value = listName;

            for (int i = 0; i <= list.Count - 1; i++)
            {
                sheet.Cells[(j + i), columnNumber].Value = list[i].Name;
            }


            //// add a validation and set values
            //var validation = sheet.DataValidations.AddListValidation("A1");
            //// Alternatively:
            //// var validation = sheet.Cells["A1"].DataValidation.AddListDataValidation();
            //validation.ShowErrorMessage = true;
            //validation.ErrorStyle = ExcelDataValidationWarningStyle.warning;
            //validation.ErrorTitle = "An invalid value was entered";
            //validation.Error = "Select a value from the list";
            //validation.Formula.ExcelFormula = "B2:B4";
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sheet"></param>
        ///// <param name="excelFormula"></param>
        ///// <param name="startRowIndex"></param>
        ///// <param name="endRowIndex"></param>
        ///// <param name="excelColumn"></param>
        //public static void AddDropDownValidationToSheet(ExcelWorksheet sheet, string excelFormula, int startRowIndex, int endRowIndex, int excelColumn)
        //{
        //    var validation = sheet.Cells[startRowIndex, excelColumn, endRowIndex, excelColumn].DataValidation.AddListDataValidation();
        //    validation.ShowErrorMessage = true;
        //    validation.ErrorStyle = ExcelDataValidationWarningStyle.warning;
        //    validation.ErrorTitle = "An invalid value was entered";
        //    validation.Error = "Select a value from the list";
        //    validation.Formula.ExcelFormula = excelFormula;
        //    //validation.ShowErrorMessage = excelProperties.ShowErrorMessage;
        //    //validation.ErrorStyle = excelProperties.ErrorStyle;
        //    //validation.ErrorTitle = excelProperties.ErrorTitle;
        //    //validation.Error = excelProperties.Error;
        //    //validation.Formula.ExcelFormula = excelProperties.ExcelFormula;
        //}



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="excelFormula"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="endRowIndex"></param>
        /// <param name="excelColumn"></param>
        ///  <param name="excelProperties"></param>
        public static void AddDropDownValidationToSheet(ExcelWorksheet sheet, ExcelProperites excelProperties, string excelFormula, int startRowIndex, int endRowIndex, int excelColumn)
        {
            var validation = sheet.Cells[startRowIndex, excelColumn, endRowIndex, excelColumn].DataValidation.AddListDataValidation();
            validation.ShowErrorMessage = excelProperties.ShowErrorMessage;
            validation.ErrorStyle = excelProperties.ErrorStyle;
            validation.ErrorTitle = excelProperties.ErrorTitle;
            validation.Error = excelProperties.Error;
            validation.Formula.ExcelFormula = excelFormula;
            //validation.ShowErrorMessage = excelProperties.ShowErrorMessage;
            //validation.ErrorStyle = excelProperties.ErrorStyle;
            //validation.ErrorTitle = excelProperties.ErrorTitle;
            //validation.Error = excelProperties.Error;
            //validation.Formula.ExcelFormula = excelProperties.ExcelFormula;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="excelProperties"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="endRowIndex"></param>
        /// <param name="excelColumn"></param>
        public static void AddValidationtoSheet(ExcelWorksheet sheet, ExcelProperites excelProperties, int startRowIndex, int endRowIndex, int excelColumn)
        {
            var validation = sheet.Cells[startRowIndex, excelColumn, endRowIndex, excelColumn].DataValidation.AddCustomDataValidation();
            validation.ShowErrorMessage = excelProperties.ShowErrorMessage;
            validation.ErrorStyle = excelProperties.ErrorStyle;
            validation.ErrorTitle = excelProperties.ErrorTitle;
            validation.Error = excelProperties.Error;
            validation.Formula.ExcelFormula = excelProperties.ExcelFormula;
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
        public static string GetAllowNumberFormula(string cellColumn,bool isNumaric)
        {
            if(isNumaric)
            return "ISNUMBER(" + cellColumn + ")";
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellColumns"></param>
        /// <param name="requiredCellColumn"></param>
        /// <returns></returns>
        public static string GetRequiredFieldFormula(List<string> cellColumns,string requiredCellColumn)
        {
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("OR(");
            foreach (var item in cellColumns)
            {
                if(item!=requiredCellColumn)
                strBuild.Append("LEN(" + item +"2"+ ")>" + 0 + ",");
            }
            return strBuild.ToString().TrimEnd(',') + ")";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validationList"></param>
        /// <returns></returns>
        public static string GetMultiValidationString(List<string> validationList)
        {
            StringBuilder strBuild =new StringBuilder();
            strBuild.Append("AND(");
            foreach (var item in validationList)
            {
                if(!string.IsNullOrEmpty(item))
                strBuild.Append(item+",");
            }
            return strBuild.ToString().TrimEnd(',')+")";
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
            return "COUNTIF(" + "$" + cellColumn + "$" + lstFromRange + ":" + "$" + cellColumn + "$" + lstEndRange+","+cellColumn+ lstFromRange+")=1";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueList"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ApplyPlaceHolderValues(string message,Dictionary<string, string> valueList)
        {
            foreach (var item in valueList)
            {
                message= message.Replace(item.Key,item.Value);
            }
            return message;
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
