using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace KazMongoDB.Core
{
    public class MongoLink<T1, T2> where T2 : IMongoDocument<T1, T2>
    {
        private T1 id;
        private T2 document;

        public MongoLink() { }
        public MongoLink(T2 _document)
        {
            Document = _document;
        }

        [BsonIgnore]
        public T2 Document
        {
            get
            {
                if (document == null)
                    GetDocument();
                return document;
            }
            set
            {
                document = value;
                id = value.Id;
            }
        }
        public T1 Id
        {
            get
            {
                if (document == null)
                    return id;
                else
                    return document.Id;
            }
            set
            {
                if (document == null)
                    id = value;
                else
                    document.Id = value;
            }
        }

        void GetDocument()
        {
            Document = IMongoDocument<T2>.Collection.Find(Builders<T2>.Filter.Eq(x => x.Id, Id)).FirstOrDefault();
        }
    }
}
