using MongoDB.Driver;
using System.Linq.Expressions;

namespace KazMongoDB.Core
{
    public interface IMongoDocument<T1>
    {
        static IMongoCollection<T1> Collection;

        static T1 SelectOne(Expression<Func<T1, bool>> _filter)
        {
            return Collection.Find(_filter).FirstOrDefault();
        }
        static T1 SelectOne(FilterDefinition<T1> _filter)
        {
            return Collection.Find(_filter).FirstOrDefault();
        }
        static List<T1> SelectMany(Expression<Func<T1, bool>> _filter)
        {
            return Collection.Find(_filter).ToList();
        }
        static List<T1> SelectMany(FilterDefinition<T1> _filter)
        {
            return Collection.Find(_filter).ToList();
        }
        static List<T1> SelectAll()
        {
            return Collection.Find(Builders<T1>.Filter.Empty).ToList();
        }
    }
    public interface IMongoDocument<T1, T2> : IMongoDocument<T2> where T2 : IMongoDocument<T1, T2>
    {
        T1 Id { get; set; }
        IMongoCollection<T2> Collection { get => IMongoDocument<T2>.Collection; }
        IMongoDocument<T1, T2> Database { get; }

        static T2 SelectOneById(T1 _id)
        {
            return IMongoDocument<T2>.Collection.Find(Builders<T2>.Filter.Eq(x => x.Id, _id)).FirstOrDefault();
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
