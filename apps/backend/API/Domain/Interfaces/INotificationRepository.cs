using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface INotificationRepository
    {
        IQueryable<Notification> QueryNotifications();
        IQueryable<Notification> QueryNotificationDeliveriesByUuidAsync(Guid uuid);
        /*Task<List<Notification>> GetUserAllNotificationsByUuidWithPagingAsync(byte[] uuidBytes, int pageNumber, int pageSize);
        Task<List<Notification>> GetMerchantAllNotificationByUuidWithPagingAsync(byte[] uuidBytes, int pageNumber, int pageSize);
        Task<List<Notification>> GetUserAllNotificationsByUuidAndTypeWithPagingAsync(byte[] uuidBytes, string type, int pageNumber, int pageSize);
        Task<List<Notification>> GetMerchantAllNotificationByUuidAndTypeWithPagingAsync(byte[] uuidBytes, string type, int pageNumber, int pageSize);*/
        Task<bool> AddNotificationAsync(Notification notification);
        Task<bool> AddBatchNotificationAsync(List<Notification> notifications);
        Task<bool> UpdateNotificationAsync(Notification notification);
        Task<bool> DeleteNotificationAsync(List<Notification> notifications);
        Task SaveChangesAsync();

    }
}
