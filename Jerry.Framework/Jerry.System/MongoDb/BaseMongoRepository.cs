using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Jerry.System.MongoDb
{
    public class BaseMongoRepository<TDocument>
    {
        private string dbName;
        protected string collectionName = "default";

        public BaseMongoRepository(string dbName)
        {
            this.dbName = dbName;
        }

        protected IMongoDatabase Database
        {
            get
            {
                IMongoDatabase db = MongoClientManager.GetClient.GetDatabase(dbName);
                return db;
            }
        }

        public IMongoCollection<TDocument> Collection
        {
            get
            {
                return Database.GetCollection<TDocument>(collectionName);
            }
        }

        public void SetCollectionName(string collectionName)
        {
            this.collectionName = collectionName;
        }

        protected ObjectId CreateNewObjectId()
        {
            return new ObjectId(Guid.NewGuid().ToString("N"));
        }

        public BsonDocument ToBsonDocumentFromJson(string json)
        {
            return BsonSerializer.Deserialize<BsonDocument>(json);
        }
    }
}
