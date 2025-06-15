namespace API.Domain.Enums
{
    public enum OrderStatus
    {
        created = 0,        //新建
        unpaid = 1,         //未支付
        unaccepted = 2,     //商户未接受
        preparing = 3,      //商户准备中
        unship = 4,         //未找到骑手配送
        shipping = 5,       //骑手配送中
        done = 6,           //订单完成
        canceled = 7,       //用户取消
        reject = 8,         //商户拒绝
        exception = 9       //异常

    }
}
