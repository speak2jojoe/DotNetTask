namespace DotNetTask.DTOs
{
    public class ApplicationDto
    {
        public string Id { get; set; }
        public string PersonalInformationId { get; set; }
        public List<AdditionalQuestionsDto> AdditionalQuestions { get; set; }
    }

    public class AdditionalQuestionsDto
    {
        public string QuestionTypeId { get; set; }
        public string QuestionId { get; set; }
        public dynamic Response { get; set; }
    }
}
