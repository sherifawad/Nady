using Core.Models;
using Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class LoginExtensions
    {
        public static LoginDto AsDto(this AppUser user)
        {
            return new LoginDto
            {
                Username = user.UserName
            };
        }
        public static AppUser FromDto(this LoginDto user)
        {
            return new AppUser
            {
                UserName = user.Username

            };
        }
    }
}
