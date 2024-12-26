namespace Web.Utility
{
    public class StaticData
    {
        public static string? CouponAPIBase { get; set; }
        public static string? AuthAPIBase { get; set; }
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public enum ApiType
        {
            Get,
            Post,
            Put,
            Delete,
        }
    }
}