using AssetManagerPro.Models;
using ClosedXML.Excel;
using Microsoft.Win32;
using System.Windows;

namespace AssetManagerPro.Services
{
    public class ExcelService
    {
        public void ExportAssets(List<AssetDisplay> assets)
        {
            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("اموال");
            worksheet.Cell(1, 1).Value = "کد اموال";
            worksheet.Cell(1, 2).Value = "نام کالا";
            worksheet.Cell(1, 3).Value = "برند";
            worksheet.Cell(1, 4).Value = "دسته‌بندی";
            worksheet.Cell(1, 5).Value = "مدل";
            worksheet.Cell(1, 6).Value = "شماره سریال";
            worksheet.Cell(1, 7).Value = "محل استقرار";
            worksheet.Cell(1, 8).Value = "تحویل گیرنده";
            worksheet.Cell(1, 9).Value = "وضعیت";
            worksheet.Cell(1, 10).Value = "قیمت";
            worksheet.Cell(1, 11).Value = "تاریخ خرید";
            var header = worksheet.Range("A1:K1");

            header.Style.Font.Bold = true;

            header.Style.Fill.BackgroundColor = XLColor.LightBlue;

            header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            int row = 2;

            foreach (var asset in assets)
            {
                worksheet.Cell(row, 1).Value = asset.AssetCode;
                worksheet.Cell(row, 2).Value = asset.Name;
                worksheet.Cell(row, 3).Value = asset.Brand;
                worksheet.Cell(row, 4).Value = asset.Category;
                worksheet.Cell(row, 5).Value = asset.Model;
                worksheet.Cell(row, 6).Value = asset.SerialNumber;
                worksheet.Cell(row, 7).Value = asset.Location;
                worksheet.Cell(row, 8).Value = asset.Receiver;
                worksheet.Cell(row, 9).Value = asset.Status;
                worksheet.Cell(row, 10).Value = asset.Price;
                worksheet.Cell(row, 11).Value = asset.PurchaseDate;

                row++;
            }
            
            worksheet.Columns().AdjustToContents();
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "Excel Workbook|*.xlsx";

            dialog.FileName = $"گزارش_اموال_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            if (dialog.ShowDialog() == true)
            {
                workbook.SaveAs(dialog.FileName);

                MessageBox.Show(
                    "فایل Excel با موفقیت ذخیره شد.",
                    "خروجی Excel",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }
}