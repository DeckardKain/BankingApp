namespace BankingAppCore.DTO.UI
{
    public record UserLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool LoginResult { get; set; }
        public string UserId { get; set; }

    }
}
