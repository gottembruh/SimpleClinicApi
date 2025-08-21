using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;

namespace SimpleClinicApi.Infrastructure.Auth.Mappers
{
   [UsedImplicitly]
   public class IdentityMappingProfile : Profile
   {
      public IdentityMappingProfile()
      {
         CreateMap<RegisterDto, IdentityUser>(MemberList.None).ReverseMap();
      }
   }
}