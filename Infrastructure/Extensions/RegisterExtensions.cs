using Core.Models;
using Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class RegisterExtensions
    {
        public static RegisterDto AsDto(this AppUser user)
        {
            return new RegisterDto
            {
                DisplayName = user.DisplayName,
                Username = user.UserName

            };
        }
        public static AppUser FromDto(this RegisterDto user)
        {
            return new AppUser
            {
                DisplayName = user.DisplayName,
                UserName = user.Username

            };
        }
    }
}
