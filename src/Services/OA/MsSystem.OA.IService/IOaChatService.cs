using MsSystem.OA.Model;
using System.Threading.Tasks;

namespace MsSystem.OA.IService
{
    public interface IOaChatService
    {
        Task<bool> InsertAsync(OaChat chat);
    }
}
