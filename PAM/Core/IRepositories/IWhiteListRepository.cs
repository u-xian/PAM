
namespace PAM.Core.IRepositories
{
    public interface IWhiteListRepository
    {
        bool CheckExists(string msisdn, int id);
    }
}
