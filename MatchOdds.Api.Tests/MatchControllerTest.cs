using MatchOdds.Api.Controllers;
using MatchOdds.Data.DataProviders;
using MatchOdds.Data.DataProviders.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MatchOdds.Api.Tests
{
    public class MatchControllerTest
    {
        MatchController _matchController;
        MatchDataProvider matchDataProvider;
        ILogger<MatchController> logger;

        List<Data.Db.Models.Match> moqMatches = new List<Data.Db.Models.Match>(){
        new Data.Db.Models.Match()
        {
            Description = "test",
            MatchDate = DateTime.Now,
            MatchTime = DateTime.Now.TimeOfDay,
            TeamA = "A",
            TeamB = "B",
            Sport = Data.Enums.SportType.Football
        }
        };

        public MatchControllerTest()
        {
            logger = Mock.Of<ILogger<MatchController>>();
            matchDataProvider = Mock.Of<MatchDataProvider>(m => m.GetAll() == moqMatches.AsEnumerable());
            _matchController = new MatchController(logger, matchDataProvider);
        }

        [Fact]
        public void GetAllTest()
        {
            //Arrange
            
            //Act
            var result = _matchController.GetAll();
            //Assert
            Assert.IsType<OkObjectResult>(result);

            var list = result as OkObjectResult;

            Assert.IsType<List<Match>>(list.Value);



            var listBooks = list.Value as List<Match>;

            Assert.Equal(5, listBooks.Count);
        }

    }
}
