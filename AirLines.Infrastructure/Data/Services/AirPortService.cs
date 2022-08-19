using AirLines.Core.Transform;
using AirLines.Infrastructure.Data.repository;


namespace AirLines.Infrastructure.Data.Services
{
    public interface IAirPortService
    {
        Task<IEnumerable<Core.Resources.AirPortResponse>> Get();
        Task<Core.Resources.AirPortResponse> GetById(int id);
        Task<bool> Add(Core.Resources.AirPortRequest airPort);
        Task<bool> Put(int id, Core.Resources.AirPortRequest airPort);
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
      

        public async Task<IEnumerable<Core.Resources.AirPortResponse>> Get()
        {
            var result = await this.repository.GetAsync();
            //AutoMapper.Mapper.Map<TResponse>(query);
            return AirPortMap.TransfromObject(result);
        }

        public async Task<Core.Resources.AirPortResponse> GetById(int id)
        {
            var result = await this.repository.GetByIdAsync(id);

            return AirPortMap.TransfromObject(result);
        }

        public async Task<bool> Add(Core.Resources.AirPortRequest airPort)
        {
            var send = AirPortMap.TransfromObject(airPort);
            return await this.repository.InsertAsync(send);
        }

        public async Task<bool> Put(int id, Core.Resources.AirPortRequest airPort)
        {
            var send = AirPortMap.TransfromObject(airPort);
            return await this.repository.UpdateAsync(send);
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
