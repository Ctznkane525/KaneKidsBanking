using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaneKidsBanking.Database
{
    public class CosmosDb
    {
        public CosmosDb(string endpointUrl, string primaryKey)
        {
            EndpointUrl = endpointUrl;
            PrimaryKey = primaryKey;
            this.CosmosClient = new CosmosClient(endpointUrl, primaryKey);
            this.AccountsContainer = this.CosmosClient.GetContainer("KaneKidsBanking", "Accounts");
        }

        public bool AccountExists(string accountId)
        {
            var account = this.AccountsContainer.GetItemLinqQueryable<Account>(true).Where(c => c.AccountId == accountId).Take(1).AsEnumerable().FirstOrDefault();
            return (account != null);
        }

        public Account GetAccountByName(string accountId)
        {
            var account = this.AccountsContainer.GetItemLinqQueryable<Account>(true).Where(c => c.AccountId == accountId).Take(1).AsEnumerable().FirstOrDefault();
            if (account == null)
                account = new Account(accountId, new List<Transaction>().ToArray());
            return account;
        }

        public List<Account> GetAccounts()
        {
            return this.AccountsContainer.GetItemLinqQueryable<Account>(true).AsEnumerable().ToList();
        }

        public void UpsertAccount(Account account)
        {
            UpsertAccountAsync(account).GetAwaiter().GetResult();
        }

        public Task<ItemResponse<Account>> UpsertAccountAsync(Account account)
        {
            return this.AccountsContainer.UpsertItemAsync<Account>(account, new PartitionKey(account.AccountId));
        }


        public string EndpointUrl { get; }
        public string PrimaryKey { get; }
        private CosmosClient CosmosClient { get; }
        private Container AccountsContainer { get; }
    }
}
