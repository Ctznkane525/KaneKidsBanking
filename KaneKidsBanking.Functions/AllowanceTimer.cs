using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace KaneKidsBanking.Functions
{
    public static class AllowanceTimer
    {
        [FunctionName("AllowanceTimer")]
        public static void Run([TimerTrigger("0 0 6 * * SUN")]TimerInfo myTimer, ILogger log)
        {
            var ab = new AllowanceBase();
            ab.Log = log;
            ab.Run();
        }
    }
}
