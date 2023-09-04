using JosesServer.Controllers.Poco;
using System.Collections.Generic;

namespace JosesServer.Util
{
    public class MockedData
    {

       public static List<TestPoco> TestPocos = new List<TestPoco>()
       { 
           new TestPoco {
               TestId = 1,
               TestName ="Test One",
               Questions = new List<Question>
               {
                   new Question{
                      QuestionId = 1,
                      QuestionText="Select Two familiy members",
                      Answers=new List < string >() { "Childrens","Mother" },
                      Options = new List<string>()
                      {
                          "Childrens",
                          "Bike",
                          "Mountain",
                          "Mother",
                          "Clock"
                      }
                       
                   },
                   new Question{
                       QuestionId = 2,
                      QuestionText="What is an Animal?",
                      Answers= new List<string>(){ "a Lion" },
                      Options = new List<string>()
                      {
                          "a Car",
                          "a Pencil",
                          "a Lion",
                          "a Book"
                      }

                   },
                   new Question{
                        QuestionId = 3,
                      QuestionText="What is the Sun color?",
                      Answers=new List < string >() { "Yellow" },
                      Options = new List<string>()
                      {
                          "Red",
                          "Black",
                          "Yellow",
                          "Pink"
                      }

                   },
                    new Question{
                      QuestionId = 4,
                      QuestionText="What is the Sun color?",
                      Answers=new List < string >() { "Yellow" },
                      Options = new List<string>()
                      {
                          "Red",
                          "Black",
                          "Yellow",
                          "Pink"
                      }

                   },
               },
               
               
           },
           new TestPoco {
               TestId = 2,
               TestName = "Test Two",
               Questions = new List<Question>
               {
                   new Question{
                      QuestionId = 1,
                      QuestionText="Where is Colombia located?",
                      Answers=new List < string >() { "South America" },
                      Options = new List<string>()
                      {
                          "Europe",
                          "Asia",
                          "Africa",
                          "South America",
                          "Australia"
                      }

                   },
                   new Question{
                       QuestionId = 2,
                      QuestionText="What is the result of 2+2?",
                      Answers=new List < string >() { "4" },
                      Options = new List<string>()
                      {
                          "45",
                          "2",
                          "4",
                          "8"
                      }

                   },
                   new Question{
                        QuestionId = 3,
                      QuestionText="Who is Jennifer Lopez?",
                      Answers=new List < string >() { "A Singer" },
                      Options = new List<string>()
                      {
                          "A Fireman",
                          "A Baker",
                          "A Singer",
                          "A Dog"
                      }

                   },
                   new Question{
                      QuestionId = 4,
                      QuestionText="Chose two main components of the OOP paradigm that improve security.",
                      Answers=new List < string >() { "Encapsulation","Abstraction" },
                      Options = new List<string>()
                      {
                          "Web Services",
                          "Abstraction",
                          "Encapsulation",
                          "Enums",
                          "Primitive String is an Object"
                      }

                   },
               }
           },

       };

       public static List<TestResultsPoco> TestResultPoco = new List<TestResultsPoco>()
       {
           new TestResultsPoco {
               Id = 1,
               Testid = 1,
               Answers = new Dictionary<int,List<string>>()
               {
                { 1,new List<string>{"Automovile" } },
                { 2,new List<string>{ "Book", } },
                { 3,new List < string > { "Nothing", } },
               },               
               ScoreResult = 1,
               UserName="Alejandro Santamaria" 
           },

           new TestResultsPoco {
               Id = 2,
               Testid = 1,
               Answers = new Dictionary<int, List<string>>(){
                { 1,new List < string > { "South America" }},
                { 2,new List < string > { "4" }},
                { 3,new List < string > { "A Singer" }},
                { 4,new List < string >() { "Encapsulation","Abstraction" } }
               },
               ScoreResult = 4,
               UserName="Gabriela Santamaria"
           },

           new TestResultsPoco {
               Id = 3,
               Testid = 1,
               Answers = new Dictionary<int, List<string>>(){
                { 1,new List < string > { "House" }},
                { 2,new List < string > { "Book" }},
                { 3,new List < string > { "Nothing" }},
               },
               ScoreResult = 0,
               UserName="Luz Marina Garcia"
           },

       };

    }
}
