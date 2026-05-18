namespace DriverApi.ModelsDtos
{
    public class AuthResponseDto
    {
        public string? UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Token { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;
    }
}
