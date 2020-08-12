using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using KaneKidsBanking.Database;
using Microsoft.AspNetCore.Http;

namespace KaneKidsBanking.Functions
{
    public static class AllowanceHttp
    {

        [FunctionName("AllowanceHttp")]
        public static void Run([HttpTrigger()]HttpRequest req, ILogger log)
        {
            var ab = new AllowanceBase();
            ab.Log = log;
            ab.Run();

        }       
    }
}
