using AirLines.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace AirLines.Infrastructure.Data.repository
{   public interface IBookRepository : Core.Repository.IEfRepositoryBase<Core.Models.Book>
    {
        Task<bool> Exists(int id);
        Task<Book> GetByIdAsync(int key, bool includeFlight, bool includePassager);
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

        public async Task<Book> GetByIdAsync(int key, bool includeFlight, bool includePassager)
        {
            var query = this.Entity;
            if (includeFlight)
                query.Include(a => a.Flight);

            if (includePassager)
                query.Include(b => b.Passager);
                                    
            return await query.FirstOrDefaultAsync(c => c.Id == key);                          
                       
        }
    }
}
