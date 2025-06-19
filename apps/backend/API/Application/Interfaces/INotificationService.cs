using API.Api.Models;
using API.Domain.Entities.Models;

namespace API.Application.Interfaces
{
    public interface INotificationService
    {
        Task<bool> AddNotification (Notification notification);     //用于System添加无ReceiverUuid的群发Notification，以及Merchant添加对用户个体的Notification
        Task<bool> AddBatchNotification(List<Notification> notifications);      //用于Merchant添加对于有限个数个用户的群发
        Task<bool> RemoveNotificationManually(NotificationDeleteOptions notificationDeleteOptions);      //用于手动删除Notification
        Task<bool> RemoveNotificationAutomatically();       //用于自动删除out of time的Notification
    }
}
