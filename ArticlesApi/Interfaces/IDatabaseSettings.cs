namespace ArticlesApi.Interfaces
{
    /// <summary>
    /// Contains Database settings for the collections in MongoDB.
    /// Configurable in appsettings.json
    /// </summary>
    public interface IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ArticlesCollectionName { get; set; }
    }
}
