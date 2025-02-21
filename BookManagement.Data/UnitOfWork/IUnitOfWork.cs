using BookManagement.Data.Repositories.Interfaces;

namespace BookManagement.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBookRepository Books { get; }
        Task<int> CompleteAsync();
    }
}
