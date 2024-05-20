using DotNetTask.DTOs;
using DotNetTask.Models;

namespace DotNetTask.Interfaces
{
    public interface IDotNetTaskService
    {
        Task<bool> CreateQuestionTypeAsync(List<string> QuestionTypes);
        Task<IEnumerable<QuestionTypeDto>> GetQuestionTypesAsync();
        Task<QuestionTypeDto> GetQuestionTypeAsync(Guid id);
        Task<string> UpdateQuestionTypeAsync(Guid id, string questionType);
        Task<string> DeleteQuestionTypeAsync(Guid id);
    }
}
