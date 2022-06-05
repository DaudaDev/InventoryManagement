using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blocks.MongoDb.Models;

public abstract record MongoDbBaseDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}