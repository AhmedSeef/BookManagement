using BookManagement.Data.Repositories.Implementations;
using BookManagement.Data.Repositories.Interfaces;

namespace BookManagement.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IBookRepository _bookRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IBookRepository Books => _bookRepository ??= new BookRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
