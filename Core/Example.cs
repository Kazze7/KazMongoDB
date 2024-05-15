using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace KazMongoDB.Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MongoClient mongoClient = new MongoClient("connectionString");
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase("Auth");
            
            //  custom interface
            IMongoDocument<ulong, Account>.MongoCollection = mongoDatabase.GetCollection<Account>("Account");

            Account account = new Account();
            account.id = 1;
            account.Database.Insert();
            account.name = "New user";
            account.Database.Update();
            account.Database.Delete();

            Console.ReadLine();
        }
    }
    internal class Account : IMongoDocument<ulong, Account>
    {
        public IMongoDocument<ulong, Account> Database { get => this; }
        public ulong Id { get => id; set => id = value; }

        [BsonIgnore]
        public ulong id;
        public string name = "User";
    }
}
