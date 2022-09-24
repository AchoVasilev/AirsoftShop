namespace AirsoftShop.Common.Constants;

public static class Constants
{
    public static class WebConstants
    {
        public const string CloudinaryFolderName = "BgAirsoft";
    }

    public static class ControllerRoutes
    {
        public const string ControllerTemplate = "[controller]";
        
        public const string Newest = "newest";
        public const string ById = "{id}";
        public const string Mine = "mine";

        public const string BulkAdd = "bulkAdd";
        public const string DeliveryData = "deliveryData";
        public const string GetNavData = "getNavData";

        public const string GunSubcategories = "gunSubcategories";
        public const string ClothingSubcategories = "clothingSubcategories";

        public const string GetDealerId = "getDealerId";

        public const string ForClient = "client";
    }
}