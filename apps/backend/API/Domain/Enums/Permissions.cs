namespace API.Domain.Enums
{
    public enum Permissions
    {
        //enum('user', 'product', 'order', 'shop', 'warehouse', 'marketing', 'support', 'finance', 'system', 'other')
        //GroupSystem
        AddAdmin = 0,
        VerifyAdmin = 1,

        //GroupShop
        AddProduct = 100,
        RemoveProduct = 101,
        UpdateProduct = 102,
        GetProduct = 103,
        //GroupUser

        //Group
    }
}
