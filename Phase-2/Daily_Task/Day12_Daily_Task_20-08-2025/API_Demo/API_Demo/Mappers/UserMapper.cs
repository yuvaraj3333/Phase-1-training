using API_Demo.Models;
using API_Demo.DTOs;

namespace API_Demo.Mappers
{
    public class UserMapper
    {
        public static UserDTO ToDTO(UserModel user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
