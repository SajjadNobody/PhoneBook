using PhoneBook.DataLayer.Entities;
using PhoneBook.Services.Models;

namespace PhoneBook.Services.Services
{
    public interface IJwtTokenBuilder
    {
        TokenViewModel JwtGenerations(User user);
    }
}