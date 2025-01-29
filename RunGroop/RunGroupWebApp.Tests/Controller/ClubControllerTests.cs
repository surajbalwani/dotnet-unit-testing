using CloudinaryDotNet.Actions;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Controllers;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunGroupWebApp.Tests.Controller
{

    public class ClubControllerTests
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        private readonly ClubController _clubController;

        public ClubControllerTests()
        {
            //Dependencies
            _clubRepository = A.Fake<IClubRepository>();
            _photoService = A.Fake<IPhotoService>();

            //SUT
            _clubController = new ClubController(_clubRepository, _photoService);
        }

        [Theory]
        [InlineData(-1, 1, 6)]
        public void ClubController_Index_ReturnsSuccess(int category = -1, int page = 1, int pagesize = 6)
        {
            //Arrage - what do i need to bring in
            var clubs = A.Fake<IEnumerable<Club>>();
            A.CallTo(() => _clubRepository.GetSliceAsync((page - 1) * pagesize, pagesize)).Returns(clubs);

            //Act
            var result = _clubController.Index(category, page, pagesize);

            //Assert - object check actions
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ClubController_DetailClub_ReturnsSuccess()
        {
            //Arrange
            var id = 1;
            var club = A.Fake<Club>();
            A.CallTo(() => _clubRepository.GetByIdAsync(id)).Returns(club);

            //Act
            var result = _clubController.DetailClub(id, string.Empty);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
