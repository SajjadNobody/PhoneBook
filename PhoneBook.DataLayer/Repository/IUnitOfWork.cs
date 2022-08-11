using PhoneBook.DataLayer.Entities;
using PhoneBook.DataLayer.Services;

namespace PhoneBook.DataLayer.Repository
{
    public interface IUnitOfWork
    {
        //GenericRepository<Book> BookRepository { get; }
        //GenericRepository<User> UserRepository { get; }
        IBookRepository BookRepository { get; }
        IUserRepository UserRepository { get; }
        Task SaveAsync();
    }
}