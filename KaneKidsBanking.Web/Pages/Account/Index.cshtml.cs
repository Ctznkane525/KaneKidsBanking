using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaneKidsBanking.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KaneKidsBanking.Web.Accounts
{
    public class IndexModel : PageModel
    {

        

        public IndexModel(CosmosDb db)
        {
            this.Db = db;
        }

        public List<KaneKidsBanking.Database.Account> Accounts { get; private set; }
        private CosmosDb Db { get; }

        public void OnGet()
        {
            this.Accounts = Db.GetAccounts();
        }
    }
}
