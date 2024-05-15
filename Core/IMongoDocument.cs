using MongoDB.Driver;

namespace KazMongoDB.Core
{
    public interface IMongoDocument<T1>
    {
        T1 Id { get; set; }
    }
    public interface IMongoDocument<T1, T2> : IMongoDocument<T1> where T2 : IMongoDocument<T1>
    {
        static IMongoCollection<T2> MongoCollection;
        IMongoCollection<T2> Collection { get => MongoCollection; }
        IMongoDocument<T1, T2> Database { get; }

        void Insert()
        {
            Collection.InsertOne((T2)this);
        }
        void Update()
        {
            Collection.ReplaceOne(Builders<T2>.Filter.Eq(x => x.Id, Id), (T2)this);
        }
        void Delete()
        {
            Collection.DeleteOne(Builders<T2>.Filter.Eq(x => x.Id, Id));
        }
    }
}
