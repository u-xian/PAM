using PAM.Core;
using PAM.Core.IRepositories;
using PAM.Models;

namespace PAM.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PAMDbContext _context;

        public IBlackListRepository BlackLists { get; private set; }
        public IWhiteListRepository WhiteLists { get; private set; }

        //public UnitOfWork(PAMDbContext context)
        //{
        //    _context = context;
        //    BlackLists = new BlackListRepository(context);
        //    WhiteLists = new WhiteListRepository(context);

        //}
    }
}