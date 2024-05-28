using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal sealed class AuditRepository : RepositoryBase<AuditLog>, IAuditRepository
    {
        public AuditRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.Timestamp).ToListAsync();
        }


    }
}
