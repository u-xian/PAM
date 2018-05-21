using System;

namespace PAM.Models
{
    public class WhiteList
    {
        public int ID { get; set; }
        public string msisdn { get; set; }


        public PlatformType PlatformType { get; set; }

        public int PlatformTypeId { get; set; }


        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}