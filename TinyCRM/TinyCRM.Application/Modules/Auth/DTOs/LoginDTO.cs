namespace TinyCRM.Application.Modules.Auth.DTOs
{
    public class LoginDTO
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}