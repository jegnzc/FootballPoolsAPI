using RecruitmentSolutionsAPI.Data;
using RecruitmentSolutionsAPI.Data.Context;
using RecruitmentSolutionsAPI.Interfaces;

namespace RecruitmentSolutionsAPI.Repositories;

internal class PipelineRepository : GenericRepository<Pipeline>, IPipelineRepository
{
    public PipelineRepository(ApplicationDbContext context) : base(context)
    {
    }
}