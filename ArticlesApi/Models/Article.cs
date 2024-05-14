using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ArticlesApi.Models
{
    public class Article
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Content {  get; set; }
        public DateTime PublishDate {  get; set; }
    }
}
