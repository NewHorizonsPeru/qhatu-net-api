using System.Collections.Generic;
using System.Linq;
using Encrypt = BCrypt.Net.BCrypt;
using Application.MainModule.DTO;
using Application.MainModule.IServices;
using AutoMapper;
using Domain.MainModule.Entities;
using Domain.MainModule.IRepositories;

namespace Application.MainModule.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserDto SignIn(string username, string password)
        {
            var currentUser = _userRepository.Find(s => s.Email.Equals(username)).FirstOrDefault();
            if (currentUser == null || !Encrypt.Verify(password, currentUser.Password)) return null;
            var currentUserDto = _mapper.Map<UserDto>(currentUser);
            return currentUserDto;

        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _userRepository.List();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public UserDto GetById(int id)
        {
            var user = _userRepository.GetById(id);
            return _mapper.Map<UserDto>(user);
        }

        public void Add(UserDto userDto)
        {
            var existsUsers = _userRepository.List(w => w.Email.Contains(userDto.Username));
            if (!existsUsers.Any())
            {
                var category = _mapper.Map<User>(userDto);
                _userRepository.Add(category);
                _userRepository.Save();
            }
        }

        public void Update(int id, UserDto userDto)
        {
            var user = _userRepository.GetById(id);
            _mapper.Map(userDto, user);
            _userRepository.Save();
        }

        public void Remove(int id)
        {
            var user = _userRepository.GetById(id);
            _userRepository.Remove(user);
            _userRepository.Save();
        }
    }
}