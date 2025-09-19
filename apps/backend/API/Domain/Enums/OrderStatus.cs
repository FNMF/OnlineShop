namespace API.Domain.Enums
{
    public enum OrderStatus
    {
        created = 1,        //新建
        paid ,           //已支付
        accepted,       //商户已接受
        prepared,       //商户准备完成,寻找骑手中
        shipped,        //骑手配送中
        completed,      //订单完成
        canceled,       //用户取消
        rejected,       //商户拒绝
        exception       //异常

    }
}
