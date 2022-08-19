using AirLines.Core.Helper;
using AirLines.Core.Transform;
using AirLines.Infrastructure.Data.repository;


namespace AirLines.Infrastructure.Data.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Core.Resources.BookResponse>> Get();
        Task<Core.Resources.BookResponse> GetById(int id);
        Task<bool> Add(Core.Resources.BookRequest Book);
        Task<bool> Put(int id, Core.Resources.BookRequest Book);
        Task<bool> Remove(int id);
        Task<bool> Exists(int id);
    }

    public partial class BookService : IBookService
    {
        private readonly repository.IBookRepository BookRepository;
        private readonly IPassagerRepository PassagerRepository;
        private readonly IFlightRepository FlightRepository;

        public BookService(IBookRepository repository
                          ,IPassagerRepository passagerRepository
                          ,IFlightRepository flightRepository
        )
        {
            this.BookRepository = repository;
            this.PassagerRepository = passagerRepository;
            this.FlightRepository = flightRepository;
        }
            

        public async Task<IEnumerable<Core.Resources.BookResponse>> Get()
        {
            var result = await this.BookRepository.GetAsync();
            //AutoMapper.Mapper.Map<TResponse>(query);
            return BookMap.TransfromObject(result);
        }

        public async Task<Core.Resources.BookResponse> GetById(int id)
        {
            var result = await this.BookRepository.GetByIdAsync(id);

            var response = BookMap.TransfromObject(result);

            if(result.Passager != null)
                response.Passager = PassagerMap.TransfromObject(result.Passager);

            if (result.Flight != null)
                response.Flight = FlightMap.TransfromObject(result.Flight);

            return response;
        }

        public async Task<bool> Add(Core.Resources.BookRequest book)
        {
            var passager = this.PassagerRepository.GetById(book.PassagerId);
            if (passager == null)
                throw new Exception("The Passager is not found it");

            var flight = this.FlightRepository.GetById(book.FlightId);
            if (flight == null)
                throw new Exception("The Flight is not found it");


            int age = Helper.CalculateAge(passager.BirthDate);

            if (age > flight.LimitAgeChildren)
                book.Price = flight.PriceChildren;
            else
                book.Price = flight.Price;

            var send = BookMap.TransfromObject(book);

            return await this.BookRepository.InsertAsync(send);
        }

        public async Task<bool> Put(int id, Core.Resources.BookRequest book)
        {
            var send = BookMap.TransfromObject(book);
            return await this.BookRepository.UpdateAsync(send);
        }

        public async Task<bool> Remove(int id)
        {
            var Book = await this.BookRepository.GetByIdAsync(id);   
            if(Book == null) return false;

            return await this.BookRepository.DeleteAsync(Book);
        }

        public async Task<bool> Exists(int id)
        {
            return await this.BookRepository.Exists(id);
        }
    }
}
