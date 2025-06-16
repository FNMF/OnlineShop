using API.Domain.Entities.Models;

namespace API.Repositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetUserAllNotificationsByUuidWithPagingAsync(byte[] uuidBytes, int pageNumber, int pageSize);
        Task<List<Notification>> GetMerchantAllNotificationByUuidWithPagingAsync(byte[] uuidBytes, int pageNumber, int pageSize);
        Task<List<Notification>> GetUserAllNotificationsByUuidAndTypeWithPagingAsync(byte[] uuidBytes, string type, int pageNumber, int pageSize);
        Task<List<Notification>> GetMerchantAllNotificationByUuidAndTypeWithPagingAsync(byte[] uuidBytes, string type, int pageNumber, int pageSize);
        Task<bool> AddNotificationAsync(Notification notification);
        Task<bool> AddBatchNotificationAsync(List<Notification> notifications);
        Task<bool> UpdateNotificationAsync(Notification notification);
        Task<bool> DeleteOutOfTimeNotificationAsync(List<Notification> notifications);

    }
}
