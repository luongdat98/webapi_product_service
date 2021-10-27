using APIWeb.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI2.Models;

namespace APIWeb.Repositories
{
    public interface IUserRepository
    {
        User RegisterUser(RegisterUserModel model);

        //LoginWithTokenModel LoginUser(LoginModel user);
        UserWithTokenDto LoginUser(LoginModel user);

    }
}
