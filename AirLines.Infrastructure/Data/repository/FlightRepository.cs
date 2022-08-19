using Microsoft.EntityFrameworkCore;

namespace AirLines.Infrastructure.Data.repository
{
 public interface IFlightRepository : Core.Repository.IEfRepositoryBase<Core.Models.Flight>
    {
        Task<bool> Exists(int id);
    }
    public partial class FlightRepository : Core.Repository.EfRepositoryBase<Core.Models.Flight>, IFlightRepository
    {
        public FlightRepository(DbContext context) : base(context)
        {
        }

        public async Task<bool> Exists(int id)
        {
            return await this.Entity.AnyAsync(x => x.Id == id);
        }
    }
}
