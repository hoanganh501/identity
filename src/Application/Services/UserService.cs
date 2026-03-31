using Application.Interface;
using Application.Respsone;
using Domain.Interface;

namespace Application.Services
{
    public class UserService: IUserService
    {
        public readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId) ?? throw new Exception("User not found");
            return new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }
    }
}
