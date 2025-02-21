namespace BookManagement.Services.Exceptions
{
    public class DuplicateBookException : Exception
    {
        public DuplicateBookException() : base("A book with the same title already exists.") { }

        public DuplicateBookException(string message) : base(message) { }

        public DuplicateBookException(string message, Exception innerException) : base(message, innerException) { }
    }
}
