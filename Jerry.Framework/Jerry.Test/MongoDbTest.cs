using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jerry.System.MongoDb;
using MongoDB.Bson;
using NUnit.Framework;

namespace Jerry.Test
{
    [TestFixture]
    public class MongoDbTest
    {
        private IMongoRepository<person> repository;
        public MongoDbTest()
        {
             repository = MongoRepositoryFactory<person>.CreateRepository("Test", "Person");
        }

        [Test]
        public void Count()
        {

            var objectid= repository.Insert(new person()
            {
                Age = 10,
                Name = "张三"
            });

            repository.Update(new person()
            {
                Id=objectid,
                Age = 1001,
                Name = "张三"
            });

            var count= repository.Count();
            Assert.IsTrue(count>0);
        }
    }


    public class person : IMongoDocument
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime birthday { get; set; }
    }
}
