using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Test
{
    public class ProductControllerTest
    {

        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IProductQueryService> _productQueryServiceMock;
        private readonly Mock<IProductCommandService> _productCommandServiceMock;
        private ProductController _controller;

        public ProductControllerTest()
        {
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                null, null, null, null, null, null, null, null);
            _mapperMock = new Mock<IMapper>();
            _productQueryServiceMock = new Mock<IProductQueryService>();
            _productCommandServiceMock = new Mock<IProductCommandService>();
            _controller = new ProductController(_productCommandServiceMock.Object, _productQueryServiceMock.Object, _mapperMock.Object, _userManagerMock.Object);
        }


        [Fact]
        public async Task GetProducts_ShouldReturnOK()
        {
            //Arrange
            var productList = A.Fake<IEnumerable<Product>>();

            _productQueryServiceMock.Setup(p => p.GetAllProductsAsync(1, 10, null))
                .ReturnsAsync(productList);

            //Act
            var result = await _controller.GetProducts();


            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType(typeof(OkObjectResult));

        }

        [Theory]
        [InlineData(null)]
        public async Task GetProductById_WithtNoneProduct_ShouldReturnBadRequest(string id)
        {
            //Arrange

            //Act
            var result = await _controller.GetProductById(id);

            //Assert
            result.ShouldBeOfType(typeof(NotFoundResult));

        }


        [Theory]
        [InlineData("123")]
        public async Task GetProductById_WithTestData_ShouldReturnOk(string id)
        {
            //Arrange
            _productQueryServiceMock.Setup(p => p.GetProductByIdAsync("123"))
                .ReturnsAsync(new Product
                {
                    Id = "123",
                    IsAvailable = true,
                    ManufactorEmail = "email",
                    ManufactorPhone = "123456",
                    Name = "name1",
                    ProduceDate = DateTime.Now,
                    UserId = "1"
                });

            //Act
            var result = await _controller.GetProductById(id);

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType(typeof(OkObjectResult));

        }



    }
}
