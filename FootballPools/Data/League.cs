namespace FootballPools.Data
{
    public class League
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<Questionnaire> Questionnaires { get; set; }
        public List<Pipeline> Pipelines { get; set; }
    }
}