namespace SimpleClinicApi.Infrastructure.Auth.Dtos
{
   public record RegisterDto(string? UserName, string? Email, string Password);
}