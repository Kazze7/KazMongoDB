using MongoDB.Driver;

namespace KazMongoDB.Core
{
    public class MongoConnection
    {
        MongoClient mongoClient;
        IMongoDatabase database;

        public MongoConnection(string _connectionString, string _databaseName)
        {
            mongoClient = new MongoClient(_connectionString);
            database = mongoClient.GetDatabase(_databaseName);
        }
        public void SetCollection<T>(string _collectionName) where T : IMongoDocument<T>
        {
            IMongoDocument<T>.Collection = database.GetCollection<T>(_collectionName);
        }
        public void SetIndex<T>(IndexKeysDefinition<T> _keys) where T : IMongoDocument<T>
        {
            IMongoDocument<T>.Collection.Indexes.CreateOne(new CreateIndexModel<T>(_keys));
        }
    }
}
