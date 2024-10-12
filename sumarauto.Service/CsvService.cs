using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using sumarauto.DataModel;
using ClosedXML.Excel;

namespace sumarauto.Service
{
    public class CsvService
    {
        public List<Make> ReadExcel(string filePath)
        {
            var makes = new List<Make>();

            using (var workbook = new ClosedXML.Excel.XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Assuming the data is on the first sheet
                var rows = worksheet.RowsUsed().Skip(1);  // Skip the header row
                int count = 0;
                foreach (var row in rows)
                {
                    count++;
                    // Try to parse the Id, set to 0 if it fails
                    int makeId;
                    bool isParsed = int.TryParse(row.Cell(1).GetValue<string>(), out makeId);

                    if (isParsed && !string.IsNullOrWhiteSpace(row.Cell(2).GetValue<string>()))
                    {
                        var make = new Make
                        {
                            MakeId = makeId,                        // First column (Id)
                            Title = row.Cell(2).GetValue<string>(),  // Second column (Title)
                            Description = row.Cell(3).GetValue<string>(),
                            CreatedBy = "Admin",
                            CreatedOn = DateTime.Now,
                            EditedOn = DateTime.Now,
                            Status = true,
                            DisplayOrder = count
                        };

                        makes.Add(make);
                    }
                    else
                    {
                        // Optionally log the issue or handle bad data
                        // For example, continue processing valid rows
                    }
                }
            }

            return makes;
        }

    }
}
