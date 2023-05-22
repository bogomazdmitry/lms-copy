using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace LmsCopy.Web.Services.Tests;

[TestFixture]
public class JsonFileReportTests
{
    [Test]
    public void GenerateReport_ReturnsFileModelWithCorrectContentType()
    {
        // Arrange
        var report = new JsonFileReport();
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
        var report = new JsonFileReport();
        report.FileName = "Report";
        var items = new List<decimal> { 1.23m, 4.56m, 7.89m };
        var expectedFileName = "Report.json";

        // Act
        var fileModel = report.GenerateReport(items);

        // Assert
        Assert.AreEqual(expectedFileName, fileModel.FileName);
    }
}
