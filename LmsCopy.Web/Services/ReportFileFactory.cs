using NuGet.Protocol.Plugins;

namespace LmsCopy.Web.Services;

public static class FileReportFactory
{
    public static IFileReport GetServiceIndexRequest(string fileType, string fileName)
    {
        return fileType switch
        {
            "Json" => new JsonFileReport{ FileName = fileName},
            "Xml" => new XmlFileReport{ FileName = fileName},
            "Csv" => new CsvFileReport{ FileName = fileName},
        };
    }
}
