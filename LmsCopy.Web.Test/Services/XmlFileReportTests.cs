using System.Text;
using System.Xml.Serialization;

namespace LmsCopy.Web.Services.Tests;

[TestFixture]
public class XmlFileReportTests
{
    [Test]
    public void GenerateReport_ReturnsFileModelWithCorrectContentType()
    {
        // Arrange
        var report = new XmlFileReport();
        var items = new List<int> { 1, 2, 3 };

        // Act
        var fileModel = report.GenerateReport(items);

        // Assert
        Assert.AreEqual("text/plain", fileModel.ContentType);
    }

    [Test]
    public void GenerateReport_ReturnsFileModelWithCorrectFileName()
    {
        // Arrange
        var report = new XmlFileReport();
        report.FileName = "Report";
        var items = new List<decimal> { 1.23m, 4.56m, 7.89m };
        var expectedFileName = "Report.xml";

        // Act
        var fileModel = report.GenerateReport(items);

        // Assert
        Assert.AreEqual(expectedFileName, fileModel.FileName);
    }

    // Helper method to serialize items to XML
    private string GetSerializedXml<T>(List<T> items, XmlSerializer serializer)
    {
        using var writer = new StringWriter();
        serializer.Serialize(writer, items);
        return writer.ToString();
    }
}
