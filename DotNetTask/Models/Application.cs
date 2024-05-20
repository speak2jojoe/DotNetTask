namespace DotNetTask.Models
{
    public class Application
    {
        public Guid Id { get; set; }
        public Guid PersonalInformationId { get; set; }
        public List<AdditionalQuestions> AdditionalQuestions { get; set; }
    }

    public class AdditionalQuestions
    {
        public Guid QuestionTypeId { get; set; }
        public Guid QuestionId { get; set; }
        public dynamic Response { get; set; }
    }
}
