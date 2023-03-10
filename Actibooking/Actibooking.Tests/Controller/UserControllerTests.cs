using Actibooking.Controllers;
using Actibooking.Data.Repository;
using Actibooking.Models;
using Actibooking.Services;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actibooking.Tests.Controller
{
    public class UserControllerTests
    {
        private IUnitOfWork _uow;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserControllerTests()
        {
            _uow = A.Fake<IUnitOfWork>();
            _userService = A.Fake<IUserService>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void UserController_GetUser_ReturnOk()
        {
            //Arange
            var user = new ActiBookingUser();
            var userList = new List<ActiBookingUser>() { user };
            A.CallTo(() => _uow.UserRepo.GetAsync()).Returns(Task.FromResult((IEnumerable<ActiBookingUser>)userList));
            var controller = new UserController(_mapper, _uow, _userService);

            //Act
            var result = controller.GetUser(user.Id);

            //Assert
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void UserController_AddChild_ReturnOk()
        {
            var addingChildDTO = A.Fake<AddingChildDTO>();
            var okResult = new OkObjectResult(addingChildDTO);
            A.CallTo(() => _userService.AddChild(addingChildDTO, _uow, _mapper)).Returns(okResult);
            var controller = new UserController(_mapper, _uow, _userService);

            var result = controller.AddChild(addingChildDTO);

            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void UserController_GetUserCourses()
        {
            var userId = "String";
            var listCourses = A.Fake<IEnumerable<Course>>();
            A.CallTo(() => _userService.GetUserCourses(userId, _uow)).Returns(listCourses);
            var controller = new UserController(_mapper, _uow, _userService);

            var result = controller.GetUserCourses(userId).Result;

            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
