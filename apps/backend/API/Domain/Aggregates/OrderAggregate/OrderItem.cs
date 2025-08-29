using API.Common.Helpers;

namespace API.Domain.Aggregates.OrderAggregates
{
    public class OrderItem
    {
        public Guid OrderItemUuid { get; private set; }
        public Guid ProductUuid { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public string Name {  get; private set; }
        public decimal PackingFee { get; private set; }

        private OrderItem() { } // for EF

        public OrderItem(Guid productUuid, int quantity, decimal unitPrice, string name, decimal packingFee)
        {
            OrderItemUuid = UuidV7Helper.NewUuidV7();
            ProductUuid = productUuid;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Name = name;
            PackingFee = packingFee;
        }
        internal OrderItem(Guid orderItemUuid, Guid productUuid, int quantity, decimal unitPrice, string name, decimal packingFee)
        {
            OrderItemUuid = orderItemUuid;
            ProductUuid = productUuid;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Name = name;
            PackingFee = packingFee;
        }
        public decimal SubTotal => Quantity * (UnitPrice+PackingFee);
        public int AddQuantity(int quantity)
        {
            return Quantity + quantity;
        }
    }
}
