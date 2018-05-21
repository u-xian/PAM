using System;

namespace PAM.Models
{
    public class Transactions
    {
        public int ID { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
        public string tnx_type { get; set; }
        public string tnxid { get; set; }
        public string external_tnx_id { get; set; }
        public double initial_amount { get; set; }
        public double comm_amount { get; set; }
        public double total_amount { get; set; }
        public bool tnx_status { get; set; }
        public DateTime created_at { get; set; }
    }
}