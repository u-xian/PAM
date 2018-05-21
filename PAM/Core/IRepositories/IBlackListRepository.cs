
namespace PAM.Core.IRepositories
{
    public interface IBlackListRepository
    {
        bool CheckExists(string msisdn);
    }
}
