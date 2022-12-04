using LmsCopy.Web.Models;

namespace LmsCopy.Web.Services;

public interface IFileReport
{
    public string FileName { get; set; }

    public FileModel GenerateReport<T>(List<T> items);
}
