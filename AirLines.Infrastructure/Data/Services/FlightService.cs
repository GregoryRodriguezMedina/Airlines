using AirLines.Infrastructure.Data.repository;


namespace AirLines.Infrastructure.Data.Services
{
    public enum Status
    {
        Bording = 0,
        Depart = 1,
        Arrive = 2,
        
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
    }

    public partial class FlightService : IFlightService
    {
        private readonly repository.IFlightRepository repository;
        private Status DefaultStatus = Status.Bording;
        public FlightService(IFlightRepository repository)
        {
            this.repository = repository;
        }

        private List<Core.Resources.FlightResponse> TransfromObject(IEnumerable<Core.Models.Flight> models)
        {
            List<Core.Resources.FlightResponse> results = new List<Core.Resources.FlightResponse>();
            int len = models.Count();
            for (int i = 0; i < len; i++)
            {
                results.Add(TransfromObject(models.ElementAt(i)));
            }

            return results;
        }

        private Core.Resources.FlightResponse TransfromObject(Core.Models.Flight  model)
        {
            return new Core.Resources.FlightResponse
            { 
               Date = model.Date,
               Id = model.Id,   
               ArrivalTime = model.ArrivalTime,
               ArriveConfirmed = model.ArriveConfirmed, 
               BoardingTime = model.BoardingTime,
               Code = model.Code,   
               DepartTime = model.DepartTime,
               LimitAgeChildren = model.LimitAgeChildren,
               MinutesToArrive = model.MinutesToArrive,
               Price = model.Price,
               PriceChildren = model.PriceChildren,
               Status = model.Status,
               //FromIdAirPortNavigation
               //ToIdAirPortNavigation
            };
        }

        private Core.Models.Flight TransfromObject(Core.Resources.FlightRequest request)
        {
            return new Core.Models.Flight 
            {
                Date = request.Date,
                Id = request.Id,
                ArrivalTime = request.ArrivalTime,
                ArriveConfirmed = request.ArriveConfirmed,
                BoardingTime = request.BoardingTime,
                Code = request.Code,
                DepartTime = request.DepartTime,
                LimitAgeChildren = request.LimitAgeChildren,
                MinutesToArrive = request.MinutesToArrive,
                Price = request.Price,
                PriceChildren = request.PriceChildren,
                Status = (int)this.DefaultStatus
            };
        }

        public async Task<IEnumerable<Core.Resources.FlightResponse>> Get(int? id, DateTime? from, DateTime? to, DateTime? depart, DateTime? arrival)
        {
            var result = await this.repository.GetAsync(id, from, to, depart, arrival);
            //AutoMapper.Mapper.Map<TResponse>(query);
            return TransfromObject(result);
        }
        public async Task<IEnumerable<Core.Resources.FlightResponse>> Get()
        {
            var result = await this.repository.GetAsync();
            //AutoMapper.Mapper.Map<TResponse>(query);
            return TransfromObject(result);
        }

        public async Task<Core.Resources.FlightResponse> GetById(int id)
        {
            var result = await this.repository.GetByIdAsync(id);

            return TransfromObject(result);
        }

        public async Task<bool> Add(Core.Resources.FlightRequest Flight)
        {
            var send = TransfromObject(Flight);
            return await this.repository.InsertAsync(send);
        }

        public async Task<bool> Put(int id, Core.Resources.FlightRequest Flight)
        {
            var send = TransfromObject(Flight);
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
