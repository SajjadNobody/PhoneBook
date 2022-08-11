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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext Context) : base(Context)
        {
        }

        public async Task<User> GetByNameAndPass(string userName, string password, CancellationToken cancellationToken)
        {
          var user = await _context.User.Where(x => x.UserName == userName && x.Password == password)
                .SingleOrDefaultAsync(cancellationToken);
            return user;
        }
    }
}
