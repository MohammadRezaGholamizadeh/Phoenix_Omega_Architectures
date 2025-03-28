namespace IdentityLayer.AspDotNetIdentity.Domain
{
    public class IdentityEmailVerificationCode
    {
        public long Id { get; set; }
        public string VerificationCode { get; set; }
        public DateTime VerificationDate { get; set; }
        public string EmailResultDesc { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
        public EmailVerificationUsage Usage { get; set; }
    }

    public enum EmailVerificationUsage : byte
    {
        Login = 1,
        EditEmail = 2
    }
}
