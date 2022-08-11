using PhoneBook.DataLayer.Context;
using PhoneBook.DataLayer.Entities;
using PhoneBook.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DataLayer.Services
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext Context) : base(Context)
        {
        }

    }
}
