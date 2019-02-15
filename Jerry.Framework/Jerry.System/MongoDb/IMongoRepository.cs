using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Jerry.System.MongoDb
{
    public interface IMongoRepository<TDocument>
    {
        TDocument Find(FilterDefinition<TDocument> filter);
        IEnumerable<TDocument> FindByPage(int pageIndex, int pageSize);
        IEnumerable<TDocument> FindByPage(int pageIndex, int pageSize, FilterDefinition<TDocument> filter);
        IEnumerable<TDocument> FindByPage(int pageIndex, int pageSize, FilterDefinition<TDocument> filter, SortDefinition<TDocument> sorter);
        IEnumerable<TDocument> FindAll(FilterDefinition<TDocument> filter);
        IEnumerable<TDocument> GetAll();
        TDocument GetFirst();
        TDocument GetFirst(SortDefinition<TDocument> sorter);
        TDocument Get(string id);
        ObjectId Insert(TDocument doc);
        void InsertMany(IEnumerable<TDocument> docs);
        void Delete(TDocument doc);
        void Delete(string id);
        void Update(TDocument doc);
        void Clear();
        long Count();
        long Count(FilterDefinition<TDocument> filter);
        void SetCollectionName(string collectionName);
        BsonDocument ToBsonDocumentFromJson(string json);
    }
}
