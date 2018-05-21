using System;

namespace PAM.Models
{
    public class Commissions
    {
        public int ID { get; set; }
        public double comm_perc_fee { get; set; }
        public double comm_perc_amount { get; set; }
        public Boolean comm_status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}