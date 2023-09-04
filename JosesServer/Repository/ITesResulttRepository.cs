using JosesServer.Repository.Models;
using System.Collections.Generic;

namespace JosesServer.Repository
{
    public interface ITestResultRepository
    {
        IEnumerable<TestResultModel> GetAll();
        TestResultModel GetTesResultBykeyId(string keyid);
        TestResultModel GetTestResultById(int id);
        TestResultModel GetTestResultByIdAndTestId(int id, int testid);
        void AddTestResult(TestResultModel testResult);
        void UpdateTestResult(TestResultModel testResult);       

    }
}
