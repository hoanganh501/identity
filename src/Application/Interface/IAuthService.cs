using Application.Request;
using Application.Respsone;

namespace Application.Interface
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(AuthRequest request);
    }
}
