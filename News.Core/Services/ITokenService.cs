using Microsoft.AspNetCore.Identity;
using News.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace News.Core.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(WebUser user , UserManager<WebUser> userManager);
    }
}
