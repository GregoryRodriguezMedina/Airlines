using AirLines.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AirLines.Infrastructure.Data.repository
{
    public interface IPassagerRepository : Core.Repository.IEfRepositoryBase<Core.Models.Passager>
    {
        Task<bool> Exists(int id);
    }
    public partial class PassagerRepository : Core.Repository.EfRepositoryBase<Core.Models.Passager>, IPassagerRepository
    {
        public PassagerRepository(DbContext context) : base(context)
        {
        }

        public async Task<bool> Exists(int id)
        {
            return await this.Entity.AnyAsync(x => x.Id == id);
        }

        public override async Task<Passager> GetByIdAsync(int key)
        {
            return await this.Entity
                             .Include(b=> b.Books)
                             .FirstOrDefaultAsync(a => a.Id == key);                              
        }
    }
}
