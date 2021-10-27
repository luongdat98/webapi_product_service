using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI2.Models;

namespace APIWeb.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public int IsActive { get; set; }
        public int TypeUserId { get; set; }

        public virtual TypeUserDto TypeUser { get; set; }
    }

    public class UserModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public int IsActive { get; set; }
        public int TypeUserId { get; set; }
    }

    public class RegisterUserModel
    {   
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Fullname { get; set; }
    }

    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginWithTokenModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Token { get; set; }
    }


    public class UserWithToken : User
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserWithToken(User user)
        {
            this.UserId = user.UserId;
            this.Email = user.Email;
            this.Fullname = user.Fullname;
            this.IsActive = user.IsActive;
            this.TypeUserId = user.TypeUserId;
        }
    }

    public class UserWithTokenDto
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }

}
