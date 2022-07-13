﻿using System.Collections.Generic;
using System.Linq;
using Application.MainModule.DTO;
using Application.MainModule.IServices;
using AutoMapper;
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
            
            var currentUser = _userRepository.Find(s => s.Email.Equals(username) && s.Password.Equals(password)).FirstOrDefault();

            if (currentUser != null)
            {
                var currentUserDto = _mapper.Map<UserDto>(currentUser);
                return currentUserDto;
            }

            return null;
        }

        public IEnumerable<UserDto> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public UserDto GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(UserDto userDto)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, UserDto userDto)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}