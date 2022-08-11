using PhoneBook.DataLayer.Entities;
using PhoneBook.DataLayer.Repository;
using PhoneBook.DataLayer.Services;

namespace PhoneBook.DataLayer.Context
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Add Access to ApplicationDbContext and Add Repsitories
        public readonly ApplicationDbContext _context;
        public IUserRepository UserRepository { get; }
        public IBookRepository BookRepository { get; }
        public UnitOfWork(ApplicationDbContext Context, IUserRepository userRepository,IBookRepository bookRepository)
        {
            UserRepository = userRepository;
            BookRepository = bookRepository;
            _context = Context;
        }
        #endregion

    
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
