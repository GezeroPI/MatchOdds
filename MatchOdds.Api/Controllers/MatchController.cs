using MatchOdds.Data.DataProviders.Contracts;
using MatchOdds.Data.Db.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchOdds.Api.Controllers
{
    /// <summary>
    /// Match controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class MatchController : ControllerBase
    {
        #region Properties   
        private readonly ILogger<MatchController> _logger;
        private readonly IMatchDataProvider _matchProvider;
        #endregion Properties

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="matchProvider">The match provider.</param>
        public MatchController(ILogger<MatchController> logger, IMatchDataProvider matchProvider)
        {
            _logger = logger;
            _matchProvider = matchProvider;
        }
        #endregion Constructor

        #region Actions

        #region GetMethods
        /// <summary>
        /// Gets all matches.
        /// </summary>
        /// <response code="200">Returns all available matches from database</response>
        /// <response code="500">An internal server error has occured</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogInformation("GetAll action called");
                return Ok(_matchProvider.GetAll().ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured: {ex.StackTrace}");
                return new ContentResult { Content = ex.Message, StatusCode = StatusCodes.Status500InternalServerError };
            }

        }

        /// <summary>
        /// Gets the match by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <response code="200">Returns the specified match from database</response>
        /// <response code="500">An internal server error has occured</response>
        /// <exception cref="System.ArgumentException">Id must not be empty or equal to zero (0)</exception>
        /// <exception cref="System.ArgumentNullException">Match with id {id} not found!</exception>
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                _logger.LogInformation($"GetById action called with id: {id}");
                if (id == default)
                    throw new ArgumentException("Id must not be empty or equal to zero (0)");
                var match = _matchProvider.GetByID(id);
                if (match == null)
                    throw new ArgumentNullException($"Match with id {id} not found!");
                return Ok(match);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured: {ex.StackTrace}");
                return new ContentResult { Content = ex.Message, StatusCode = StatusCodes.Status500InternalServerError };
            }

        }
        #endregion GetMethods

        #region PostMethods
        /// <summary>
        /// Adds the specified match.
        /// </summary>
        /// <remarks>
        /// Sample request
        ///
        ///     {
        ///         "Description":"Test",
        ///         "MatchDate":"2021/11/09",
        ///         "MatchTime":"11:00",
        ///         "TeamA":"OSFP",
        ///         "TeamB":"PAO",
        ///         "Sport":"1",
        ///         "MatchOdds":[
        ///             {
        ///                 "Specifier":"X",
        ///                 "Odd":"1.4"
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <param name="match">The match.</param>
        /// <response code="200">Returns the added match</response>
        /// <response code="500">An internal server error has occured</response>
        [HttpPost]
        public ActionResult Add([FromBody] Match match)
        {
            try
            {
                _logger.LogInformation("Add action called");
                _matchProvider.Insert(ref match);
                return Ok(match);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured: {ex.StackTrace}");
                return new ContentResult { Content = ex.Message, StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        #endregion PostMethods

        #region PutMethods

        /// <summary>
        /// Updates the specified match.
        /// </summary>
        /// Sample request
        ///
        ///     {
        ///         "Description":"Test",
        ///         "MatchDate":"2021/11/09",
        ///         "MatchTime":"11:00",
        ///         "TeamA":"OSFP",
        ///         "TeamB":"PAO",
        ///         "Sport":"1",
        ///         "MatchOdds":[
        ///             {
        ///                 "Specifier":"X",
        ///                 "Odd":"1.4"
        ///             }
        ///         ]
        ///     }
        /// <param name="match">The match.</param>
        /// <response code="200">Returns the updated match</response>
        /// <response code="500">An internal server error has occured</response>
        [HttpPut]
        public ActionResult Update([FromBody] Match match)
        {
            try
            {
                _logger.LogInformation("Update action called");
                return Ok(_matchProvider.Update(match));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured: {ex.StackTrace}");
                return new ContentResult { Content = ex.Message, StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        #endregion PutMethods

        #region DeleteMethods

        /// <summary>
        /// Deletes the match by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <response code="200">Returns success message</response>
        /// <response code="500">An internal server error has occured</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            try
            {
                _logger.LogInformation("Delete action called");
                _matchProvider.Delete(id);
                return Ok("Match deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured: {ex.StackTrace}");
                return new ContentResult { Content = ex.Message, StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        #endregion DeleteMethods

        #endregion Actions
    }
}
