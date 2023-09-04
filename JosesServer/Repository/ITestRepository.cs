using JosesServer.Repository.Models;
using System.Collections.Generic;

namespace JosesServer.Repository
{
    public interface ITestRepository
    {
        IEnumerable<TestModel> GetAll();
        TestModel GetTestById(int id);
    }
}
