using PhoneBook.DataLayer.Entities;
using PhoneBook.DataLayer.Repository;
using PhoneBook.DataLayer.Services;

namespace PhoneBook.DataLayer.Context
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Add Access to ApplicationDbContext and Add Repsitories
        public readonly ApplicationDbContext _context;

        // Add Interface Repositories 
        public IUserRepository UserRepository { get; }
        public IBookRepository BookRepository { get; }

        // make constroctor for set value to our property
        public UnitOfWork(ApplicationDbContext Context, IUserRepository userRepository,IBookRepository bookRepository)
        {
            // Set Value to our interface
            UserRepository = userRepository;
            BookRepository = bookRepository;
            _context = Context;
        }
        #endregion

        // For Have save mthod in unit of work 
        // you can see how can you use this mehod in Controllers 
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // This method will dispose all object that we made here after task compeleted
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
