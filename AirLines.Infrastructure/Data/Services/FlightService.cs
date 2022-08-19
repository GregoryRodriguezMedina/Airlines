using AirLines.Core.Transform;
using AirLines.Infrastructure.Data.repository;


namespace AirLines.Infrastructure.Data.Services
{
    public enum Status
    {
        Bording = 0,
        Depart = 1,
        Arrive = 2,
        
    }

    public enum FlightInclude
    {
        Books = 0,
        FromAirPort = 1,
        ToAirPort = 2,
    }


    public interface IFlightService
    {
        Task<IEnumerable<Core.Resources.FlightResponse>> Get();
        Task<Core.Resources.FlightResponse> GetById(int id);
        Task<bool> Add(Core.Resources.FlightRequest Flight);
        Task<bool> Put(int id, Core.Resources.FlightRequest Flight);
        Task<bool> Remove(int id);
        Task<bool> Exists(int id);
        Task<IEnumerable<Core.Resources.FlightResponse>> Get(int? id, DateTime? from, DateTime? to, DateTime? depart, DateTime? arrival);
        Task<Core.Resources.FlightResponse> GetById(int id, string[] includes);
    }

    public partial class FlightService : IFlightService
    {
        private readonly repository.IFlightRepository repository;
        private Status DefaultStatus = Status.Bording;
        public FlightService(IFlightRepository repository)
        {
            this.repository = repository;
        }      

        public async Task<IEnumerable<Core.Resources.FlightResponse>> Get(int? id, DateTime? from, DateTime? to, DateTime? depart, DateTime? arrival)
        {
            var result = await this.repository.GetAsync(id, from, to, depart, arrival);
            //AutoMapper.Mapper.Map<TResponse>(query);
            return FlightMap.TransfromObject(result);
        }
        public async Task<IEnumerable<Core.Resources.FlightResponse>> Get()
        {
            var result = await this.repository.GetAsync();
            //AutoMapper.Mapper.Map<TResponse>(query);
            return FlightMap.TransfromObject(result);
        }

        public async Task<Core.Resources.FlightResponse> GetById(int id)
        {
            var result = await this.repository.GetByIdAsync(id);

            return FlightMap.TransfromObject(result);
        }

        public async Task<Core.Resources.FlightResponse> GetById(int id, string[] includes)
        {
            bool includeBook = false;
            bool FromAirPort = false;
            bool ToAirPort = false;
            foreach (var include in includes)
            {
                if (include == FlightInclude.Books.ToString())
                    includeBook = true;
                else if (include == FlightInclude.FromAirPort.ToString())
                    FromAirPort = true;
                else if (include == FlightInclude.ToAirPort.ToString())
                    ToAirPort = true;
            }

            
            var result = await this.repository.GetByIdAsync(id, includeBook, FromAirPort, ToAirPort);

            var response = FlightMap.TransfromObject(result);
            if(result.Books != null)
                response.Books = BookMap.TransfromObject(result.Books);

            if(response.FromAirPort!= null)
                response.FromAirPort = AirPortMap.TransfromObject(result.FromIdAirPortNavigation);

            if (response.ToAirPort != null)
                response.ToAirPort = AirPortMap.TransfromObject(result.ToIdAirPortNavigation);

            return response;
        }

        public async Task<bool> Add(Core.Resources.FlightRequest Flight)
        {
            var send = FlightMap.TransfromObject(Flight);
            return await this.repository.InsertAsync(send);
        }

        public async Task<bool> Put(int id, Core.Resources.FlightRequest Flight)
        {
            var send = FlightMap.TransfromObject(Flight);
            return await this.repository.UpdateAsync(send);
        }

        public async Task<bool> Remove(int id)
        {
            var Flight = await this.repository.GetByIdAsync(id);   
            if(Flight == null) return false;

            return await this.repository.DeleteAsync(Flight);
        }

        public async Task<bool> Exists(int id)
        {
            return await this.repository.Exists(id);
        }
    }
}
