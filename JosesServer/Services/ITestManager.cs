using JosesServer.Controllers.Poco;
using System.Collections.Generic;

namespace JosesServer.Services
{
    public interface ITestManager
    {
       TestPoco GetTestById(int id);
       Question GetQuestion(int testid,int questionid);
       TestResultsPoco GetTestResultById(int id, int testid);
       bool CheckAnswer(Answer answer);
       TestResultsPoco CreateTestResult(TestPoco selectedTest, string username);
       List<TestPoco> GetAllTest();
    }
}
