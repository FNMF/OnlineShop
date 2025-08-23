namespace API.Domain.Aggregates.OrderAggregates
{
    public class OrderItem
    {
        public Guid OrderUuid { get; private set; }
        public Guid ProductUuid { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public string Name {  get; private set; }

        private OrderItem() { } // for EF

        public OrderItem(Guid orderUuid, Guid productUuid, int quantity, decimal unitPrice, string name)
        {
            OrderUuid = orderUuid;
            ProductUuid = productUuid;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Name = name;
        }
        public decimal SubTotal => Quantity * UnitPrice;
        public int AddQuantity(int quantity)
        {
            return Quantity + quantity;
        }
    }
}
