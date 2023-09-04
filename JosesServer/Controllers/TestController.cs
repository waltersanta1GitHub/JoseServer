using JosesServer.Controllers.Poco;
using JosesServer.Services;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JosesServer.Controllers
{
    public class TestController
    {
        readonly ITestManager _testManager;


        public TestController(ITestManager testManager) { 

            _testManager = testManager;           
        
        }

        public string GetAllTest()
        {           
            return JsonConvert.SerializeObject(_testManager.GetAllTest());
        }

        public string GetTestById(int id)
        {
            return JsonConvert.SerializeObject(_testManager.GetTestById(id));
        }

        public string GetQuestionById(int testid, int questionid)
        {
            return JsonConvert.SerializeObject( _testManager.GetQuestion(testid,questionid));
        }

        public bool CheckAnswer(int testid, int questionid, List<string> answer)
        {

           return _testManager.CheckAnswer( new Answer
            {
                TestId = testid,
                QuestionId = questionid,
                AnswerContent= answer
            });

        }
    }
}
