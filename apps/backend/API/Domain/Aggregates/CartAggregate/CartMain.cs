using API.Common.Helpers;

namespace API.Domain.Aggregates.CartAggregate
{
    public class CartMain
    {
        public Guid CartUuid { get; private set; }
        public Guid UserUuid {  get; private set; }
        public Guid MerchantUuid { get; private set; }
        private readonly List<CartItem> _items = new List<CartItem>();
        public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

        public CartMain( Guid userUuid, Guid merchantUuid, List<CartItem> items)
        {
            CartUuid = UuidV7Helper.NewUuidV7();
            UserUuid = userUuid;
            MerchantUuid = merchantUuid;
            _items = items;
        }
        //内部构造函数，用于从数据库中加载数据时使用
        internal CartMain(Guid cartUuid, Guid userUuid, Guid merchantUuid, List<CartItem> items)
        {
            CartUuid = cartUuid;
            UserUuid = userUuid;
            MerchantUuid = merchantUuid;
            _items = items ?? new List<CartItem>();
        }

        public void AddItem(CartItem item)
        {
            var existingItem = _items.FirstOrDefault(i => i.ProductUuid == item.ProductUuid);

            if (existingItem != null)
            {
                existingItem.UpdateQuantity(item.Quantity);
            }
            else
            {
                _items.Add(item);
            }
        }

        public void RemoveItem(Guid productUuid)
        {
            var item = _items.FirstOrDefault(i => i.ProductUuid == productUuid);
            if (item != null)
            {
                _items.Remove(item);
                // 发布领域事件：CartItemRemovedEvent
            }
        }
        public decimal GetTotalPrice()
        {
            return _items.Sum(i => i.Price * i.Quantity);
        }
    }
}
