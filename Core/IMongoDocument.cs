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

        static T2 SelectOne(FilterDefinition<T2> _filter)
        {
            return MongoCollection.Find(_filter).FirstOrDefault();
        }
        static T2 SelectOneById(T1 _id)
        {
            return MongoCollection.Find(Builders<T2>.Filter.Eq(x => x.Id, _id)).FirstOrDefault();
        }
        static List<T2> SelectMany(FilterDefinition<T2> _filter)
        {
            return MongoCollection.Find(_filter).ToList();
        }
        static List<T2> SelectAll()
        {
            return MongoCollection.Find(Builders<T2>.Filter.Empty).ToList();
        }
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
