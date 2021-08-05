using Core.Models;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}