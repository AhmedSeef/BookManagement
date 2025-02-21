using AutoMapper;
using BookManagement.Data.Models;
using BookManagement.Data.UnitOfWork;
using BookManagement.Services.DTOs;
using BookManagement.Services.Interfaces;

namespace BookManagement.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Book>> GetPaginatedBooksAsync(int pageNumber, int pageSize)
        {
            return await _unitOfWork.Books.GetAllAsync(pageNumber, pageSize);
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null) return null;

            // Increment book views
            await _unitOfWork.Books.IncrementBookViewsAsync(id);

            // Calculate Popularity Score
            double popularityScore = (book.ViewsCount * 0.5) + ((DateTime.Now.Year - book.PublishedDate.Year) * 2);

            var bookDto = _mapper.Map<BookDto>(book);
            bookDto.PopularityScore = popularityScore; // Add calculated score

            return bookDto;
        }

        public async Task AddBookAsync(BookDto bookDto)
        {
            // Prevent duplicate books
            bool exists = await _unitOfWork.Books.ExistsByTitleAsync(bookDto.Title);
            if (exists)
                throw new Exception("A book with the same title already exists.");

            var book = _mapper.Map<Book>(bookDto);
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateBookAsync(int id, BookDto bookDto)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
                throw new Exception("Book not found.");

            _mapper.Map(bookDto, book);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            await _unitOfWork.Books.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task RestoreBookAsync(int id)
        {
            await _unitOfWork.Books.RestoreAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
