using AirLines.Infrastructure.Data.repository;


namespace AirLines.Infrastructure.Data.Services
{
    public interface IAirPortService
    {
        Task<IEnumerable<Core.Resources.AirPortResorce>> Get();
        Task<Core.Resources.AirPortResorce> GetById(int id);
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

        private List<Core.Resources.AirPortResorce> TransfromObject(IEnumerable<Core.Models.AirPort> models)
        {
            List<Core.Resources.AirPortResorce> results = new List<Core.Resources.AirPortResorce>();
            int len = models.Count();
            for (int i = 0; i < len; i++)
            {
                results.Add(TransfromObject(models.ElementAt(i)));
            }

            return results;
        }

        private Core.Resources.AirPortResorce TransfromObject(Core.Models.AirPort  model)
        {
            return new Core.Resources.AirPortResorce
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        private Core.Models.AirPort TransfromObject(Core.Resources.AirPortRequest request)
        {
            return new Core.Models.AirPort 
            {
                Id = request.Id,
                Name = request.Name
            };
        }

        public async Task<IEnumerable<Core.Resources.AirPortResorce>> Get()
        {
            var result = await this.repository.GetAsync();
            //AutoMapper.Mapper.Map<TResponse>(query);
            return TransfromObject(result);
        }

        public async Task<Core.Resources.AirPortResorce> GetById(int id)
        {
            var result = await this.repository.GetByIdAsync(id);

            return TransfromObject(result);
        }

        public async Task<bool> Add(Core.Resources.AirPortRequest airPort)
        {
            var send = TransfromObject(airPort);
            return await this.repository.InsertAsync(send);
        }

        public async Task<bool> Put(int id, Core.Resources.AirPortRequest airPort)
        {
            var send = TransfromObject(airPort);
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
