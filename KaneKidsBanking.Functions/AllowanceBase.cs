using KaneKidsBanking.Database;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KaneKidsBanking.Functions
{
    public class AllowanceBase
    {

        public ILogger Log { get; set; }


        public CosmosDb GetDb()
        {
            var endPoint = System.Environment.GetEnvironmentVariable("endPointUrl");
            var primaryKey = System.Environment.GetEnvironmentVariable("primaryKey");
            return new CosmosDb(endPoint, primaryKey);
        }

        public void Run()
        {
            Log.LogInformation($"Running Allowance Function at: {DateTime.Now}");
            var db = GetDb();
            var allowanceDate = DateTime.Today;
            var allowanceName = "Weekly Allowance";
            var allowanceAmount = 15;
            foreach (var name in new[] { "Lilly", "Autumn"})
            {
                var acct = db.GetAccountByName(name);
                if (!acct.TransactionExists(allowanceDate, allowanceName))
                {
                    acct.InsertTransaction(new Transaction()
                    {
                        Name = allowanceName,
                        TransDate = allowanceDate,
                        Amount = allowanceAmount,
                        TransactionId = System.Guid.NewGuid()
                    }) ;
                    db.UpsertAccount(acct);
                }
            }
           
        }
    }
}
