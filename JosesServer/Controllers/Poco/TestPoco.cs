using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace JosesServer.Controllers.Poco
{
    public class TestPoco
    {
        [JsonProperty("testid")]
        public int TestId { get; set; }

        [JsonProperty("testname")]
        public string TestName { get; set; }


        [JsonProperty("questions")]
        public List<Question> Questions { get; set; }
        
    }
}
