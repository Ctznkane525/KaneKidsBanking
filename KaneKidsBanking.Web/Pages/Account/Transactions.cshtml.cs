using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaneKidsBanking.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Cosmos.Linq;

namespace KaneKidsBanking.Web.Pages.Account
{
    public class TransactionsModel : PageModel
    {

        public TransactionsModel(CosmosDb db)
        {
            this.Db = db;
        }

        public CosmosDb Db { get; }
        public Database.Account Account { get; private set; }
        public IOrderedEnumerable<Transaction> Transactions { get; private set; }

        public void OnGet(string Id)
        {
            GetItems(Id);
        }

        private void GetItems(string Id)
        {
            this.Account = this.Db.GetAccountByName(Id);
            this.Transactions = this.Account.Transactions.OrderByDescending(c => c.TransDate);
        }

        public ActionResult OnPostDelete(string Id, string TransactionId)
        {
            this.Account = this.Db.GetAccountByName(Id);
            this.Account.RemoveTransaction(TransactionId);
            this.Db.UpsertAccount(this.Account);
            return this.RedirectToPage("./Transactions", new { Id = Id });
            //this.GetItems(Id);
        }

        public ActionResult OnPostSave(string Id)
        {
            // Get Account
            this.Account = this.Db.GetAccountByName(Id);

            if (!String.IsNullOrEmpty(Request.Form["edit-guid"]))
            {
                // Update
                this.Account = this.Db.GetAccountByName(Id);
                var transItem = this.Account.Transactions.Where(c => c.TransactionId.ToString() == Request.Form["edit-guid"]).FirstOrDefault();
                SetTransactionFromForm(transItem);
            }
            else
            {
                // Insert
                var transItem = new Transaction();
                transItem.TransactionId = System.Guid.NewGuid();
                SetTransactionFromForm(transItem);
                this.Account.InsertTransaction(transItem);
            }

            // Save
            this.Db.UpsertAccount(this.Account);

            // Redirect
            return this.RedirectToPage("./Transactions", new { Id = Id });
        }

        private void SetTransactionFromForm(Transaction transItem)
        {
            transItem.Name = Request.Form["edit-name"].ToString();
            transItem.TransDate = Convert.ToDateTime(Request.Form["edit-date"]);
            transItem.Amount = Convert.ToDecimal(Request.Form["edit-amount"]);
        }
    }
}
