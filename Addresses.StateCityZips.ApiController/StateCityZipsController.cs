// Copyright © Christopher Dorst. All rights reserved.
// Licensed under the GNU General Public License, Version 3.0. See the LICENSE document in the repository root for license information.

using Addresses.StateCityZips.DatabaseContext;
using DevOps.Code.DataAccess.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Addresses.StateCityZips.ApiController
{
    /// <summary>ASP.NET Core web API controller for StateCityZip entities</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StateCityZipsController : ControllerBase
    {
        /// <summary>Represents the application events logger</summary>
        private readonly ILogger<StateCityZipsController> _logger;

        /// <summary>Represents repository of StateCityZip entity data</summary>
        private readonly IRepository<StateCityZipDbContext, StateCityZip, int> _repository;

        /// <summary>Constructs an API controller for StateCityZip entities using the given repository service</summary>
        public StateCityZipsController(ILogger<StateCityZipsController> logger, IRepository<StateCityZipDbContext, StateCityZip, int> repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>Handles HTTP GET requests to access StateCityZip resources at the given ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<StateCityZip>> Get(int id)
        {
            if (id < 1) return NotFound();
            var resource = await _repository.FindAsync(id);
            if (resource == null) return NotFound();
            return resource;
        }

        /// <summary>Handles HTTP HEAD requests to access StateCityZip resources at the given ID</summary>
        [HttpHead("{id}")]
        public ActionResult<StateCityZip> Head(int id) => null;

        /// <summary>Handles HTTP POST requests to save StateCityZip resources</summary>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<StateCityZip>> Post(StateCityZip resource)
        {
            var saved = await _repository.AddAsync(resource);
            return CreatedAtAction(nameof(Get), new { id = saved.GetKey() }, saved);
        }
    }
}
