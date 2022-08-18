using AirLines.Infrastructure.Data.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLines.Infrastructure.Data.Services
{
    public interface IAirPortService
    {
        Task<IEnumerable<Core.Models.AirPort>> Get();
        Task<Core.Models.AirPort> GetById(int id);
        Task<bool> Add(Core.Models.AirPort airPort);
        Task<bool> Put(int id, Core.Models.AirPort airPort);
        Task<bool> Remove(int id);
        Task<bool> Exists(int id);
    }

    public partial class AirPortService : IAirPortService
    {
        private readonly repository.IAirPortRepository repository;

        public AirPortService(IAirPortRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Core.Models.AirPort>> Get()
        {
            return await this.repository.GetAsync();
        }

        public async Task<Core.Models.AirPort> GetById(int id)
        {
            return await this.repository.GetByIdAsync(id);
        }

        public async Task<bool> Add(Core.Models.AirPort airPort)
        {
            return await this.repository.InsertAsync(airPort);
        }

        public async Task<bool> Put(int id, Core.Models.AirPort airPort)
        {
            return await this.repository.UpdateAsync(airPort);
        }

        public async Task<bool> Remove(int id)
        {
            var airPort = await this.repository.GetByIdAsync(id);   
            if(airPort == null) return false;

            return await this.repository.DeleteAsync(airPort);
        }

        public async Task<bool> Exists(int id)
        {
            return await this.repository.Exists(id);
        }
    }
}
