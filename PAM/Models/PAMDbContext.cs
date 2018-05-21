using System.Data.Entity;

namespace PAM.Models
{
    public class PAMDbContext : DbContext
    {
        public PAMDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<Commissions> Commissions { get; set; }
        public DbSet<BlackList> BlackList { get; set; }
        public DbSet<WhiteList> WhiteList { get; set; }
    }
}