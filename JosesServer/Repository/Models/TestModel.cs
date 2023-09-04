using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;

namespace JosesServer.Repository.Models
{    
    public class TestModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]        
        public ObjectId _Id { get; set; }

        [BsonElement("testid")]       
        public int TestId { get; set; }

        [BsonElement("testname")]
        public string TestName { get; set; }

        [BsonElement("questions")]
        public List<QuestionModel> Questions { get; set; }
       
    }
}
