using JosesServer.Repository.Models;
using JosesServer.Util;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace JosesServer.Repository
{
    public class TestRespository : ITestRepository
    {
        private readonly IMongoCollection<TestModel> _testCollection;
        public TestRespository(string connection)
        {
            var client = new MongoClient(connection);
            var database = client.GetDatabase(UtilCollection.DATABASE_NAME);            
            _testCollection = database.GetCollection<TestModel>("tests");
            
        }       

        public IEnumerable<TestModel> GetAll()
        {
            return _testCollection.Find(test => true).ToList();
        }

        public TestModel GetTestById(int id)
        {
            return _testCollection.Find(test => test.TestId== id).FirstOrDefault();
        }
       
    }
}
