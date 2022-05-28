using RecruitmentSolutionsAPI.Data.Context;
using RecruitmentSolutionsAPI.Interfaces;

namespace RecruitmentSolutionsAPI.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext context;
    public ICandidateRepository Candidate { get; private set; }
    public IPipelineRepository Pipeline { get; private set; }
    public IQuestionnaireRepository Questionnaire { get; private set; }
    public ICompanyRepository Company { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        this.context = context;
        Candidate = new CandidateRepository(this.context);
        Pipeline = new PipelineRepository(this.context);
        Company = new CompanyRepository(this.context);
        Questionnaire = new QuestionnaireRepository(this.context);
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