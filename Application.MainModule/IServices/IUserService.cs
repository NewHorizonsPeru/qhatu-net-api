using System.Collections.Generic;
using Application.MainModule.DTO;

namespace Application.MainModule.IServices
{
    public interface IUserService
    {
        UserDto SignIn(string username, string password);
        UserDto SignUp(UserDto userDto);
        IEnumerable<UserDto> GetAll();
        UserDto GetById(int id);
        void Add(UserDto userDto);
        void Update(int id, UserDto userDto);
        void Remove(int id);
    }
}