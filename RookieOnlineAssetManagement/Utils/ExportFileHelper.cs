using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using RookieOnlineAssetManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Utils
{
    public class ExportFileHelper
    {
        public static FileContentResult ExportExcel(ICollection<ReportModel> report, string workSheetName, string fileName)
        {
            if (report.Count > 0)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(workSheetName);
                    var currentRow = 1;
                    PropertyInfo[] properties = report.FirstOrDefault().GetType().GetProperties();
                    for (int i = 0; i < properties.Length; i++)
                    {
                        var displayName = properties[i].GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().SingleOrDefault();
                        worksheet.Cell(currentRow, i + 1).Style.Font.SetBold();
                        worksheet.Cell(currentRow, i + 1).Value = displayName != null ? displayName.DisplayName.ToString() : properties[i].Name.ToString();
                    }
                    foreach (var reportmodel in report)
                    {
                        currentRow++;
                        PropertyInfo[] propertyInfos = reportmodel.GetType().GetProperties();
                        for (int i = 0; i < propertyInfos.Length; i++)
                        {
                            worksheet.Cell(currentRow, i + 1).Value = propertyInfos[i].GetValue(reportmodel, null).ToString();
                        }
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        FileContentResult file = new FileContentResult(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                        {
                            FileDownloadName = fileName + ".xlsx"
                        };
                        return file;
                    }
                }
            }
            return (FileContentResult)null;
        }
    }
}
