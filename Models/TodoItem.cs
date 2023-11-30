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