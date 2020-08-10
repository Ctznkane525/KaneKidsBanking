using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace KaneKidsBanking.Database
{
    public class Transaction
    {

        [JsonProperty(PropertyName = "Amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "TransDate")]
        public DateTime TransDate { get; set; }

        [JsonProperty(PropertyName = "TransactionId")]
        public Guid TransactionId { get; set; }
    }
}
