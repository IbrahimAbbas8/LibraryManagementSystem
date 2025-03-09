using LibraryManagementSystem.API.Helper;
using LibraryManagementSystem.Core.Dtos;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.Sharing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("get-all-books")]
        public async Task<ActionResult> Get([FromQuery] Params Params)
        {
            var books = await unitOfWork.BookRepository.GetAllAsync(Params);
            if (books is not null)
            {
                return Ok(new Pagination<BookDto>(Params.PageNumber, Params.PageSize, Params.TotalItems, books));
            }
            return BadRequest();
        }


        [HttpPost("add-new-book")]
        public async Task<ActionResult> Post(BookDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await unitOfWork.BookRepository.AddAsync(dto);
                    return res ? Ok(dto) : BadRequest();
                }
                return BadRequest(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
