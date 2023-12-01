using TodoApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TodoApi.Services;

public class ToDoService
{
    private readonly IMongoCollection<TodoItemDTO> _todoCollection;

    public ToDoService(
        IOptions<ToDoDatabaseSettings> toDoDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            toDoDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            toDoDatabaseSettings.Value.DatabaseName);

        _todoCollection = mongoDatabase.GetCollection<TodoItemDTO>(
            toDoDatabaseSettings.Value.ToDoCollectionName);
    }

    public async Task<List<TodoItemDTO>> GetAsync() =>
        await _todoCollection.Find(_ => true).ToListAsync();

    public async Task<TodoItemDTO?> GetAsync(string id) =>
        await _todoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TodoItemDTO newToDo) =>
        await _todoCollection.InsertOneAsync(newToDo);

    public async Task UpdateAsync(string id, TodoItemDTO updatedToDo) =>
        await _todoCollection.ReplaceOneAsync(x => x.Id == id, updatedToDo);

    public async Task RemoveAsync(string id) =>
        await _todoCollection.DeleteOneAsync(x => x.Id == id);
}