using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace SimpleClinicApi.Infrastructure.Auth.Mappers
{
   public class IdentityMappingProfile : Profile
   {
      public IdentityMappingProfile()
      {
         CreateMap<RegisterDto, IdentityUser>(MemberList.None).ReverseMap();
      }
   }
}