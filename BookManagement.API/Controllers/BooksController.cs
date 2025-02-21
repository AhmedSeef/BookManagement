using BookManagement.Data.Models;
using BookManagement.Services.DTOs;
using BookManagement.Services.Exceptions;
using BookManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IBookService bookService, ILogger<BooksController> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }

    // ✅ GET: api/Books - Retrieve all books (with optional pagination)
    [HttpGet]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("Page number and page size must be greater than zero.");

        var books = await _bookService.GetPaginatedBooksAsync(pageNumber, pageSize);

        return Ok(books);
    }

    // ✅ GET: api/Books/{id} - Retrieve a single book by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBook(int id)
    {
        try
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                _logger.LogWarning($"Book with ID {id} not found.");
                return NotFound(new { message = $"Book with ID {id} not found." });
            }

            return Ok(book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching book with ID {id}");
            return StatusCode(500, new { message = "An error occurred while retrieving the book." });
        }
    }

    // ✅ POST: api/Books - Create a new book
    [HttpPost]
    public async Task<ActionResult> CreateBook([FromBody] BookDto bookDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _bookService.AddBookAsync(bookDto);
            return Created();
        }
        catch (DuplicateBookException ex)
        {
            _logger.LogWarning(ex, "Duplicate book entry attempted.");
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating book");
            return StatusCode(500, new { message = "An error occurred while creating the book." });
        }
    }

    // ✅ PUT: api/Books/{id} - Update an existing book
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDto bookDto)
    {
        if (id != bookDto.Id)
            return BadRequest(new { message = "Book ID mismatch." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _bookService.UpdateBookAsync(id, bookDto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            _logger.LogWarning($"Update failed. Book with ID {id} not found.");
            return NotFound(new { message = $"Book with ID {id} not found." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating book with ID {id}");
            return StatusCode(500, new { message = "An error occurred while updating the book." });
        }
    }

    // ✅ DELETE: api/Books/{id} - Delete a book (Soft Delete)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                _logger.LogWarning($"Delete failed. Book with ID {id} not found.");
                return NotFound(new { message = $"Book with ID {id} not found." });
            }

            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting book with ID {id}");
            return StatusCode(500, new { message = "An error occurred while deleting the book." });
        }
    }
}
