using Homebank.Core.Entities;
using Homebank.Core.Helpers;
using Homebank.Core.Interfaces.Repositories;
using Homebank.Core.Repositories;
using Homebank.Web.Controllers;
using Homebank.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Homebank.Web.Tests.Controllers
{
    public class SecurityControllerTests
    {
        [Fact]
        public async Task Login_ReturnsAViewResult_WithNoErrors()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository => repository.Get(It.Is<string>(s => s == "Max"))).Returns(new User { Id = 1, Name = "Max" });

            var controller = new SecurityController(userRepositoryMock.Object);
            var model = new LoginModel
            {
                Name = "Max",
                Password = "Pa$$w0rd!"
            };

            //Act
            var result = await controller.Login(model, string.Empty);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Home", redirectResult.ControllerName);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Login_ReturnsAViewResult_WithValidationErrors()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();

            var controller = new SecurityController(userRepositoryMock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            var model = new LoginModel();

            //Act
            var result = await controller.Login(model, string.Empty);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var modelResult = Assert.IsAssignableFrom<LoginModel>(viewResult.Model);
        }
    }
}
