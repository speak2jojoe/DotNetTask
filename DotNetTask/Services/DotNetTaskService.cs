using DotNetTask.Interfaces;
using Microsoft.Azure.Cosmos;
using DotNetTask.Models;
using System.ComponentModel;
using Container = Microsoft.Azure.Cosmos.Container;
using DotNetTask.DTOs;

namespace DotNetTask.Services
{
    public class DotNetTaskService : IDotNetTaskService
    {
        private readonly IConfiguration _configuration;
        private readonly string _databaseName;
        private readonly string _containerName;
        private readonly Container _container;

        public IContainer Object { get; }

        public DotNetTaskService(IConfiguration configuration, IContainer @object)
        {
            _configuration = configuration;
            var cosmosDBSettings = _configuration.GetSection("CosmosDBSettings");
            var endpointUri = cosmosDBSettings["EndpointUri"];
            var primaryKey = cosmosDBSettings["PrimaryKey"];
            _databaseName = cosmosDBSettings["DatabaseName"];
            _containerName = cosmosDBSettings["ContainerName"];

            var cosmosClient = new CosmosClient(endpointUri, primaryKey);
            var database = cosmosClient.GetDatabase(_databaseName);
            _container = database.GetContainer(_containerName);
            Object = @object;
        }

        public async Task<bool> CreateQuestionTypeAsync(List<string> QuestionTypes)
        {
            try
            {
                // Create a new item/document to add to the container
                List<QuestionType> QuestionType = new List<QuestionType>();
                foreach (var type in QuestionTypes)
                {
                    QuestionType.Add(new QuestionType
                    {
                        Id = Guid.NewGuid().ToString(),
                        Type = type
                    });
                }
              
                // Add the item to the container
                await _container.CreateItemAsync(QuestionType);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<QuestionTypeDto>> GetQuestionTypesAsync()
        {
            try
            {
                var query = _container.GetItemQueryIterator<QuestionType>();
                var result = new List<QuestionTypeDto>();

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    result.AddRange(response.Select(q => new QuestionTypeDto
                    {
                        Id = q.Id,
                        Type = q.Type
                    }));
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<QuestionTypeDto> GetQuestionTypeAsync(Guid id)
        {
            try
            {
                var response = await _container.ReadItemAsync<QuestionType>(id.ToString(), new PartitionKey(id.ToString()));
                var result = new QuestionTypeDto
                {
                    Id = response.Resource.Id,
                    Type = response.Resource.Type
                };

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UpdateQuestionTypeAsync(Guid id, string questionType)
        {
            try
            {
                var existingItem = await GetQuestionTypeAsync(id);
                if (existingItem == null)
                {
                    return $"Question type with Id {id} does not exist";
                }

                existingItem.Type = questionType;
                var response = await _container.UpsertItemAsync(questionType);

                return $"Question type with Id {id} updated successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteQuestionTypeAsync(Guid id)
        {
            try
            {
                await _container.DeleteItemAsync<QuestionType>(id.ToString(), new PartitionKey(id.ToString()));
                return $"Question type with Id {id} deleted successfully";    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
