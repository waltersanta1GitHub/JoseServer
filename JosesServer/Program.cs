using JosesServer.Controllers;
using JosesServer.Repository;
using JosesServer.Services;
using JosesServer.UnitTests;
using JosesServer.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace JosesServer
{
    class Program
    {
        static void Main(string[] args)
        {
            RuntUnitTest();
            StartServer();
        }


        static void RuntUnitTest()
        {
            try
            {
                // TestManager UnitTest
                TestManagerUnitTest testManagerTest = new TestManagerUnitTest();
                testManagerTest.Given_A_Valid_TestId_Return_A_Test();
                testManagerTest.Given_A_Valid_Test_And_Username_Return_A_TestResult_Instance();
                testManagerTest.Given_A_Valid_TestId_And_TestResultId_Return_A_Test();
                testManagerTest.When_A_Valid_Question_Do_CheckAnswer();

            }
            catch (Exception ex)
            {
                Console.WriteLine("-- UNITESTING FAILED --");
                Console.WriteLine(ex.Message.ToString());
                throw ex;
            }
        }

        static void StartServer()
        {
            // Define the URL to listen on
            string url = "http://localhost:8081/";

            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add(url);
                listener.Start();
                Console.WriteLine($"Jose Server Test listening on {url}");

                while (true)
                {
                    HttpListenerContext context = listener.GetContext();
                    Task.Run(() => HandleRequest(context));
                }
            }
        }

        static void HandleRequest(HttpListenerContext context)
        {
            AllowCors(context);
            
            string responseText = "";
            string token = context.Request.Headers["Authorization"];
            if (!string.IsNullOrWhiteSpace(token) && token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = token.Replace("Bearer ", string.Empty);
                if (UtilCollection.ValidateToken(token))
                {

                    ProcessRequest(context, responseText);

                }
                else
                {
                    responseText = "Invalid token";
                    
                    byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.ContentType = "application/json";
                    context.Response.ContentEncoding = Encoding.UTF8;
                    context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                    context.Response.Close();
                }
            }
            else
            {
                if (context.Request.HttpMethod != "OPTIONS")
                {
                    responseText = $"Bearer token is required. This is a new valid token for a TestUser -->  {UtilCollection.GenerateToken("testuser")}";
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }

                byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);
                context.Response.StatusCode = (int)HttpStatusCode.OK;               
                context.Response.ContentType = "application/json";
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                context.Response.Close();
            }

        }


        static void ProcessRequest(HttpListenerContext context, string responseText)
        {
            var testManager = new TestManager(
                new TestRespository(UtilCollection.CONNECTION) { },
                new TestResultRepository(UtilCollection.CONNECTION) { });
            var testController = new TestController(testManager);


            if (context.Request.HttpMethod == "GET")
            {

                string allTestList = context.Request.QueryString.Get("alltestlist");
                if (allTestList.Equals("*"))
                {
                    responseText = testController.GetAllTest();
                }

                string testid = context.Request.QueryString.Get("testid");
                if (!string.IsNullOrEmpty(testid))
                {
                    responseText = testController.GetTestById(int.Parse(testid));
                }

                string questionid = context.Request.QueryString.Get("questionid");
                if (!string.IsNullOrEmpty(questionid))
                {
                    responseText = testController.GetQuestionById(int.Parse(testid), int.Parse(questionid));
                }

                if (string.IsNullOrEmpty(testid) && string.IsNullOrEmpty(responseText))
                {
                    responseText = "parameter is required for Get method and [questionid] parameter is optional.This is an example: http://localhost:8081?testid=1, Or http://localhost:8081?alltestlist=*";

                }


            }
            else if (context.Request.HttpMethod == "POST")
            {

                string postData;
                using (var reader = new System.IO.StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                {
                    postData = reader.ReadToEnd();
                }

                dynamic requestData = JsonConvert.DeserializeObject(postData);
                if (requestData != null)
                {
                    int testid = requestData.testid != null ? requestData.testid : 0;
                    int questionid = requestData.questionid != null ? requestData.questionid : 0;
                    var result = requestData.answercontent != null ? requestData.answercontent : null;
                    if (result == null)
                    {
                        responseText = "false";
                    }
                    else
                    {
                        var answer = new List<string>();
                        for (int i = 0; i < result.Count; i++)
                        {
                            answer.Add(result[i].ToString());
                        }

                        responseText = testController.CheckAnswer(testid, questionid, answer).ToString().ToLower();
                    }
                }

                if (requestData == null)
                {
                    responseText = "A json request body parameter is required for Post method.This is an example: { 'testId':1,'questionid':2,'answerContent':'My answer is Yes' }";
                }
            
            }

            byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
            context.Response.Close();

        }

        private static void AllowCors(HttpListenerContext context)
        {
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.AppendHeader("Access-Control-Allow-Credentials", "true");
            context.Response.AppendHeader("Access-Control-Allow-Methods", "GET,HEAD,POST,OPTIONS");
            context.Response.AppendHeader("Access-Control-Allow-Headers", "Origin,Content-Type,Accept,Authorization");
        }
        
    }


}
