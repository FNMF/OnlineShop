namespace API.Domain.Enums
{
    public enum OrderStatus
    {
        created = 0,        //新建
        paid = 1,           //已支付
        accepted = 2,       //商户已接受
        preparing = 3,      //商户准备中
        prepared = 4,       //商户准备完成,寻找骑手中
        shipped = 5,        //骑手配送中
        completed = 6,      //订单完成
        canceled = 7,       //用户取消
        rejected = 8,       //商户拒绝
        exception = 9       //异常

    }
}
