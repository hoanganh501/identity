using Application.Respsone;

namespace Application.Interface
{
    public interface IUserService
    {
        Task<UserResponse> GetUserByIdAsync(Guid id);
    }
}
