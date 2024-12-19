namespace Web.Utility
{
    public class StaticData
    {
        public static string CouponAPIBase { get; set; }
        public enum ApiType
        {
            Get,
            Post,
            Put,
            Delete,
        }
    }
}