using AirLines.Infrastructure.Data.repository;


namespace AirLines.Infrastructure.Data.Services
{
    public interface IPassagerService
    {
        Task<IEnumerable<Core.Resources.PassagerResponse>> Get();
        Task<Core.Resources.PassagerResponse> GetById(int id);
        Task<bool> Add(Core.Resources.PassagerRequest Passager);
        Task<bool> Put(int id, Core.Resources.PassagerRequest Passager);
        Task<bool> Remove(int id);
        Task<bool> Exists(int id);
    }

    public partial class PassagerService : IPassagerService
    {
        private readonly repository.IPassagerRepository repository;

        public PassagerService(IPassagerRepository repository)
        {
            this.repository = repository;
        }

        private List<Core.Resources.PassagerResponse> TransfromObject(IEnumerable<Core.Models.Passager> models)
        {
            List<Core.Resources.PassagerResponse> results = new List<Core.Resources.PassagerResponse>();
            int len = models.Count();
            for (int i = 0; i < len; i++)
            {
                results.Add(TransfromObject(models.ElementAt(i)));
            }

            return results;
        }

        private Core.Resources.PassagerResponse TransfromObject(Core.Models.Passager  model)
        {
            return new Core.Resources.PassagerResponse
            {
               BirthDate = model.BirthDate,
               FirstName = model.FirstName,
               LastName = model.LastName,
               Id = model.Id,
               //TODO: crear metodo para agregar los book
            };
        }

        private Core.Models.Passager TransfromObject(Core.Resources.PassagerRequest request)
        {
            return new Core.Models.Passager 
            {
                BirthDate = request.BirthDate,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Id = request.Id,
            };
        }

        public async Task<IEnumerable<Core.Resources.PassagerResponse>> Get()
        {
            var result = await this.repository.GetAsync();
            //AutoMapper.Mapper.Map<TResponse>(query);
            return TransfromObject(result);
        }

        public async Task<Core.Resources.PassagerResponse> GetById(int id)
        {
            var result = await this.repository.GetByIdAsync(id);

            return TransfromObject(result);
        }

        public async Task<bool> Add(Core.Resources.PassagerRequest Passager)
        {
            var send = TransfromObject(Passager);
            return await this.repository.InsertAsync(send);
        }

        public async Task<bool> Put(int id, Core.Resources.PassagerRequest Passager)
        {
            var send = TransfromObject(Passager);
            return await this.repository.UpdateAsync(send);
        }

        public async Task<bool> Remove(int id)
        {
            var Passager = await this.repository.GetByIdAsync(id);   
            if(Passager == null) return false;

            return await this.repository.DeleteAsync(Passager);
        }

        public async Task<bool> Exists(int id)
        {
            return await this.repository.Exists(id);
        }
    }
}
