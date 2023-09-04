using AutoMapper;
using JosesServer.Controllers.Poco;
using JosesServer.Repository;
using JosesServer.Repository.Models;
using JosesServer.Util;
using System.Collections.Generic;
using System.Linq;

namespace JosesServer.Services
{
    public class TestManager : ITestManager
    {
        private readonly ITestRepository _testRepository;
        private readonly ITestResultRepository _testResultRepository;
        private bool _isTestingMode = false;
        private int _lastId = 1;

        private IMapper _mapper;

        public TestManager(ITestRepository testRepository, ITestResultRepository testResultRepository, bool isTestingmode = false)
        {
            _isTestingMode = isTestingmode;
            _testRepository = testRepository;
            _testResultRepository = testResultRepository;
            _mapper = TestMapper.InitializeAutomapper();

        }

        public Question GetQuestion(int testid, int questionid)
        {
            var currentTest = new TestPoco();

            if (_isTestingMode)
            {
                currentTest = _mapper.Map<TestPoco>(MockedData.TestPocos.FirstOrDefault(x => (x.TestId == testid)));  // Unit test using Mocked Data;  
            }
            else
            {
                currentTest = _mapper.Map<TestPoco>(_testRepository.GetTestById(testid));
            }

            if (currentTest != null)
            {
                return currentTest.Questions.FirstOrDefault(q => q.QuestionId == questionid);
            }

            return null;

        }

        public TestResultsPoco CreateTestResult(TestPoco selectedTest, string username)
        {
            var testResult = new TestResultsPoco()
            {
                Id = _lastId,
                ScoreResult = 0,
                Testid = selectedTest.TestId,
                UserName = username
            };

            var testResultModel = _mapper.Map<TestResultModel>(testResult);

            if (!_isTestingMode)
            {
                _testResultRepository.AddTestResult(testResultModel);
            }

            _lastId++;

            return testResult;

        }

        public TestPoco GetTestById(int id)
        {

            var testList = new List<TestPoco>();
            if (!_isTestingMode)
            {
                testList = _mapper.Map<List<TestPoco>>(_testRepository.GetTestById(id));
                return testList.FirstOrDefault(x => x.TestId == id);
            }
            else
            {
                testList = MockedData.TestPocos;  // MockedData.data;
                return testList.FirstOrDefault(x => x.TestId.Equals(id));
            }

        }

        public TestResultsPoco GetTestResultById(int id)
        {
            var testResultList = new List<TestResultsPoco>();

            if (!_isTestingMode)
            {
                testResultList = _mapper.Map<List<TestResultsPoco>>(_testResultRepository.GetTestResultById(id));
                return testResultList.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                testResultList = MockedData.TestResultPoco; // Mocked data
                return testResultList.FirstOrDefault(x => x.Id.Equals(id));
            }

        }

        public TestResultsPoco GetTestResultById(int id, int testid)
        {

            var testResultList = new List<TestResultsPoco>();

            if (!_isTestingMode)
            {
                testResultList = _mapper.Map<List<TestResultsPoco>>(_testResultRepository.GetTestResultByIdAndTestId(id, testid));
                return testResultList.FirstOrDefault();

            }
            else
            {
                testResultList = _mapper.Map<List<TestResultsPoco>>(
                   MockedData.TestResultPoco
                   .FirstOrDefault(x => (x.Id.Equals(id)) && (x.Testid.Equals(testid)))
                   ); // Mocked Data
                return testResultList.FirstOrDefault();

            }

        }

        public bool CheckAnswer(Answer answer)
        {
            bool answerFound = false;
            Question currentQuestion = null;
            if (_isTestingMode)
            {
                currentQuestion = MockedData.TestPocos.ElementAt(0).Questions.FirstOrDefault();
            }
            else
            {
                currentQuestion = GetQuestion(answer.TestId, answer.QuestionId);
            }            

            if (currentQuestion != null)
            {
                List<string> diference = answer.AnswerContent.Intersect(currentQuestion.Answers).ToList();
                
                    if (diference.Count == currentQuestion.Answers.Count)
                    {
                        answerFound = true;
                    }                
            }
            return answerFound;
        }

        public List<TestPoco> GetAllTest()
        {
            if (!_isTestingMode)
            {
                var result = _testRepository.GetAll();
                return _mapper.Map<List<TestPoco>>(result);

            }

            return MockedData.TestPocos;
        }

        public List<TestResultsPoco> GetAllTestResults()
        {
            if (!_isTestingMode)
            {
                var result = _testResultRepository.GetAll();
                return _mapper.Map<List<TestResultsPoco>>(result);

            }

            return MockedData.TestResultPoco;

        }

    }
}
