using Api.Extensions;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Test.Extensions
{
    public class TestController : ControllerBase { }
    public class TestEntity { }

    public class ControllerBaseExtensionsTests
    {
        private readonly ControllerBase _controller;

        public ControllerBaseExtensionsTests()
        {
            _controller = new TestController();
        }

        [Fact]
        public void Return_204NoContent()
        {
            var serviceResponse = new ServiceResponse();

            var result = _controller.GetResultFromServiceResponse(serviceResponse);

            Assert.True(result.GetType() == typeof(NoContentResult), "Result should be 204 No Content");
        }

        [Fact]
        public void Return_200Ok()
        {
            var serviceResponse = new ServiceResponse<TestEntity>();
            serviceResponse.Data = new TestEntity();

            var result = _controller.GetResultFromServiceResponse(serviceResponse);

            Assert.True(result.GetType() == typeof(OkObjectResult), "Result should be 200 OK");
        }

        [Fact]
        public void Return_201Created()
        {
            var serviceResponse = new ServiceResponse<TestEntity>();
            serviceResponse.Data = new TestEntity();

            var result = _controller.GetResultFromServiceResponse(serviceResponse, "SomeRandomUri");

            Assert.True(result.GetType() == typeof(CreatedResult), "Result should be 201 Created");
        }
    }
}
