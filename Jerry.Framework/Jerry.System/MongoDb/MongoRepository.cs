using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jerry.System.Log;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Jerry.System.MongoDb
{
    public class MongoRepository<TDocument> : BaseMongoRepository<TDocument>, IMongoRepository<TDocument> where  TDocument : class, IMongoDocument
    {
        private ILog log = LogManager.GetLogger(typeof(MongoRepository<TDocument>));
        public MongoRepository(string dbName) : base(dbName)
        {

        }

        public void Clear()
        {
            try
            {
                Collection.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public long Count()
        {
            try
            {
                return Collection.CountDocumentsAsync(new BsonDocument()).Result;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public long Count(FilterDefinition<TDocument> filter)
        {
            try
            {
                return Collection.CountDocumentsAsync(filter).Result;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public void Delete(TDocument doc)
        {
            try
            {
                 Collection.DeleteOneAsync(c=>c.Id.Equals(doc.Id));
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public void Delete(string id)
        {
            try
            {
                ObjectId oid = new ObjectId(id);
                var result = Collection.DeleteOneAsync(m => m.Id.Equals(oid)).Result;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public TDocument Find(FilterDefinition<TDocument> filter)
        {
            try
            {
                return Collection.Find(filter).FirstOrDefaultAsync().Result;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public IEnumerable<TDocument> FindAll(FilterDefinition<TDocument> filter)
        {
            try
            {
                return Collection.FindAsync(filter).Result.ToList();
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public IEnumerable<TDocument> FindByPage(int pageIndex, int pageSize)
        {
            return FindByPage(pageIndex, pageSize, new BsonDocument());
        }

        public IEnumerable<TDocument> FindByPage(int pageIndex, int pageSize, FilterDefinition<TDocument> filter)
        {
            try
            {
                return Collection.Find(filter).Skip(pageIndex * pageSize).Limit(pageSize).ToListAsync().Result;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public IEnumerable<TDocument> FindByPage(int pageIndex, int pageSize, FilterDefinition<TDocument> filter, SortDefinition<TDocument> sorter)
        {
            try
            {
                return Collection.Find(filter).Sort(sorter).Skip(pageIndex * pageSize).Limit(pageSize).ToListAsync().Result;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public TDocument Get(string id)
        {
            try
            {
                ObjectId oid = new ObjectId(id);
                return Collection.Find(m => m.Id.Equals(oid)).FirstOrDefaultAsync().Result;
            }
            catch (Exception e)
            {
                log.Error(e);
                return null;
            }
        }

        public IEnumerable<TDocument> GetAll()
        {
            try
            {
                return Collection.Find(new BsonDocument()).ToListAsync().Result;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public TDocument GetFirst()
        {
            try
            {
                return Collection.Find(new BsonDocument()).FirstOrDefaultAsync().Result;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public TDocument GetFirst(SortDefinition<TDocument> sorter)
        {
            try
            {
                return Collection.Find(new BsonDocument()).Sort(sorter).FirstOrDefaultAsync().Result;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public ObjectId Insert(TDocument doc)
        {
            try
            {
                doc.Id = CreateNewObjectId();
                Collection.InsertOneAsync(doc);
                return doc.Id;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public void InsertMany(IEnumerable<TDocument> docs)
        {
            try
            {
                Collection.InsertManyAsync(docs);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }



        public void Update(TDocument doc)
        {
            try
            {
                var result = Collection.ReplaceOneAsync(m => m.Id.Equals(doc.Id), doc).Result;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }
    }
}
