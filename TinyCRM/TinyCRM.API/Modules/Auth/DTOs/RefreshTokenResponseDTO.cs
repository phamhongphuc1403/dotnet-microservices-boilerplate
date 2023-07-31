namespace TinyCRM.API.Modules.Auth.DTOs
{
    public class RefreshTokenResponseDTO
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}