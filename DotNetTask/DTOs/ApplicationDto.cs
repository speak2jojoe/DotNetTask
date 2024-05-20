namespace DotNetTask.DTOs
{
    public class ApplicationDto
    {
        public Guid Id { get; set; }
        public Guid PersonalInformationId { get; set; }
        public List<AdditionalQuestionsDto> AdditionalQuestions { get; set; }
    }

    public class AdditionalQuestionsDto
    {
        public Guid QuestionTypeId { get; set; }
        public Guid QuestionId { get; set; }
        public dynamic Response { get; set; }
    }
}
