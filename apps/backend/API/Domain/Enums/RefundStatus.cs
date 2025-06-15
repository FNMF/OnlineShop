namespace API.Domain.Enums
{
    public enum RefundStatus
    {
        create = 0,     //新退款订单
        review = 1,     //退款订单审核中
        pass = 2,       //退款订单通过
        refuse = 3      //退款订单拒绝
    }
}
