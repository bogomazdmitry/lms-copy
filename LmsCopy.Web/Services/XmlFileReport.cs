using LmsCopy.Web.Models;
using System.Text;
using System.Xml.Serialization;

namespace LmsCopy.Web.Services;

public class XmlFileReport : IFileReport
{
    public string FileName { get; set; }

    public FileModel GenerateReport<T>(List<T> items)
    {
        var serializer = new XmlSerializer(typeof(List<T>));
        using var writer = new StringWriter();
        
        serializer.Serialize(writer, items);
        return new FileModel()
        {

            Data = Encoding.ASCII.GetBytes(writer.ToString()),
            ContentType = "text/plain",
            FileName = $"{FileName}.xml"
        };
    }
}
