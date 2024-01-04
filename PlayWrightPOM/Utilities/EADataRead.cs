using ExcelDataReader;
using PlayWrightPOM.TestDataClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayWrightPOM.Utilities
{
    internal class EADataRead
    {
        public static List<EAData> ReadLoginCredentials(string excelFilePath, string sheetName)
        {
            List<EAData> excelDataList = new();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });
                    var dataTable = result.Tables[sheetName];
                    if (dataTable != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            EAData excelData = new()
                            {
                                Username = GetValueOrDefault(row, "username"),
                                Password = GetValueOrDefault(row, "password")
                            };
                            excelDataList.Add(excelData);
                        }
                    }
                    else
                    {
                        Console.WriteLine(sheetName + " not found in the excel file.");
                    }
                }
            }
            return excelDataList;
        }
        static string? GetValueOrDefault(DataRow row, string columnName)
        {
            return row.Table.Columns.Contains(columnName) ? row[columnName].ToString() : null;
        }
    }
}
