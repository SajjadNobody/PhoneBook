using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.DataLayer.Entities;
using PhoneBook.DataLayer.Repository;
using PhoneBook.Dtos;

namespace PhoneBook.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [EndpointGroupName("v2")]
    [Authorize]
    public class BookController : ControllerBase
    {
        #region Add Access to Data Base
        private readonly IUnitOfWork _unitOfWork;
        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var Book = _unitOfWork.BookRepository.GetAll();
            return Ok(Book);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> Get(int id)
        {
            var Book = _unitOfWork.BookRepository.GetById(id);
            return Ok(Book);
        }
        #endregion

        #region Post and Put
        [HttpPost]
        public async Task<IActionResult> Insert(BookDtos dto)
        {
            Book Book = new Book()
            {
                Name = dto.DtoName,
                PhoneNumber = dto.DtoPhoneNumber
            };
            await _unitOfWork.BookRepository.AddAsync(Book);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UserUpdate(BookDtos dto, int id)
        {
            var Book = await _unitOfWork.BookRepository.GetById(id);
            if (Book != null)
            {
                Book.Name = dto.DtoName;
                Book.PhoneNumber = dto.DtoPhoneNumber;
            }
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.BookRepository.Delete(id);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        #endregion
    }
}


