using BookManagement.Data.Models;

namespace BookManagement.Data.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync(int pageNumber, int pageSize);
        Task<Book> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
        Task<bool> ExistsByTitleAsync(string title);
        Task IncrementBookViewsAsync(int id);
    }
}
