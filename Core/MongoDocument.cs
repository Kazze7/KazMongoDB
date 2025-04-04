﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace KazMongoDB.Core
{
    public abstract class MongoDocument<T1, T2> : IMongoDocument<T2> where T2 : MongoDocument<T1, T2>
    {
        [BsonIgnore] public static IMongoCollection<T2> Collection { get { return IMongoDocument<T2>.Collection; } }
        [BsonId] public T1 Id { get; set; }

        public void Insert()
        {
            Collection.InsertOne((T2)this);
        }
        public void Update()
        {
            Collection.ReplaceOne(Builders<T2>.Filter.Eq(x => x.Id, Id), (T2)this);
        }
        public void Delete()
        {
            Collection.DeleteOne(Builders<T2>.Filter.Eq(x => x.Id, Id));
        }
        public static List<T2> SelectAll()
        {
            return Collection.Find(Builders<T2>.Filter.Empty).ToList();
        }
        public static T2 SelectByID(T1 _id)
        {
            return Collection.Find(x => x.Id.Equals(_id)).FirstOrDefault();
        }
    }
}
