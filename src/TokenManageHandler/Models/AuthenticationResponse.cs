namespace TokenManageHandler.Models
{
    public class AuthenticationResponse
    {
        public required string Token { get; set; }
        public required string AuthType { get; set; }
        public required int ExpireIns { get; set; }
    }
}
