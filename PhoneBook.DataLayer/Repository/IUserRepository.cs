using PhoneBook.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DataLayer.Repository
{
    public interface IUserRepository: IGenericRepository<User> // add inheritance to generic repository
    {
        // add other methods. Apart from generic repository methods 
        Task<User> GetByNameAndPass(string UserName, string password,CancellationToken cancellationToken);
    }
}
