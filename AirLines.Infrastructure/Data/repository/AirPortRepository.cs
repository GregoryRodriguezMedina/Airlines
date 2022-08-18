using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLines.Infrastructure.Data.repository
{
    public interface IAirPortRepository: Core.Repository.IEfRepositoryBase<Core.Models.AirPort>
    {
      Task<bool> Exists(int id);
    }
    public partial class AirPortRepository: Core.Repository.EfRepositoryBase<Core.Models.AirPort>, IAirPortRepository
    {
        public AirPortRepository(DbContext context) : base(context)
        {
        }

        public async Task<bool> Exists(int id)
        {
           return await this.Entity.AnyAsync(x => x.Id == id);
        }
    }
}
