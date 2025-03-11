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
    public class BooksControllerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BooksController _controller;

        public BooksControllerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _controller = new BooksController(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithPagination()
        {
            // Arrange
            var booksDto = new List<GetBookDto>
            {
                new GetBookDto { Id = 1, Title = "Book1" },
                new GetBookDto { Id = 2, Title = "Book2" }
            };

            // إنشاء كائن Params افتراضي
            Params param = new Params { PageNumber = 1, PageSize = 10, TotalItems = 2, Search = "", Sort = "" };

            _unitOfWorkMock.Setup(u => u.BookRepository.GetAllAsync(It.IsAny<Params>()))
                           .ReturnsAsync(booksDto);

            // Act
            var result = await _controller.Get(param);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var pagination = Assert.IsType<Pagination<GetBookDto>>(okResult.Value);
            Assert.Equal(2, pagination.Count);
        }

        [Fact]
        public async Task GetBook_ReturnsOkResult_WhenBookExists()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Book1", Author = "Author", PublicationYear = DateTime.Now, ISBN = "123" };
            _unitOfWorkMock.Setup(u => u.BookRepository.GetByIdAsync(1)).ReturnsAsync(book);
            _mapperMock.Setup(m => m.Map<GetBookDto>(It.IsAny<Book>()))
                       .Returns(new GetBookDto { Id = 1, Title = "Book1" });

            // Act
            var result = await _controller.GetBook(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var bookDto = Assert.IsType<GetBookDto>(okResult.Value);
            Assert.Equal(1, bookDto.Id);
        }

        [Fact]
        public async Task Post_ReturnsOk_WhenModelIsValid()
        {
            // Arrange
            var bookDto = new BookDto { Title = "New Book", Author = "Author", PublicationYear = DateTime.Now, ISBN = "123" };
            // نفترض أن عملية الإضافة تعيد true
            _unitOfWorkMock.Setup(u => u.BookRepository.AddAsync(It.IsAny<BookDto>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Post(bookDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            var updateDto = new UpdateBookDto { Id = 1, Title = "Updated Book", Author = "Author", PublicationYear = DateTime.Now, ISBN = "123" };
            var existingBook = new Book { Id = 1, Title = "Old Book", Author = "Author", PublicationYear = DateTime.Now, ISBN = "123" };

            _unitOfWorkMock.Setup(u => u.BookRepository.GetByIdAsync(1)).ReturnsAsync(existingBook);
            _unitOfWorkMock.Setup(u => u.BookRepository.UpdateAsync(1, existingBook)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(1, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            // نقارن بأن القيمة المرجعة هي نفسها قيمة الـ DTO المُحدّث
            Assert.Equal(updateDto, okResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenBookIsDeleted()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Book1", Author = "Author", PublicationYear = DateTime.Now, ISBN = "123" };
            _unitOfWorkMock.Setup(u => u.BookRepository.GetByIdAsync(1)).ReturnsAsync(book);
            _unitOfWorkMock.Setup(u => u.BookRepository.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Deleted Successfully", okResult.Value.ToString());
        }
    }
}
