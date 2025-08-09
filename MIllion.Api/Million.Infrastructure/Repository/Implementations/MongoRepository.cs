using Microsoft.Extensions.Options;
using Million.Data.Interfaces;
using Million.Data.Response;
using Million.Infrastructure.Mongo;
using MongoDB.Driver;

namespace Million.Infrastructure.Repository.Implementations
{
    public class MongoRepository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<T>(typeof(T).Name);
        }
        public async Task<ActionResponse<T>> GetByIdAsync(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var data = await _collection.Find(filter).FirstOrDefaultAsync();

            if (data == null)
            {
                return new ActionResponse<T>
                {
                    Success = false,
                    Message = "Entity not found"
                };
            }

            return new ActionResponse<T>
            {
                Success = true,
                Result = data
            };

        }

        public async Task<ActionResponse<IEnumerable<T>>> GetAllAsync()
        {
            var data = await _collection.Find(_ => true).ToListAsync();
            return new ActionResponse<IEnumerable<T>>
            {
                Success = true,
                Result = data
            };
        }

        public async Task<ActionResponse<T>> AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);

            return new ActionResponse<T>
            {
                Message = "Entity added successfully",
                Success = true,
            };
        }

        public async Task<ActionResponse<T>> UpdateAsync(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");

            if (idProperty == null)
            {
                return new ActionResponse<T>
                {
                    Message = "Not found",
                    Success = false,
                };
            }
            var id = (Guid)idProperty.GetValue(entity);
            var filter = Builders<T>.Filter.Eq("Id", id);
            await _collection.ReplaceOneAsync(filter, entity);
            return new ActionResponse<T>
            {
                Message = "Entity updated successfully",
                Success = true,
            };
        }

        public async Task<ActionResponse<bool>> DeleteAsync(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            if (filter == null)
            {
                return new ActionResponse<bool>
                {
                    Message = "Not found",
                    Success = false,
                };
            }
            await _collection.DeleteOneAsync(filter);
            return new ActionResponse<bool>
            {
                Message = "Entity deleted successfully",
                Success = true,
            };
        }
    }
}
