using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(LoginDto login)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);
            if (user == null)
            {
                throw new InvalidOperationException("Incorrect e-mail or password");
            }
            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                UserType = user.UserType
            };
        }
        public UserDto Add(UserDtoInsert user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                throw new InvalidOperationException("User email already exists");
            }
            
            try
            {
                _context.Users.Add(
                    new User
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        UserType = "client"
                    }
                );
                _context.SaveChanges();
                var newUser = _context.Users.First(u => u.Email == user.Email);
                return new UserDto
                {
                    UserId = newUser.UserId,
                    Name = newUser.Name,
                    Email = newUser.Email,
                    UserType = newUser.UserType
                };
                }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetUsers()
        {
            try
            {
                return _context.Users.Select(u => new UserDto
            {
                UserId = u.UserId,
                Name = u.Name,
                Email = u.Email,
                UserType = u.UserType
            });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public UserDto Update(User user, int userId)
        {
            var userToUpdate = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (userToUpdate == null)
            {
                throw new InvalidOperationException("User not found");
            }
            if (user.Name != null)
            {
                userToUpdate.Name = user.Name;
            }
            if (user.Email != null)
            {
                userToUpdate.Email = user.Email;
            }
            if (user.UserType != null)
            {
                userToUpdate.UserType = user.UserType;
            }
            if (user.Password != null)
            {
                userToUpdate.Password = user.Password;
            }
            _context.SaveChanges();
            return new UserDto
            {
                UserId = userToUpdate.UserId,
                Name = userToUpdate.Name,
                Email = userToUpdate.Email,
                UserType = userToUpdate.UserType
            };
        }

    }
}