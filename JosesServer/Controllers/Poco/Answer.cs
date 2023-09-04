using Newtonsoft.Json;
using System.Collections.Generic;

namespace JosesServer.Controllers.Poco
{
    public class Answer
    {
        
            [JsonProperty("questionid")]
            public int QuestionId { get; set; }

            [JsonProperty("testid")]
            public int  TestId { get; set; }            

            [JsonProperty("answer")]
            public List<string> AnswerContent { get; set; }
       
    }
}
