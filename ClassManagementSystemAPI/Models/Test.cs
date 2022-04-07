using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ClassManagementSystemAPI.Models
{
    public class Test
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
