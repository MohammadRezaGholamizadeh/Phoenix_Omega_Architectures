namespace IdentityLayer.AspDotNetIdentity.Domain
{
    public class IdentitySmsVerificationCode
    {
        public IdentitySmsVerificationCode()
        {
        }

        public IdentitySmsVerificationCode(
            string verificationCode,
            DateTime verificationDate,
            string smsResultDesc,
            Mobile mobile)
        {
            VerificationCode = verificationCode;
            VerificationDate = verificationDate;
            SmsResultDesc = smsResultDesc;
            Mobile = mobile;
        }

        public long Id { get; set; }
        public string VerificationCode { get; set; }
        public DateTime VerificationDate { get; set; }
        public string SmsResultDesc { get; set; }
        public Mobile Mobile { get; set; }
        public bool IsVerified { get; set; }
        public SmsVerificationCodeUsage Usage { get; set; }
    }

    public enum SmsVerificationCodeUsage : byte
    {
        Login = 0,
        EditMobileNumber = 1
    }
}