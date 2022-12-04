using CsvHelper;
using LmsCopy.Web.Models;
using System.Globalization;
using System.Text;

namespace LmsCopy.Web.Services;

public class CsvFileReport : IFileReport
{
    public string FileName { get; set; }
    
    public FileModel GenerateReport<T>(List<T> items)
    {
        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        
        csv.WriteRecords(items);
        
        return new FileModel()
        {
            Data = Encoding.ASCII.GetBytes(writer.ToString()),
            ContentType = "text/plain",
            FileName = $"{FileName}.csv"
        };
    }
}
