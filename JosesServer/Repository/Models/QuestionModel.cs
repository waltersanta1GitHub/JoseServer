using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace JosesServer.Repository.Models
{    
    public class QuestionModel
    {
        [BsonElement("questionid")]        
        public int QuestionId { get; set; }

        [BsonElement("questiontext")]
        public string QuestionText { get; set; }

        [BsonElement("options")]
        public List<string> Options { get; set; }

        [BsonElement("answers")]
        public List<string> Answers { get; set; }
       
    }
}
