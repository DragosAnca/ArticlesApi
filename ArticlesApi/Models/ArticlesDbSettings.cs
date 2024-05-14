using ArticlesApi.Interfaces;

namespace ArticlesApi.Models
{
    /// <summary>
    /// Contains Database settings for the Articles collection.
    /// Configured in appsettings.json
    /// </summary>
    public class ArticleDbSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ArticlesCollectionName { get; set; }
    }
}
