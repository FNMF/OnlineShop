using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly OnlineshopContext _context;
        public NotificationRepository(OnlineshopContext context)
        {
            _context = context;
        }
        public IQueryable<Notification> QueryNotifications()
        {
            return _context.Notifications;
        }
        public IQueryable<Notification> QueryNotificationDeliveriesByUuidAsync(Guid uuid)
        {
            return _context.Notifications
                .Where(n => n.Deliveries.Any(d => d.DeliveryReceiveruuid == uuid))
                .Include(n => n.Deliveries.Where(d => d.DeliveryReceiveruuid == uuid));
        }
        /*public async Task<List<Notification>> GetUserAllNotificationsByUuidWithPagingAsync(byte[] uuidBytes, int pageNumber, int pageSize)
        {
            if (pageSize <= 0) pageSize = 10;
            if (pageNumber <= 0) pageNumber = 1;

            return await _context.Notifications
                .Where(n => n.NotificationReceiveuuid == uuidBytes && (n.NotificationReceivertype == "user" || n.NotificationReceivertype == "alluser") && !n.NotificationIsdeleted)
                .OrderByDescending(n => n.NotificationTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<Notification>> GetMerchantAllNotificationByUuidWithPagingAsync(byte[] uuidBytes, int pageNumber, int pageSize)
        {
            if (pageSize <= 0) pageSize = 10;
            if (pageNumber <= 0) pageNumber = 1;

            return await _context.Notifications
                .Where(n => n.NotificationReceiveuuid == uuidBytes && (n.NotificationReceivertype == "merchant" || n.NotificationReceivertype == "allmerchant") && !n.NotificationIsdeleted)
                .OrderByDescending(n => n.NotificationTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<Notification>> GetUserAllNotificationsByUuidAndTypeWithPagingAsync(byte[] uuidBytes, string type, int pageNumber, int pageSize)
        {
            if (pageSize <= 0) pageSize = 10;
            if (pageNumber <= 0) pageNumber = 1;

            return await _context.Notifications
                .Where(n => n.NotificationReceiveuuid == uuidBytes && (n.NotificationReceivertype == "user" || n.NotificationReceivertype == "alluser") && !n.NotificationIsdeleted && n.NotificationType == type)
                .OrderByDescending(n => n.NotificationTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<Notification>> GetMerchantAllNotificationByUuidAndTypeWithPagingAsync(byte[] uuidBytes, string type, int pageNumber, int pageSize)
        {
            if (pageSize <= 0) pageSize = 10;
            if (pageNumber <= 0) pageNumber = 1;

            return await _context.Notifications
                .Where(n => n.NotificationReceiveuuid == uuidBytes && (n.NotificationReceivertype == "merchant" || n.NotificationReceivertype == "allmerchant") && !n.NotificationIsdeleted && n.NotificationType == type)
                .OrderByDescending(n => n.NotificationTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }*/
        public async Task<bool> AddNotificationAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddBatchNotificationAsync(List<Notification> notifications)
        {
            await _context.Notifications.AddRangeAsync(notifications);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateNotificationAsync(Notification notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteNotificationAsync(List<Notification> notifications)
        {
            _context.Notifications.RemoveRange(notifications);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
