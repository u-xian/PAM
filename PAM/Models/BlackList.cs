using System;

namespace PAM.Models
{
    public class BlackList
    {
        public int ID { get; set; }
        public string msisdn { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}