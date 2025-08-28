namespace SimpleClinicApi.Infrastructure.Auth
{
   public record RegisterDto(string? UserName, string? Email, string Password);
}