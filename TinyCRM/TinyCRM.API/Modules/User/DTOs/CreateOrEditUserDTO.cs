namespace TinyCRM.API.Modules.User.DTOs
{
    public class CreateOrEditUserDTO
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}