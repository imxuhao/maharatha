using System;
using System.Collections.Generic;
using System.IO;
using Abp.Collections.Extensions;
using Abp.Dependency;
using CAPS.CORPACCOUNTING.Dto;
using CAPS.CORPACCOUNTING.Net.MimeTypes;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace CAPS.CORPACCOUNTING.DataExporting.Excel.EpPlus
{
    public abstract class EpPlusExcelExporterBase : CORPACCOUNTINGServiceBase, ITransientDependency
    {
        public IAppFolders AppFolders { get; set; }

        protected FileDto CreateExcelPackage(string fileName, Action<ExcelPackage> creator)
        {
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);

            using (var excelPackage = new ExcelPackage())
            {
                creator(excelPackage);
                Save(excelPackage, file);
            }

            return file;
        }

        protected void AddHeader(ExcelWorksheet sheet, params string[] headerTexts)
        {
            if (headerTexts.IsNullOrEmpty())
            {
                return;
            }

            for (var i = 0; i < headerTexts.Length; i++)
            {
                AddHeader(sheet, i + 1, headerTexts[i]);
            }
        }

        protected void AddHeader(ExcelWorksheet sheet, int columnIndex, string headerText)
        {
            sheet.Cells[1, columnIndex].Value = headerText;
            sheet.Cells[1, columnIndex].Style.Font.Bold = true;
            sheet.Cells[1, columnIndex].Style.Font.Color.SetColor(Color.White);
            sheet.Cells[1, columnIndex].Style.WrapText = false;
            sheet.Cells[1, columnIndex].AutoFitColumns();
            sheet.Cells[1, columnIndex].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            sheet.Cells[1, columnIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[1, columnIndex].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[1, columnIndex].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(127, 176, 218));
        }

        protected void AddObjects<T>(ExcelWorksheet sheet, int startRowIndex, IList<T> items, params Func<T, object>[] propertySelectors)
        {
            if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty())
            {
                return;
            }

            for (var i = 0; i < items.Count; i++)
            {
                for (var j = 0; j < propertySelectors.Length; j++)
                {
                    sheet.Cells[i + startRowIndex, j + 1].Value = propertySelectors[j](items[i]);
                }
            }
        }

        protected void Save(ExcelPackage excelPackage, FileDto file)
        {
            var filePath = Path.Combine(AppFolders.TempFileDownloadFolder, file.FileToken);
            excelPackage.SaveAs(new FileInfo(filePath));
        }
    }
}