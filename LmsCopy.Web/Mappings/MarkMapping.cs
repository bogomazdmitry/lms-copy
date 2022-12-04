using LmsCopy.Web.Entites;
using LmsCopy.Web.Models;

namespace LmsCopy.Web.Mappings;

public static class MarkMapping
{
    public static Mark ToMark(MarkStudentModel markStudentModel)
    {
        return new Mark
        {
            Id = markStudentModel.Id,
            Value = markStudentModel.Value,
        };
    }
    
    public static MarkStudentModel ToMarkStudentModel(Mark mark)
    {
        return new MarkStudentModel
        {
            Id = mark.Id,
            Value = mark.Value,
            ProfessorName = mark.Professor?.UserName,
            SubjectName = mark.Subject?.Name,
        };
    }
    
    
    public static Mark ToMark(MarkProfessorModel markProfessorModel)
    {
        return new Mark
        {
            Id = markProfessorModel.Id,
            Value = markProfessorModel.Value,
        };
    }
    
    public static MarkProfessorModel ToMarkProfessorModel(Mark mark)
    {
        return new MarkProfessorModel
        {
            Id = mark.Id,
            Value = mark.Value,
            StudentName = mark.Student?.UserName,
            SubjectName = mark.Subject?.Name,
        };
    }
}
