using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace KaneKidsBanking.Database
{
    public class Account
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get { return AccountId; } set { } }

        [JsonProperty(PropertyName = "AccountId")]
        public string AccountId { get; set; }

        [JsonPropertyName("Transactions")]
        //[Newtonsoft.Json.JsonIgnore()]
        public Transaction[] Transactions { get; set; }

        public Account()
        {

        }

        public Account(string accountId, Transaction[] transactions)
        {
            AccountId = accountId;
            Transactions = transactions;
        }

        public bool TransactionExists(DateTime transactionDate, string name)
        {
            return Transactions.ToList().Where(c => transactionDate == c.TransDate && c.Name == name).Count() > 0;
        }

        public void RemoveTransaction(string transactionId)
        {
            var list = Transactions.ToList();
            list.Remove(list.Where(item => item.TransactionId.ToString() == transactionId).FirstOrDefault());
            Transactions = list.ToArray();
        }

        public void InsertTransaction(Transaction transItem)
        {
            var transactions = this.Transactions.ToList();
            transactions.Add(transItem);
            this.Transactions = transactions.ToArray();
        }

        public decimal Balance()
        {
            return Transactions.Sum(c => c.Amount);
        }
    }
}
