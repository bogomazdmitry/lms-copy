using LmsCopy.Web.Models;
using System.Text;
using System.Text.Json;

namespace LmsCopy.Web.Services;

public class JsonFileReport : IFileReport
{

    public string FileName { get; set; }

    public FileModel GenerateReport<T>(List<T> items)
    {
        var dataString = JsonSerializer.Serialize(items);
        return new FileModel()
        {
            Data = Encoding.ASCII.GetBytes(dataString),
            ContentType = "text/plain",
            FileName = $"{FileName}.json"
        };
    }
}
