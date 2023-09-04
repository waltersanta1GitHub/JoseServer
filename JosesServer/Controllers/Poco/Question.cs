using Newtonsoft.Json;
using System.Collections.Generic;

namespace JosesServer.Controllers.Poco
{
    public class Question
    {
       [JsonProperty("questionid")]
       public int QuestionId { get; set; }
       
       [JsonProperty("questiontext")]
       public string QuestionText { get; set; }
       
        [JsonProperty("options")]
       public List<string> Options { get; set; }
       
        [JsonProperty("answers")]
       public List<string> Answers { get; set; }
    }
}