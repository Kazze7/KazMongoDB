using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace KazMongoDB.Core
{
    public interface IMongoDocument<T>
    {
        [BsonIgnore]
        T Id { get; set; }
    }

    public interface IMongoDocument<T1, T2> : IMongoDocument<T1> where T2 : IMongoDocument<T1>
    {
        [BsonIgnore]
        static IMongoCollection<T2> collection { get; set; }
        [BsonIgnore]
        IMongoCollection<T2> Collection { get { return collection; } }

        void Insert()
        {
            collection.InsertOne((T2)this);
        }
        void Update()
        {
            //collection.ReplaceOne(x => x.Id == Id, (T2)this);
            collection.ReplaceOne(Builders<T2>.Filter.Eq(x => x.Id, Id), (T2)this);
        }
        void Delete()
        {
            //collection.DeleteOne(x => (x.Id == Id));
            collection.DeleteOne(Builders<T2>.Filter.Eq(x => x.Id, Id));
        }
    }
}
