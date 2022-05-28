namespace RecruitmentSolutionsAPI.Data
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Questionnaire> Questionnaires { get; set; }
        public List<Pipeline> Pipelines { get; set; }
    }
}