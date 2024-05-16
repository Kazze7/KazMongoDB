using MongoDB.Driver;

namespace KazMongoDB.Core
{
    public interface IMongoDocument<T1>
    {
        static IMongoCollection<T1> Collection;
    }
    public interface IMongoDocument<T1, T2> : IMongoDocument<T2> where T2 : IMongoDocument<T1, T2>
    {
        T1 Id { get; set; }
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
