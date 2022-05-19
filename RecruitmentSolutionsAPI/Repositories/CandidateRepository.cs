using RecruitmentSolutionsAPI.Data;
using RecruitmentSolutionsAPI.Interfaces;

namespace RecruitmentSolutionsAPI.Repositories;

internal class CandidateRepository : GenericRepository<Candidate>, ICandidateRepository
{
    public CandidateRepository(ApplicationDbContext context) : base(context)
    {
    }
}