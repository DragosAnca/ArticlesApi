using ArticlesApi.Interfaces;

namespace ArticlesApi.Models
{
    public class ArticleDbSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ArticlesCollectionName { get; set; }
    }
}
