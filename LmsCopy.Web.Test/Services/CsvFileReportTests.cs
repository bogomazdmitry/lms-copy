namespace LmsCopy.Web.Services.Tests;

[TestFixture]
public class CsvFileReportTests
{
    [Test]
    public void GenerateReport_ReturnsFileModelWithCorrectContentType()
    {
        // Arrange
        var report = new CsvFileReport();
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
        var report = new CsvFileReport();
        report.FileName = "Report";
        var items = new List<decimal> { 1.23m, 4.56m, 7.89m };

        // Act
        var fileModel = report.GenerateReport(items);

        // Assert
        Assert.AreEqual("Report.csv", fileModel.FileName);
    }
}
