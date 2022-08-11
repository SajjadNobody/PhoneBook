using Microsoft.EntityFrameworkCore;
using PhoneBook.DataLayer.Context;
using PhoneBook.DataLayer.Entities;
using PhoneBook.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DataLayer.Services
{
    // inheritance this method to generic repository for having all mehods in this class in the future
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        // add constroctor ** it's necessary **
        public UserRepository(ApplicationDbContext Context) : base(Context)
        {
        }

        public async Task<User> GetByNameAndPass(string userName, string password, CancellationToken cancellationToken)
        {
          var user = await _context.User.Where(x => x.UserName == userName && x.Password == password)
                .SingleOrDefaultAsync(cancellationToken);
            return user!; // the (!) it mean, this data type can't be null 
        }
    }
}
