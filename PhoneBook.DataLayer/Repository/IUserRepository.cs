using PhoneBook.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DataLayer.Repository
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<User> GetByNameAndPass(string UserName, string password,CancellationToken cancellationToken);
    }
}
