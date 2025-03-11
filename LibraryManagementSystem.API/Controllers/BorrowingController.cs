using LibraryManagementSystem.API.Helper;
using LibraryManagementSystem.Core.Dtos;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.Sharing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api")]
    [Authorize]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public BorrowingController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("get-all-BorrowingRecord")]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await unitOfWork.BorrowingRepository.GetAllAsync());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("borrow/{bookId}/patron/{patronId}")]
        public async Task<IActionResult> BorrowBook(int bookId, int patronId)
        {
            try
            {
                var record = await unitOfWork.BorrowingRepository.BorrowBook(bookId, patronId);
                return Ok(record);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("return/{bookId}/patron/{patronId}")]
        public async Task<IActionResult> ReturnBook(int bookId, int patronId)
        {
            try
            {
                var record = await unitOfWork.BorrowingRepository.ReturnBook(bookId, patronId);
                return Ok(record);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
