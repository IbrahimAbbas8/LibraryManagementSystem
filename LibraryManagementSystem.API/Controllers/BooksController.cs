using AutoMapper;
using LibraryManagementSystem.API.Helper;
using LibraryManagementSystem.Core.Dtos;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.Sharing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BooksController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("get-all-books")]
        public async Task<ActionResult> Get([FromQuery] Params Params)
        {
            var books = await unitOfWork.BookRepository.GetAllAsync(Params);
            if (books is not null)
            {
                return Ok(new Pagination<GetBookDto>(Params.PageNumber, Params.PageSize, Params.TotalItems, books));
            }
            return BadRequest();
        }

        [HttpGet("get-books-by-id/{id}")]
        public async Task<ActionResult> GetBook(int id)
        {
            var book = await unitOfWork.BookRepository.GetByIdAsync(id);
            if (book is null)
            {
                return BadRequest($"Not Found This Id [{id}]");
            }
            var res = mapper.Map<GetBookDto>(book);
            return Ok(res);
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


        [HttpPut("update-exiting-book-by-id/{id}")]
        public async Task<IActionResult> Put(int id, UpdateBookDto bookDto)
        {
            try
            {
                if (id == bookDto.Id)
                {
                    if (ModelState.IsValid)
                    {
                        var book = await unitOfWork.BookRepository.GetByIdAsync(bookDto.Id);
                        if (book is not null)
                        {
                            mapper.Map(bookDto, book);
                            await unitOfWork.BookRepository.UpdateAsync(bookDto.Id, book);
                            return Ok(bookDto);
                        }
                    }
                }
                return BadRequest($"Book Not Found, Id {id} Incorrect");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var book = await unitOfWork.BookRepository.GetByIdAsync(id);
                if (book is not null)
                {
                    await unitOfWork.BookRepository.DeleteAsync(id);
                    return Ok($"This Book [{book.Title}] is Deleted Successfully");
                }
                return BadRequest("Not Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
