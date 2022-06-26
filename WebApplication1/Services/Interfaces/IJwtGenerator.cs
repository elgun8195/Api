

using System.Collections.Generic;
using WebApplication1.Data.Entities;

namespace WebApplication1.Services.Interfaces
{
    public interface IJwtGenerator
    {
        string GenerateJwt(AppUser user, IList<string> roles);
    }
}
