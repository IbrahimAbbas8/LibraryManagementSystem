using AutoMapper;
using LibraryManagementSystem.API.Helper;
using LibraryManagementSystem.Core.Dtos;
using LibraryManagementSystem.Core.Entities;
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
    public class PatronsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PatronsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("get-all-patrons")]
        public async Task<ActionResult> Get([FromQuery] Params Params)
        {
            var patrons = await unitOfWork.PatronRepository.GetAllAsync(Params);
            if (patrons is not null)
            {
                return Ok(new Pagination<GetPatronDto>(Params.PageNumber, Params.PageSize, Params.TotalItems, patrons));
            }
            return BadRequest();
        }

        [HttpGet("get-patron-by-id/{id}")]
        public async Task<ActionResult> GetPatron(int id)
        {
            var patron = await unitOfWork.PatronRepository.GetByIdAsync(id);
            if (patron is null)
            {
                return BadRequest($"No patron found with id [{id}]");
            }
            var res = mapper.Map<GetPatronDto>(patron);
            return Ok(res);
        }

        [HttpPost("add-new-patron")]
        public async Task<ActionResult> Post(PatronDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = mapper.Map<Patron>(dto);
                    await unitOfWork.PatronRepository.AddAsync(entity);
                    return Ok(dto);
                }
                return BadRequest(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-existing-patron-by-id/{id}")]
        public async Task<IActionResult> Put(int id, UpdatePatronDto patronDto)
        {
            try
            {
                if (id == patronDto.Id)
                {
                    if (ModelState.IsValid)
                    {
                        var patron = await unitOfWork.PatronRepository.GetByIdAsync(patronDto.Id);
                        if (patron is not null)
                        {
                            mapper.Map(patronDto, patron);
                            await unitOfWork.PatronRepository.UpdateAsync(patronDto.Id, patron);
                            return Ok(patronDto);
                        }
                    }
                }
                return BadRequest($"Patron not found or invalid id [{id}]");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-patron-by-id/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var patron = await unitOfWork.PatronRepository.GetByIdAsync(id);
                if (patron is not null)
                {
                    await unitOfWork.PatronRepository.DeleteAsync(id);
                    return Ok($"The patron [{patron.Name}] has been deleted successfully");
                }
                return BadRequest("Patron not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
