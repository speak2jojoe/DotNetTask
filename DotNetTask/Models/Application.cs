namespace DotNetTask.Models
{
    public class Application
    {
        public string Id { get; set; }
        public string PersonalInformationId { get; set; }
        public List<AdditionalQuestions> AdditionalQuestions { get; set; }
    }

    public class AdditionalQuestions
    {
        public string QuestionTypeId { get; set; }
        public string QuestionId { get; set; }
        public dynamic Response { get; set; }
    }
}
