using AirLines.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace AirLines.Infrastructure.Data.repository
{   public interface IBookRepository : Core.Repository.IEfRepositoryBase<Core.Models.Book>
    {
        Task<bool> Exists(int id);
    }
    public partial class BookRepository : Core.Repository.EfRepositoryBase<Core.Models.Book>, IBookRepository
    {
        public BookRepository(DbContext context) : base(context)
        {
        }

        public async Task<bool> Exists(int id)
        {
            return await this.Entity.AnyAsync(x => x.Id == id);
        }

        public override async Task<Book> GetByIdAsync(int key)
        {
           return await  this.Entity
                                    .Include(a => a.Flight)
                                    .Include(b => b.Passager)
                                    .FirstOrDefaultAsync(c => c.Id == key);                          
                       
        }
    }
}
