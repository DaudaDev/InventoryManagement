using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blocks.MongoDb.Models;

public abstract class MongoDbBaseDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public Guid EntityId { get; set; }

}