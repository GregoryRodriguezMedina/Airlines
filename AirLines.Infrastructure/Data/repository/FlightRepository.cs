using Microsoft.EntityFrameworkCore;

namespace AirLines.Infrastructure.Data.repository
{
 public interface IFlightRepository : Core.Repository.IEfRepositoryBase<Core.Models.Flight>
    {
        Task<bool> Exists(int id);
        Task<IEnumerable<Core.Models.Flight>> GetAsync(int? id, DateTime? from, DateTime? to, DateTime? depart, DateTime? arrival);
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

        public async Task<IEnumerable<Core.Models.Flight>> GetAsync(int? id, DateTime? from, DateTime? to, DateTime? depart, DateTime? arrival)
        {
            var query = this.Entity;
            if(id != null)
                query.Where(a => a.Id == id);
            if (from != null)
                query.Where(a => a.Date >= from);
            if(to != null)               
                query.Where(a=> a.Date <= to);
            if (depart != null)
                query.Where(a => a.DepartTime == depart);
            if (arrival != null)
                query.Where(a => a.ArrivalTime == arrival);

            return await query.ToArrayAsync();
        }
    }
}
