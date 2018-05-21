using PAM.Models;
using System;

namespace PAM.Repositories
{
    public class TransactionRepository
    {
        private readonly PAMDbContext _context;

        public void Save(string sender, string receiver, string tnx_type, string tnxid, string external_tnx_id, double initial_amount, double comm_amount, double total_amount, bool status)
        {
            Transactions tnx = new Transactions();

            tnx.sender = sender;
            tnx.receiver = receiver;
            tnx.tnx_type = tnx_type;
            tnx.tnxid = tnxid;
            tnx.external_tnx_id = external_tnx_id;
            tnx.initial_amount = initial_amount;
            tnx.comm_amount = comm_amount;
            tnx.total_amount = total_amount;
            tnx.tnx_status = status;
            tnx.created_at = DateTime.Now;

            _context.Transactions.Add(tnx);
            _context.SaveChanges();

        }
    }
}