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
            IMongoDocument<Account>.Collection = mongoDatabase.GetCollection<Account>("Account");

            Account account = new Account();
            account.Id = 1;
            account.Database.Insert();
            account.name = "New user";
            account.Database.Update();
            account = IMongoDocument<Account>.SelectOne(x => x.Id == 1);
            account.Database.Delete();

            Console.ReadLine();
        }
    }
    internal class Account : IMongoDocument<ulong, Account>
    {
        public IMongoDocument<ulong, Account> Database => this;
        public ulong Id { get => id; set => id = value; }

        [BsonIgnore]
        private ulong id;
        public string name = "User";
    }
}
