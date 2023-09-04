using JosesServer.Repository.Models;
using JosesServer.Util;
using MongoDB.Driver;
using System.Collections.Generic;

namespace JosesServer.Repository
{
    public class TestResultRepository : ITestResultRepository
    {
        private readonly IMongoCollection<TestResultModel> _testResultCollection;
        public TestResultRepository(string connection)
        {
            var client = new MongoClient(connection);
            var database = client.GetDatabase(UtilCollection.DATABASE_NAME);           
            _testResultCollection = database.GetCollection<TestResultModel>("testresults");

        }
        public void AddTestResult(TestResultModel testResult)
        {
            _testResultCollection.InsertOne(testResult);
        }

        public IEnumerable<TestResultModel> GetAll()
        {
          return  _testResultCollection.Find(tr => true).ToList();
        }

        public TestResultModel GetTesResultBykeyId(string keyid)
        {
            throw new System.NotImplementedException();
        }

        public TestResultModel GetTestResultById(int id)
        {
            return _testResultCollection.Find(x => x.TestResultId == id).FirstOrDefault();
        }

        public TestResultModel GetTestResultByIdAndTestId(int id, int testid)
        {
            return _testResultCollection.Find(x => (x.TestResultId == id ) && (x.TestId == testid) ).FirstOrDefault();

        }

        public void UpdateTestResult(TestResultModel testResult)
        {
            throw new System.NotImplementedException();
        }
    }
}
