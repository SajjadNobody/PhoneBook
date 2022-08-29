using Microsoft.AspNetCore.Mvc;
using PhoneBook.DataLayer.Repository;
using PhoneBook.Services.Models;
using PhoneBook.Services.Services;
using PhoneBook.WebFramework.ApiResult;

namespace PhoneBook.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLogin : ControllerBase
    {
        private readonly IJwtTokenBuilder _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public UserLogin(IUnitOfWork unitOfWork, IJwtTokenBuilder jwtService)
        {
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("[action]")]
        public async Task<ApiResult<TokenViewModel>> Login([FromForm] SwaggerTokenRequest req, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByNameAndPass(req.username, req.password, cancellationToken);
            if (user == null)
                return BadRequest();

            var jwt = _jwtService.JwtGenerations(user);

            return jwt;
        }
    }
}
