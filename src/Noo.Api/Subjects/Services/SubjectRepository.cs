using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Subjects.Models;

namespace Noo.Api.Subjects.Services;

public class SubjectRepository : Repository<SubjectModel>, ISubjectRepository { }

public static class UnitIOfWorkSubjectRepositoryExtensions
{
    public static ISubjectRepository SubjectRepository(this IUnitOfWork unitOfWork)
    {
        return new SubjectRepository()
        {
            Context = unitOfWork.Context
        };
    }
}
