using MongoDB.Bson;

namespace Jerry.System.MongoDb
{
    public interface IMongoDocument
    {
        ObjectId Id { get; set; }
    }
}
