using System.Collections.Generic;
using System.Security.Claims; 

namespace SecretSantaBackend.Services
{
    public interface IJwtService
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}