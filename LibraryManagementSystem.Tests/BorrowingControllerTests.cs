using LibraryManagementSystem.API.Controllers;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LibraryManagementSystem.Tests
{
    public class BorrowingControllerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly BorrowingController _controller;

        public BorrowingControllerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _controller = new BorrowingController(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithBorrowingRecords()
        {
            // Arrange
            var records = new List<BorrowingRecord>
            {
                new BorrowingRecord { Id = 1, BookId = 1, PatronId = 1, BorrowDate = DateTime.Now },
                new BorrowingRecord { Id = 2, BookId = 2, PatronId = 2, BorrowDate = DateTime.Now }
            };
            _unitOfWorkMock.Setup(u => u.BorrowingRepository.GetAllAsync()).ReturnsAsync(records);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRecords = Assert.IsAssignableFrom<IEnumerable<BorrowingRecord>>(okResult.Value);
            Assert.Equal(2, returnedRecords.Count());
        }

        [Fact]
        public async Task BorrowBook_ReturnsOk_WhenBorrowingIsSuccessful()
        {
            // Arrange
            int bookId = 1, patronId = 1;
            var record = new BorrowingRecord { Id = 1, BookId = bookId, PatronId = patronId, BorrowDate = DateTime.Now };
            _unitOfWorkMock.Setup(u => u.BorrowingRepository.BorrowBook(bookId, patronId)).ReturnsAsync(record);

            // Act
            var result = await _controller.BorrowBook(bookId, patronId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRecord = Assert.IsType<BorrowingRecord>(okResult.Value);
            Assert.Equal(bookId, returnedRecord.BookId);
        }

        [Fact]
        public async Task ReturnBook_ReturnsOk_WhenReturningIsSuccessful()
        {
            // Arrange
            int bookId = 1, patronId = 1;
            var record = new BorrowingRecord { Id = 1, BookId = bookId, PatronId = patronId, BorrowDate = DateTime.Now, ReturnDate = DateTime.Now };
            _unitOfWorkMock.Setup(u => u.BorrowingRepository.ReturnBook(bookId, patronId)).ReturnsAsync(record);

            // Act
            var result = await _controller.ReturnBook(bookId, patronId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRecord = Assert.IsType<BorrowingRecord>(okResult.Value);
            Assert.NotNull(returnedRecord.ReturnDate);
        }
    }
}
