using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(Guid userPublicId, string role);
        string GenerateToken(Guid userPublicId, string role, List<string> permissions);
        string GenerateToken(Guid userPublicId, string role, List<string> permissions, long? propertyId, long? propertyRoleId, string propertyRoleCode);
    }
}
