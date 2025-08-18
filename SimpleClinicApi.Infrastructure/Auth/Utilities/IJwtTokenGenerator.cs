using Microsoft.AspNetCore.Identity;

namespace SimpleClinicApi.Infrastructure.Auth.Utilities
{
   public interface IJwtTokenGenerator
   {
      public string GenerateJwtToken(IdentityUser user);
   }
}