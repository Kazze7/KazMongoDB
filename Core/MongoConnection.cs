using MongoDB.Driver;

namespace KazMongoDB.Core
{
    public class MongoConnection
    {
        MongoClient mongoClient;

        public MongoConnection(string _connectionString)
        {
            mongoClient = new MongoClient(_connectionString);
        }
        public void SetCollection<T>(string _collectionName, string _databaseName) where T : IMongoDocument<T>
        {
            IMongoDocument<T>.Collection = mongoClient.GetDatabase(_databaseName).GetCollection<T>(_collectionName);
        }
        public void SetIndex<T>(IndexKeysDefinition<T> _keys) where T : IMongoDocument<T>
        {
            IMongoDocument<T>.Collection.Indexes.CreateOne(new CreateIndexModel<T>(_keys));
        }
    }
}
