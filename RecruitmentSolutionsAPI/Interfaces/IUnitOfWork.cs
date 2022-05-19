namespace RecruitmentSolutionsAPI.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICandidateRepository Candidate { get; }

    int Save();
}