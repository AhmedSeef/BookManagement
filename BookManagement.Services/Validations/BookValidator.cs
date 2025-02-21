using BookManagement.Services.DTOs;
using FluentValidation;

namespace BookManagement.Services.Validations
{
    public class BookValidator : AbstractValidator<BookDto>
    {
        public BookValidator()
        {
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");

            RuleFor(book => book.Author)
                .NotEmpty().WithMessage("Author is required.");

            RuleFor(book => book.ISBN)
                .Matches(@"^\d{3}-\d{10}$").WithMessage("ISBN must be in format 'XXX-XXXXXXXXXX'.");
        }
    }
}
