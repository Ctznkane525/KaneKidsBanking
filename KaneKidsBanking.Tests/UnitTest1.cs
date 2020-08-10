using KaneKidsBanking.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KaneKidsBanking.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public IConfiguration Config { get; private set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.Config = InitConfiguration();
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }

        [TestMethod]
        public void OpenCosmos()
        {

            var c = new CosmosDb(Config["endPointUrl"], Config["primaryKey"]);
            var acct = c.GetAccountByName("Test");
            var transactions = new List<Transaction>();
            transactions.Add(new Transaction() { Amount = 2, Name = "Hello 1", TransDate = new System.DateTime(2020, 02, 20), TransactionId = System.Guid.NewGuid() });
            transactions.Add(new Transaction() { Amount = 3, Name = "Hello 2", TransDate = new System.DateTime(2020, 02, 21), TransactionId = System.Guid.NewGuid() });
            acct.Transactions = transactions.ToArray();
            c.UpsertAccount(acct);


            foreach (var name in new[] { "Autumn", "Lilly" })
            {
                if (!c.AccountExists(name))
                {
                    var acct3 = c.GetAccountByName(name);
                    var transactions3 = new List<Transaction>();
                    transactions3.Add(new Transaction() { Amount = 0, Name = "Initial", TransDate = DateTime.Today, TransactionId = System.Guid.NewGuid() });
                    acct3.Transactions = transactions3.ToArray();
                    c.UpsertAccount(acct3);
                }
            }

            

            var acct2 = c.GetAccountByName("Test");
            Assert.AreEqual(acct2.Transactions.Length, acct.Transactions.Length);
            Assert.AreEqual(acct2.Balance(), 5);
        }
    }
}
