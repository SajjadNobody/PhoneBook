using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.DataLayer.Entities;
using PhoneBook.DataLayer.Repository;
using PhoneBook.Dtos;
using PhoneBook.Services.Models;
using PhoneBook.Services.Services;
using PhoneBook.WebFramework.ApiResult;

namespace PhoneBook.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    /* [EndpointGroupName("v1")]Set version for grouping Controllers */
    [Authorize]
    public class UserController : ControllerBase
    {
        #region Add Access to Data Base and JwtBuilder
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork, IJwtTokenBuilder jwtService)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Get
        [HttpGet]
        public async Task<ApiResult<IEnumerable<User>>> Get()
        {
            var user = await _unitOfWork.UserRepository.GetAll();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<IEnumerable<User>>> Get(int id, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            return Ok(user);
        }
        #endregion

        #region Post and Put
        [HttpPost]
        public async Task<ApiResult<IActionResult>> Insert(UserDtos dto, CancellationToken cancellationToken)
        {
            User user = new User()
            {
                UserName = dto.UserName,
                Password = dto.Password
            };
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<ApiResult<IActionResult>> UserUpdate(UserDtos dto, int id, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            if (user != null)
            {
                user.UserName = dto.UserName;
                user.Password = dto.Password;
            }
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ApiResult<IActionResult>> Delete(int id, CancellationToken cancellationToken)
        {
            await _unitOfWork.UserRepository.Delete(id);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        #endregion
    }
}
