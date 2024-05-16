using MongoDB.Driver;

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

            Account account1 = new Account();
            account1.Id = 1;
            account1.Database.Insert();
            account1.name = "New user";
            account1.Database.Update();

            Account account2 = new Account();
            account2.Id = 2;
            account2.friends.Add(new MongoLink<ulong, Account>(account1));
            account2.Database.Insert();
            account1 = IMongoDocument<Account>.Collection.Find(x => x.Id == 1).FirstOrDefault();
            account1.Database.Delete(); 
            account2.Database.Delete();

            Console.ReadLine();
        }
    }
    internal class Account : IMongoDocument<ulong, Account>
    {
        public IMongoDocument<ulong, Account> Database => this;
        public ulong Id { get => id; set => id = value; }

        private ulong id;
        public string name = "User";
        public List<MongoLink<ulong, Account>> friends = new List<MongoLink<ulong, Account>>();
    }
}
