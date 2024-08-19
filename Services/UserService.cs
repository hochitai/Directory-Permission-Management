using DirectoryPermissionManagement.Configs;
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

        public UserResponse? AddUser(UserRequest userRequest)
        {
            if (_userRepository.IsExisted(userRequest.Username))
            {
                return null;
            }

            StringHelper stringHelper = new StringHelper();
            byte[] salt = stringHelper.CreateSalt();

            User user = new User();
            user.Username = userRequest.Username;
            user.Password = stringHelper.HashPassword(userRequest.Password, salt);
            user.Name = userRequest.Name;
            user.IsActived = true;
            user.Salt = Convert.ToBase64String(salt);

            user = _userRepository.Add(user);

            return new UserResponse(user);
        }
        public UserResponse? Login(UserRequest userRequest) 
        {
            if (!_userRepository.IsExisted(userRequest.Username))
            {
                return null;
            }

            var userInDb = _userRepository.GetUserByUsername(userRequest.Username);

            if (userInDb == null)
            {
                return null;
            }

            StringHelper stringHelper = new StringHelper();
            string hashedPassword = stringHelper.HashPassword(userRequest.Password, Convert.FromBase64String(userInDb.Salt));

            if (hashedPassword != userInDb.Password)
            {
                return null;
            }

            var result = new UserResponse(userInDb.Id, userInDb.Name, "");

            TokenHelper tokenHelper = new TokenHelper();
            var token = tokenHelper.GenerateToken(result, _jwtConfig);

            result.Token = token;

            return result;
        }
    }
}
