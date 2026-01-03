namespace API.Domain.Enums
{
    public enum Permissions
    {
        //enum('user', 'product_cover', 'order', 'shop', 'warehouse', 'marketing', 'support', 'finance', 'system', 'other')
        //GroupSystem
        AddAdmin = 0,
        VerifyAdmin = 1,

        //GroupShop
        AddProduct = 1000,
        RemoveProduct = 1001,
        UpdateProduct = 1002,
        GetProduct = 1003,

        GetOrder = 1004,

        AddMerchantShop = 1005,
        UpdateMerchantShop = 1006,
        GetMerchantShop = 1007,
        //GroupUser

        //Group
    }
}
