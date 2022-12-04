using LmsCopy.Web.Data;
using LmsCopy.Web.Entites;
using LmsCopy.Web.Models;
using LmsCopy.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LmsCopy.Web.Controllers;

public class SubjectController : Controller
{
    private readonly ApplicationDbContext _context;
    
    private readonly SettingsService _settings;

    public SubjectController(ApplicationDbContext context, SettingsService settings)
    {
        _context = context;
        _settings = settings;
    }
    
    [HttpGet]
    [Authorize(Roles = UserRole.Professor)]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [Authorize(Roles = UserRole.Professor)]
    public IActionResult Create(Subject subject)
    {
        if (ModelState.IsValid)
        {
            _context.Subjects.Add(subject);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("Create", subject);
    }
    
    [HttpGet]
    [Authorize(Roles = UserRole.Professor)]
    public IActionResult Edit(Guid id)
    {
        var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
        return View(subject);
    }
    
    [HttpPost]
    [Authorize(Roles = UserRole.Professor)]
    public IActionResult Edit(Subject subject)
    {
        if (ModelState.IsValid)
        {
            _context.Subjects.Update(subject);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("Edit", subject);
    }
    
    [Authorize(Roles = UserRole.Professor)]
    public IActionResult Delete(Guid id)
    {
        if (ModelState.IsValid)
        {
            var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
        return View("Index");
    }
    
    public IActionResult Index()
    {
        var subjects = _context.Subjects.ToList();
        return View(subjects);
    }

    [HttpGet]
    [Authorize(Roles = UserRole.Professor)]
    public IActionResult GenerateReport(String reportType)
    {
        var fileReport = FileReportFactory.GetServiceIndexRequest(reportType, _settings.SubjectReportName); 

        var file = fileReport.GenerateReport<Subject>(_context.Subjects.ToList());
        return File(file.Data, file.ContentType, file.FileName);
    }
}
