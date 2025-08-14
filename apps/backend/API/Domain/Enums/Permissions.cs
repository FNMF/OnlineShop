namespace API.Domain.Enums
{
    public enum Permissions
    {
        //enum('user', 'product', 'order', 'shop', 'warehouse', 'marketing', 'support', 'finance', 'system', 'other')
        //GroupSystem
        AddAdmin = 0,
        VerifyAdmin = 1,

        //GroupShop
        AddProduct = 1000,
        RemoveProduct = 1001,
        UpdateProduct = 1002,
        GetProduct = 1003,
        //GroupUser

        //Group
    }
}
