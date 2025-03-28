namespace IdentityLayer.AspDotNetIdentity.Domain
{
    public struct AccessRightRoles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = $"Admin, {SuperAdmin}";
        public const string Client = $"Client, {Admin}, {SuperAdmin}";
    }
}