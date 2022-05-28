namespace RecruitmentSolutionsAPI.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICandidateRepository Candidate { get; }
    IPipelineRepository Pipeline { get; }
    IQuestionnaireRepository Questionnaire { get; }
    ICompanyRepository Company { get; }

    int Save();
}