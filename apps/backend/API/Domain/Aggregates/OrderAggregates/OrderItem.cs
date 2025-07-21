namespace API.Domain.Aggregates.OrderAggregates
{
    public class OrderItem
    {
        public byte[] OrderUuid { get; private set; }
        public byte[] ProductUuid { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public string Name {  get; private set; }

        private OrderItem() { } // for EF

        public OrderItem(byte[] orderUuid, byte[] productUuid, int quantity, decimal unitPrice, string name)
        {
            OrderUuid = orderUuid;
            ProductUuid = productUuid;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Name = name;
        }
        
    }
}
