using API.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace API.Api.SignalR
{
    public class NotificationHub:Hub
    {
        private readonly ICurrentService _currentService;
        public NotificationHub(ICurrentService currentService)
        {
            _currentService = currentService;
        }

        //重写识别用户
        public override async Task OnConnectedAsync()
        {
            var userUuid = _currentService.RequiredUuid.ToString();
            if (!string.IsNullOrEmpty(userUuid))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userUuid);
            }
            await base.OnConnectedAsync();
        }
        //定义客户端能调用的方法
        //TODO，这是个测试案例，后续可以修改为平台端需要使用的方法，但是貌似理论上不需要客户端调用，只需要通过EventBus实现通知即可。但是ChatHub还是需要这个的
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


    }
}
