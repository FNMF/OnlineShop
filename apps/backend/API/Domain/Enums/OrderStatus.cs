namespace API.Domain.Enums
{
    public enum OrderStatus
    {
        created = 0,        //新建
        paid = 1,           //已支付
        accepted = 2,       //商户已接受
        prepared = 3,       //商户准备完成,寻找骑手中
        shipped = 4,        //骑手配送中
        completed = 8,      //订单完成
        canceled = 6,       //用户取消
        rejected = 7,       //商户拒绝
        exception = 8       //异常

    }
}
