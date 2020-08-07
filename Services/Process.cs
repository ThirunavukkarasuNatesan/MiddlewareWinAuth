using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MiddlewareWinAuth.Models.dto;
using System.Security.Claims;

namespace MiddlewareWinAuth.Services
{
    public class ProcessToken
    {
        public ProcessToken()
        {

        }
        public async Task<TokenDetail> GetToken()
        {
            TokenDetail tk = new TokenDetail();

            tk.name = "name";
            //tk.email = "email";
           // tk.token = "test token";


           

            return tk;
        }
    }
}
