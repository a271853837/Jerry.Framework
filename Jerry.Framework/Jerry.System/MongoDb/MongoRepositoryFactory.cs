using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.System.MongoDb
{
    public class MongoRepositoryFactory<TDocument> where TDocument : class, IMongoDocument
    {
        public static IMongoRepository<TDocument> CreateRepository(string dbName)
        {
            return new MongoRepository<TDocument>(dbName);
        }

        public static IMongoRepository<TDocument> CreateRepository(string dbName,string collectionName)
        {
            MongoRepository<TDocument> rep = new MongoRepository<TDocument>(dbName);
            rep.SetCollectionName(collectionName);
            return rep;
        }
    }
}
