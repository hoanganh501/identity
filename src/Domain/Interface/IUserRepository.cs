using Domain.Entity;

namespace Domain.Interface
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetUserByIdAsync(Guid userId);
    }
}
