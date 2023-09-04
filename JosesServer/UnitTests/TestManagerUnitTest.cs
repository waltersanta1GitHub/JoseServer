using AutoMapper;
using JosesServer.Controllers.Poco;
using JosesServer.Services;
using JosesServer.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JosesServer.UnitTests
{
    public class TestManagerUnitTest
    {

        TestManager _currentTestManager;
        private Mapper _mapper;

        public TestManagerUnitTest()
        {
            // Create a TestManager instance to run the Unit Tests
            _currentTestManager = new TestManager(null, null,true);         

        }


        public void Given_A_Valid_Test_And_Username_Return_A_TestResult_Instance()
        {
            TestPoco testPoco = MockedData.TestPocos.ElementAt(0);
            string userName = "User Test One";
            var testResultPoco = _currentTestManager.CreateTestResult(testPoco, userName);

            if (testResultPoco == null)
            {
                throw new Exception($"UnitTesting - CreateTestResult Method is throwing a NULL value." +
                    $" ExpectedValue= TestResutlPoco Object Value. ResultValue= {testResultPoco}");
            }

            if (!testResultPoco.UserName.Equals(userName) || !testResultPoco.ScoreResult.Equals(0)) 
            {
                throw new Exception(
                    $"UnitTesting - CreateTestResult Method is throwing a wrong resul value." +
                    $" ExpectedValues= UserName:{userName}, ScoreResult:{0}. ResultValue= UserName:{testResultPoco.UserName}, ScoreResult:{testResultPoco.ScoreResult}");
            }

        }




        public void Given_A_Valid_TestId_Return_A_Test()
        {
           var firstResult =  _currentTestManager.GetTestById(1);

            if (firstResult == null)
            {
                throw new Exception($"UnitTesting - GetTestById Method is throwing a NULL value." +
                    $" ExpectedValue= TestPoco Object Value. ResultValue= {firstResult}");
            }

            firstResult = _currentTestManager.GetTestById(0);

            if (firstResult != null)
            {
                throw new Exception($"UnitTesting - GetTestById Method is throwing Not NULL value." +
                    $" ExpectedValue= NULL. ResultValue={firstResult}");
            }
        }


        public void Given_A_Valid_TestId_And_TestResultId_Return_A_Test() 
        {
            var test = _currentTestManager.GetTestById(1);

            if (test == null)
            {
                throw  new Exception("UnitTesting - Invalid testId");
            }

        }

        public void When_A_Valid_Question_Do_CheckAnswer()
        {
            Answer answer = new Answer() { 
            
                TestId = 1,
                QuestionId = 1,
                AnswerContent = new List<string>() { "Bike" }
            };

            var result  = _currentTestManager.CheckAnswer(answer);

            if (result == true)
            {
                throw new Exception($"UnitTesting - CheckAnswer Method is throwing True invalid value." +
                    $" ExpectedValue= False. ResultValue= {result}");
            }

            answer.AnswerContent = new List<string>() { "Mountain", "Mother" };

            result = _currentTestManager.CheckAnswer(answer);

            if (result == true)
            {
                throw new Exception($"UnitTesting - CheckAnswer Method is throwing True invalid value." +
                    $" ExpectedValue= False. ResultValue= {result}");
            }

            answer.AnswerContent = new List<string>() { "Childrens", "Mother" };

            result = _currentTestManager.CheckAnswer(answer);

            if (result == false)
            {
                throw new Exception($"UnitTesting - CheckAnswer Method is throwing False invalid value." +
                    $" ExpectedValue= True. ResultValue={result}");
            }


        }

    }
}
