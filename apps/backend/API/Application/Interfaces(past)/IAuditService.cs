using API.Domain.Entities.Models;

namespace API.Application.Interfaces
{
    public interface IAuditService
    {
        Task<bool> AddAudit(Audit audit);           //这里直接传入Audit的原因是
                                                    //Audit和AuditGroup需要组成一个重合服务
                                                    //来当作API下面的Service
                                                    //比如叫做AuditAndGroupService
                                                    //所以在上述Service中业务做好，整理后的数据再传入AuditService
        IQueryable<Audit> GetAudits();        //同上，这个Service的功能类似于Repository层的扩展，详细业务逻辑在重合服务中实现即可
        Task<bool> UpdateAudit(Audit audit);
        Task<bool> RemoveAudit(Audit audit);
        Task SaveChangesAsync();
    }
}
