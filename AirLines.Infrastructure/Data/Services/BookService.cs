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
        private readonly repository.IBookRepository repository;

        public BookService(IBookRepository repository)
        {
            this.repository = repository;
        }

        private List<Core.Resources.BookResponse> TransfromObject(IEnumerable<Core.Models.Book> models)
        {
            List<Core.Resources.BookResponse> results = new List<Core.Resources.BookResponse>();
            int len = models.Count();
            for (int i = 0; i < len; i++)
            {
                results.Add(TransfromObject(models.ElementAt(i)));
            }

            return results;
        }

        private Core.Resources.BookResponse TransfromObject(Core.Models.Book  model)
        {
            return new Core.Resources.BookResponse
            { 
                CheckIn = model.CheckIn,    
                CheckOut = model.CheckOut,
                Id = model.Id,  
                Date = model.Date,
                //Flight
                //Passager
              
               
            };
        }

        private Core.Models.Book TransfromObject(Core.Resources.BookRequest request)
        {
            return new Core.Models.Book 
            {
                CheckIn = request.CheckIn,
                CheckOut = request.CheckOut,
                Id = request.Id,
                Date = request.Date,
            };
        }

        public async Task<IEnumerable<Core.Resources.BookResponse>> Get()
        {
            var result = await this.repository.GetAsync();
            //AutoMapper.Mapper.Map<TResponse>(query);
            return TransfromObject(result);
        }

        public async Task<Core.Resources.BookResponse> GetById(int id)
        {
            var result = await this.repository.GetByIdAsync(id);

            return TransfromObject(result);
        }

        public async Task<bool> Add(Core.Resources.BookRequest Book)
        {
            var send = TransfromObject(Book);
            return await this.repository.InsertAsync(send);
        }

        public async Task<bool> Put(int id, Core.Resources.BookRequest Book)
        {
            var send = TransfromObject(Book);
            return await this.repository.UpdateAsync(send);
        }

        public async Task<bool> Remove(int id)
        {
            var Book = await this.repository.GetByIdAsync(id);   
            if(Book == null) return false;

            return await this.repository.DeleteAsync(Book);
        }

        public async Task<bool> Exists(int id)
        {
            return await this.repository.Exists(id);
        }
    }
}
