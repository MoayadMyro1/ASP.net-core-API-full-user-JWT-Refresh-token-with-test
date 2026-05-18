using DriverApi.Models;

namespace DriverApi.Services
{
    public interface IJwtService
    {
        Task<string> GenerateToken(Users user);
    }
}
