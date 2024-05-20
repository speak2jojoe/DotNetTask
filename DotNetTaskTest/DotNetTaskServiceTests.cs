using DotNetTask.DTOs;
using DotNetTask.Models;
using DotNetTask.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Moq;
using System.ComponentModel;

namespace DotNetTaskTest
{
    public class DotNetTaskServiceTests
    {
        private readonly DotNetTaskService _dotNetTaskService;
        private readonly Mock<IContainer> _mockContainer;
        private readonly IConfiguration _configuration;

        public DotNetTaskServiceTests()
        {
            var configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
            _configuration = configuration;
            _mockContainer = new Mock<IContainer>();
            _dotNetTaskService = new DotNetTaskService(_configuration);
        }

        [Fact]
        public async Task CreateQuestionTypeAsync_ValidInput_ReturnsTrue()
        {
            // Arrange
            var questionTypes = new List<string> { Guid.NewGuid().ToString(), "Multiple Choice" };

            // Act
            var result = await _dotNetTaskService.CreateQuestionTypeAsync(questionTypes);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetQuestionTypeAsync_ExistingId_ReturnsQuestionTypeDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var questionType = new QuestionType { Id = id.ToString(), Type = "Multiple Choice" };

            // Act
            var result = await _dotNetTaskService.GetQuestionTypeAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id.ToString(), result.Id);
            Assert.Equal("Multiple Choice", result.Type);
        }

        [Fact]
        public async Task UpdateQuestionTypeAsync_ExistingId_ReturnsSuccessMessage()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existingQuestionType = new QuestionTypeDto { Id = id.ToString(), Type = "Multiple Choice" };
            var updatedQuestionType = "True/False";

            // Act
            var result = await _dotNetTaskService.UpdateQuestionTypeAsync(id, updatedQuestionType);

            // Assert
            Assert.Contains(id.ToString(), result);
            Assert.Contains("updated successfully", result);
        }

        [Fact]
        public async Task DeleteQuestionTypeAsync_ExistingId_ReturnsSuccessMessage()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await _dotNetTaskService.DeleteQuestionTypeAsync(id);

            // Assert
            Assert.Contains(id.ToString(), result);
            Assert.Contains("deleted successfully", result);
        }
    }
}