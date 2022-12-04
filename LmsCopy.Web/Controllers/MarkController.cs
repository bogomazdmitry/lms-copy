using LmsCopy.Web.Areas.Identity.Pages;
using LmsCopy.Web.Data;
using LmsCopy.Web.Entites;
using LmsCopy.Web.Mappings;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LmsCopy.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LmsCopy.Web.Controllers;

public class MarkController : Controller
{
    private readonly ILogger<MarkController> _logger;

    private readonly UserManager<User> _userManager;

    private readonly ApplicationDbContext _context;

    public MarkController(UserManager<User> userManager, ILogger<MarkController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _userManager = userManager;
        _context = context;
    }

    [Authorize]
    public IActionResult Index()
    {
        var user = _userManager.Users.Where(u => u.Id == new Guid(_userManager.GetUserId(User)))
            .Include(u => u.ProfessorMarks)
            .Include(u => u.StudentMarks)
            .ThenInclude(m => m.Subject)
            .Include(u => u.StudentMarks)
            .ThenInclude(m => m.Subject)
            .FirstOrDefault();

        if (User.IsInRole(UserRole.Professor))
        {
            var markModels = user?.ProfessorMarks
                .Select(m => MarkMapping.ToMarkProfessorModel(m))
                .ToList() ?? new List<MarkProfessorModel>();
            return View("IndexProfessor", markModels);
        }
        else
        {
            var markModels = user?.StudentMarks
                .Select(m => MarkMapping.ToMarkStudentModel(m))
                .ToList() ?? new List<MarkStudentModel>();
            return View("IndexStudent", markModels);
        }
    }

    [HttpGet]
    [Authorize(Roles = UserRole.Professor)]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = UserRole.Professor)]
    public async Task<IActionResult> Create(MarkProfessorModel markModel)
    {
        var mark = MarkMapping.ToMark(markModel);
        var subjectId = _context.Subjects
            .Where(s => s.Name == markModel.SubjectName)
            .Select(s => s.Id)
            .FirstOrDefault();
        if (subjectId == Guid.Empty)
        {
            ModelState.AddModelError("SubjectName", "Subject is not found");
        }
        else
        {
            mark.SubjectId = subjectId;
        }

        var student = _userManager.Users
            .FirstOrDefault(s => s.UserName == markModel.StudentName);
        if (student == null)
        {
            ModelState.AddModelError("StudentName", "Student is not found");
        }
        else
        {
            var isProfessor = !(await _userManager.IsInRoleAsync(student, UserRole.Professor));
            if (isProfessor)
            {
                ModelState.AddModelError("StudentName", "It is professor!");
            }
            else
            {
                mark.StudentId = student.Id;
            }
        }
        mark.ProfessorId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (ModelState.IsValid)
        {
            _context.Marks.Add(mark);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("Create", markModel);
    }

    [HttpGet]
    [Authorize(Roles = UserRole.Professor)]
    public IActionResult Edit(Guid id)
    {
        var mark = _context.Marks
            .Include(m => m.Student)
            .Include(m => m.Subject)
            .FirstOrDefault(s => s.Id == id);
        return View(MarkMapping.ToMarkProfessorModel(mark));
    }

    [HttpPost]
    [Authorize(Roles = UserRole.Professor)]
    public IActionResult Edit(MarkProfessorModel markModel)
    {
        var mark = MarkMapping.ToMark(markModel);
        var subjectId = _context.Subjects
            .Where(s => s.Name == markModel.SubjectName)
            .Select(s => s.Id)
            .FirstOrDefault();
        if (subjectId == Guid.Empty)
        {
            ModelState.AddModelError("SubjectName", "Subject is not found");
        }
        else
        {
            mark.SubjectId = subjectId;
        }

        var studentId = _userManager.Users
            .Where(s => s.UserName == markModel.StudentName)
            .Select(s => s.Id)
            .FirstOrDefault();
        if (studentId == null)
        {
            ModelState.AddModelError("StudentName", "Student is not found");
        }
        else
        {
            mark.StudentId = studentId;
        }
        mark.ProfessorId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (ModelState.IsValid)
        {
            _context.Marks.Update(mark);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("Edit", markModel);
    }

    [Authorize(Roles = UserRole.Professor)]
    public IActionResult Delete(Guid id)
    {
        if (ModelState.IsValid)
        {
            var markModel = _context.Marks.FirstOrDefault(s => s.Id == id);
            if (markModel != null)
            {
                _context.Marks.Remove(markModel);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        return View("IndexProfessor");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
