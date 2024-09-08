namespace MyApi.DTOs.AuthDtos
{
    public class TokenResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string UserName { get; set; }
    }
}
