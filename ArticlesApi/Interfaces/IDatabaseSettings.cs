namespace ArticlesApi.Interfaces
{
    public interface IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ArticlesCollectionName { get; set; }
    }
}
