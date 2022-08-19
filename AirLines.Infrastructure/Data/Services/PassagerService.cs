using AirLines.Core.Transform;
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

        public async Task<IEnumerable<Core.Resources.PassagerResponse>> Get()
        {
            var result = await this.repository.GetAsync();
            //AutoMapper.Mapper.Map<TResponse>(query);
            return PassagerMap.TransfromObject(result);
        }

        public async Task<Core.Resources.PassagerResponse> GetById(int id)
        {
            var result = await this.repository.GetByIdAsync(id);
           
            var response = PassagerMap.TransfromObject(result);
            if (result.Books != null)
                response.Books = BookMap.TransfromObject(result.Books);

            return response;
        }

        public async Task<bool> Add(Core.Resources.PassagerRequest Passager)
        {
            var send = PassagerMap.TransfromObject(Passager);
            return await this.repository.InsertAsync(send);
        }

        public async Task<bool> Put(int id, Core.Resources.PassagerRequest Passager)
        {
            var send = PassagerMap.TransfromObject(Passager);
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
