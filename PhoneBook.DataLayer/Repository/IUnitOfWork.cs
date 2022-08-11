using PhoneBook.DataLayer.Entities;
using PhoneBook.DataLayer.Services;

namespace PhoneBook.DataLayer.Repository
{
    public interface IUnitOfWork
    {
        // the interfaces that we want to use them in Controllers
        IBookRepository BookRepository { get; }
        IUserRepository UserRepository { get; }
        Task SaveAsync();
    }
}