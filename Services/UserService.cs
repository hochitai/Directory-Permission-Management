﻿using DirectoryPermissionManagement.Configs;
using DirectoryPermissionManagement.DTOs;
using DirectoryPermissionManagement.Helpers;
using DirectoryPermissionManagement.Models;
using DirectoryPermissionManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace DirectoryPermissionManagement.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly JWTConfig _jwtConfig;

        public UserService(UserRepository userRepository, JWTConfig jwtConfig)
        {
            _userRepository = userRepository;
            _jwtConfig = jwtConfig;
        }

        public async Task<UserResponse?> CreateUser(UserRequest userRequest)
        {
            if (await _userRepository.IsExisted(userRequest.Username))
            {
                return null;
            }

            byte[] salt = StringHelper.CreateSalt();

            User user = new User()
            {
                Username = userRequest.Username,
                Password = StringHelper.HashPassword(userRequest.Password, salt),
                Name = userRequest.Name,
                IsActived = true,
                Salt = Convert.ToBase64String(salt),
            };

            user = await _userRepository.Add(user);

            return new UserResponse(user);
        }
        public async Task<UserResponse?> Login(UserRequest userRequest) 
        {
            if ( ! await _userRepository.IsExisted(userRequest.Username))
            {
                return null;
            }

            var userInDb = await _userRepository.GetUserByUsername(userRequest.Username);

            if (userInDb == null)
            {
                return null;
            }

            string hashedPassword = StringHelper.HashPassword(userRequest.Password, Convert.FromBase64String(userInDb.Salt));

            if (hashedPassword != userInDb.Password)
            {
                return null;
            }

            var result = new UserResponse()
            {
                Id = userInDb.Id, 
                Name = userInDb.Name,
            };

            var token = TokenHelper.GenerateToken(result, _jwtConfig);

            result.Token = token;

            return result;
        }
    }
}
