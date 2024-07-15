using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public interface IUserRepository
    {
        UserDto GetUserById(int userId);
        UserDto Add(UserDtoInsert user);
        UserDto Login(LoginDto login);
        UserDto GetUserByEmail(string userEmail);
        IEnumerable<UserDto> GetUsers();

        void Delete(int userId);

        UserDto Update(User user, int userId);
    }

}