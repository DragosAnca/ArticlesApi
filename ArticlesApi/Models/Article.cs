namespace ArticlesApi.Models
{
    public class Article
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content {  get; set; }
        public DateTime PublishDate {  get; set; }
    }
}
