﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.DataLayer.Entities;
using PhoneBook.DataLayer.Repository;
using PhoneBook.Dtos;
using PhoneBook.Services.Models;
using PhoneBook.Services.Services;

namespace PhoneBook.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [EndpointGroupName("v1")]
    [Authorize]
    public class UserController : ControllerBase
    {
        #region Add Access to Data Base
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenBuilder _jwtService;

        public UserController(IUnitOfWork unitOfWork, IJwtTokenBuilder jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }
        #endregion

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var user = await _unitOfWork.UserRepository.GetAll();
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<string>> Login(string UserName, string Password, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByNameAndPass(UserName, Password, cancellationToken);
            if (user == null)
                return BadRequest();

            var jwt = _jwtService.JwtGenerations(user);
            return jwt.access_token;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<TokenViewModel>> LoginSwagger([FromForm] SwaggerTokenRequest req, CancellationToken cancellationToken)
        {

            var user = await _unitOfWork.UserRepository.GetByNameAndPass(req.username, req.password, cancellationToken);
            if (user == null)
                return BadRequest();

            var jwt = _jwtService.JwtGenerations(user);

            return jwt;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> Get(int id, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            return Ok(user);
        }
        #endregion

        #region Post and Put
        [HttpPost]
        public async Task<IActionResult> Insert(UserDtos dto, CancellationToken cancellationToken)
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
        public async Task<IActionResult> UserUpdate(UserDtos dto, int id, CancellationToken cancellationToken)
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
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            await _unitOfWork.UserRepository.Delete(id);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        #endregion
    }
}
