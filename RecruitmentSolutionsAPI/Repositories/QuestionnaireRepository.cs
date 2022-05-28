using RecruitmentSolutionsAPI.Data;
using RecruitmentSolutionsAPI.Data.Context;
using RecruitmentSolutionsAPI.Interfaces;

namespace RecruitmentSolutionsAPI.Repositories;

internal class QuestionnaireRepository : GenericRepository<Questionnaire>, IQuestionnaireRepository
{
    public QuestionnaireRepository(ApplicationDbContext context) : base(context)
    {
    }
}