namespace API.Domain.Enums
{
    public enum RefundStatus
    {
        create = 1,     //新退款订单
        review ,     //退款订单审核中
        pass,       //退款订单通过
        refuse      //退款订单拒绝
    }
}
