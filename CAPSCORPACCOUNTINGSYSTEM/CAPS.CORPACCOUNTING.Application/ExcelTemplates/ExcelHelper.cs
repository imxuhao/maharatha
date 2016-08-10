using System.IO;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using OfficeOpenXml.DataValidation.Contracts;
using System.Threading.Tasks;
using System;
using System.Data;
using Abp.Collections.Extensions;
using Abp.Application.Services.Dto;
using OfficeOpenXml.DataValidation;

namespace CAPS.CORPACCOUNTING.ExcelTemplates
{

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

        //public ExcelFields(string columnName, bool iseditable = false)
        //{
        //    IsEditable = iseditable;
        //    ColumnName = columnName;
        //}
    }


    /// <summary>
    /// 
    /// </summary>
    /// 

    public static class ExcelHelper
    {
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
        /// 
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
        /// 
        /// </summary>
        /// <param name="cell"></param>
        public static void CellEditable(ExcelRange cell)
        {
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<NameValueDto> GetBooleanList()
        {
            return (new List<NameValueDto>()
            {
                new NameValueDto(name:"true",value:"1"),
                 new NameValueDto(name:"false",value:"0"),

            });
        }

        private static void AddListDataIntoWorkSheet(ExcelWorksheet sheet, List<NameValueDto> list, string listName, string cellColumn)
        {
            int j = 2;
            //var sheet = package.Workbook.Worksheets.Add("list formula");
            sheet.Cells[cellColumn + "1"].Style.Font.Bold = true;
            sheet.Cells[cellColumn + "1"].Value = listName;

            for (int i = 0; i <= list.Count - 1; i++)
            {
                j = j + i;
                sheet.Cells[cellColumn + j].Value = list[i].Name;
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

        private static void AddDropDownValidationToSheet(ExcelWorksheet sheet, string excelFormula,int startRowIndex, int endRowIndex,int excelColumn)
        {
            var validation = sheet.Cells[startRowIndex, excelColumn, endRowIndex, excelColumn].DataValidation.AddListDataValidation();
            validation.ShowErrorMessage = true;
            validation.ErrorStyle = ExcelDataValidationWarningStyle.warning;
            validation.ErrorTitle = "An invalid value was entered";
            validation.Error = "Select a value from the list";
            validation.Formula.ExcelFormula = excelFormula;
        }

        private static string GetDropDownListFormula(string cellColumn, int lstFromRange, int lstEndRange)
        {
            return cellColumn + lstFromRange + ":" + cellColumn + lstEndRange;
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
                row["No"] = rowNum - 1;
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
