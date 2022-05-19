using RecruitmentSolutionsAPI.Data;
using RecruitmentSolutionsAPI.Interfaces;

namespace RecruitmentSolutionsAPI.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext context;
    public ICandidateRepository Candidate { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        this.context = context;
        Candidate = new CandidateRepository(this.context);
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public int Save()
    {
        return context.SaveChanges();
    }
}