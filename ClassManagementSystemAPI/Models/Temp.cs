using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ClassManagementSystemAPI.Models
{
    public class Temp
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
