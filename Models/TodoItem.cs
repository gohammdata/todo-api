using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi.Models;

public class TodoItemDTO
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set; }
    [BsonElement("Name")]
    public string? Name { get; set; }
    public bool IsComplete {get; set; }
}

public class ToDoDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ToDoCollectionName { get; set; } = null!;
}