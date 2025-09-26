using MongoDB.Bson;
using Backend.Courses.Application.Common;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Backend.Courses.Infrastructure.Persistence;

public class MongoRepository<T> : IRepository<T>
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        var objectId = ObjectId.Parse(id);
        var filter = Builders<T>.Filter.Eq("_id", objectId);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetAllAsync() =>
        await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();

    public async Task AddAsync(T entity) =>
        await _collection.InsertOneAsync(entity);

    public async Task UpdateAsync(string id, T entity)
    {
        var objectId = ObjectId.Parse(id);
        var filter = Builders<T>.Filter.Eq("_id", objectId);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var objectId = ObjectId.Parse(id);
        var filter = Builders<T>.Filter.Eq("_id", objectId);
        await _collection.DeleteOneAsync(filter);
    }

    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter) =>
        await _collection.Find(filter).ToListAsync();
}
