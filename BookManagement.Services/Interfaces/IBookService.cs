using BookManagement.Data.Models;
using BookManagement.Services.DTOs;

namespace BookManagement.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetPaginatedBooksAsync(int pageNumber, int pageSize);
        Task<BookDto> GetBookByIdAsync(int id);
        Task AddBookAsync(BookDto bookDto);
        Task UpdateBookAsync(int id, BookDto bookDto);
        Task DeleteBookAsync(int id);
    }
}
