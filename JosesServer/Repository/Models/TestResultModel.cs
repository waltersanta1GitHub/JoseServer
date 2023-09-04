using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;


namespace JosesServer.Repository.Models
{
    public class TestResultModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId KeyId { get; set; }       
        
        public int TestResultId { get; set; }

        public int TestId { get; set; }
                
        public string UserName { get; set; }
       
        public Dictionary<int, string> Answers { get; set; }
        
        public int ScoreResult { get; set; }
    }
}
