using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ArticlesApi.Models
{
    public class Article
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Content {  get; set; }
        public DateTime PublishDate {  get; set; } = DateTime.UtcNow;
    }
}
