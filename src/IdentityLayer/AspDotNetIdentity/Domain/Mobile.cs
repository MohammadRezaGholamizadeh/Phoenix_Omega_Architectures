namespace IdentityLayer.AspDotNetIdentity.Domain
{
    public class Mobile
    {
        public Mobile() { }

        public Mobile(string countryCallingCode, string mobileNumber)
        {
            CountryCallingCode = countryCallingCode;
            MobileNumber = mobileNumber;
        }

        public string MobileNumber { get; set; }
        public string CountryCallingCode { get; set; }
    }
}