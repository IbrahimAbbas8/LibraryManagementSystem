using AutoMapper;
using LibraryManagementSystem.API.Controllers;
using LibraryManagementSystem.API.Helper;
using LibraryManagementSystem.Core.Dtos;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.Sharing;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LibraryManagementSystem.Tests
{
    public class PatronsControllerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PatronsController _controller;

        public PatronsControllerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _controller = new PatronsController(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithPagination()
        {
            // Arrange
            var patronsDto = new List<GetPatronDto>
            {
                new GetPatronDto { Id = 1, Name = "Patron1" },
                new GetPatronDto { Id = 2, Name = "Patron2" }
            };

            Params param = new Params { PageNumber = 1, PageSize = 10, TotalItems = 2, Search = "", Sort = "" };

            _unitOfWorkMock.Setup(u => u.PatronRepository.GetAllAsync(It.IsAny<Params>()))
                           .ReturnsAsync(patronsDto);

            // Act
            var result = await _controller.Get(param);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var pagination = Assert.IsType<Pagination<GetPatronDto>>(okResult.Value);
            Assert.Equal(2, pagination.Count);
        }

        [Fact]
        public async Task GetPatron_ReturnsOk_WhenPatronExists()
        {
            // Arrange
            var patron = new Patron { Id = 1, Name = "Patron1", ContactInfo = new ContactInfo { Email = "a@b.com", Phone = "123", Address = "Addr" } };
            _unitOfWorkMock.Setup(u => u.PatronRepository.GetByIdAsync(1)).ReturnsAsync(patron);
            _mapperMock.Setup(m => m.Map<GetPatronDto>(It.IsAny<Patron>()))
                       .Returns(new GetPatronDto { Id = 1, Name = "Patron1" });

            // Act
            var result = await _controller.GetPatron(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var patronDto = Assert.IsType<GetPatronDto>(okResult.Value);
            Assert.Equal(1, patronDto.Id);
        }

        [Fact]
        public async Task Post_ReturnsOk_WhenModelIsValid()
        {
            // Arrange
            var patronDto = new PatronDto { Name = "New Patron", ContactInfo = new ContactInfoDto { Email = "new@patron.com", Phone = "123456", Address = "Address" } };
            var patronEntity = new Patron
            {
                Id = 1,
                Name = "New Patron",
                ContactInfo = new ContactInfo { Email = "new@patron.com", Phone = "123456", Address = "Address" }
            };

            _mapperMock.Setup(m => m.Map<Patron>(It.IsAny<PatronDto>())).Returns(patronEntity);

            _unitOfWorkMock.Setup(u => u.PatronRepository.AddAsync(It.IsAny<Patron>()))
                   .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Post(patronDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            var updateDto = new UpdatePatronDto { Id = 1, Name = "Updated Patron", ContactInfo = new ContactInfoDto { Email = "updated@patron.com", Phone = "987654", Address = "New Address" } };
            var existingPatron = new Patron
            {
                Id = 1,
                Name = "Old Patron",
                ContactInfo = new ContactInfo { Email = "old@patron.com", Phone = "111111", Address = "Old Address" }
            };

            _unitOfWorkMock.Setup(u => u.PatronRepository.GetByIdAsync(1)).ReturnsAsync(existingPatron);
            _unitOfWorkMock.Setup(u => u.PatronRepository.UpdateAsync(1, existingPatron)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(1, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(updateDto, okResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenPatronIsDeleted()
        {
            // Arrange
            var patron = new Patron { Id = 1, Name = "Patron1", ContactInfo = new ContactInfo { Email = "a@b.com", Phone = "123", Address = "Addr" } };
            _unitOfWorkMock.Setup(u => u.PatronRepository.GetByIdAsync(1)).ReturnsAsync(patron);
            _unitOfWorkMock.Setup(u => u.PatronRepository.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("deleted successfully", okResult.Value.ToString());
        }
    }
}
