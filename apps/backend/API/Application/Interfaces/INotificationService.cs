using API.Api.Models;
using API.Application.DTOs;
using API.Domain.Entities.Models;

namespace API.Application.Interfaces
{
    public interface INotificationService
    {
        Task<bool> AddNotification (CreateNotificationCommandDto commandDto);     //用于添加Notification，群发或者条件发送用Delivery+Notification的混合Service实现
        Task<List<Notification>> GetNotifications (NotificationQueryOptions notificationQueryOptions);      //条件查询所需的Notifications
        Task<bool> RemoveNotificationManually(NotificationDeleteOptions notificationDeleteOptions);      //用于手动删除Notification
        Task<bool> RemoveNotificationAutomatically();       //用于自动删除out of time的Notification
    }
}
