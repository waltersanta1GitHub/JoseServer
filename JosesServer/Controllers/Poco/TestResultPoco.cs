using Newtonsoft.Json;
using System.Collections.Generic;

namespace JosesServer.Controllers.Poco
{
    public class TestResultsPoco
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("testid")]
        public int Testid { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("answers")]
        public Dictionary<int,List<string>> Answers { get; set; }

        [JsonProperty("scoreresult")]
        public int ScoreResult { get; set; }
    }
}
