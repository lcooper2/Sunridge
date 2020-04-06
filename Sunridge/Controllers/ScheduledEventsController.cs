using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Sunridge.Utility;
using Microsoft.AspNetCore.Identity;
using Sunridge.Models;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduledEventsController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public ScheduledEventsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/ScheduledEvents
        [HttpGet]
        public IEnumerable<ScheduledEvent> GetScheduledEvents()
        {
            return _unitOfWork.ScheduledEvent.GetAll();
        }

        // GET: api/ScheduledEvents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetScheduledEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scheduledEvent = _unitOfWork.ScheduledEvent.Get(id);

            if (scheduledEvent == null)
            {
                return NotFound();
            }

            return Ok(scheduledEvent);
        }

        // PUT: api/ScheduledEvents/5
        [HttpPut("{id}")]
        public IActionResult PutScheduledEvent([FromRoute] int id, [FromBody] ScheduledEvent scheduledEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scheduledEvent.Id)
            {
                return BadRequest();
            }

            _unitOfWork.ScheduledEvent.Update(scheduledEvent);
            _unitOfWork.Save();

            return NoContent();
        }

        // POST: api/ScheduledEvents
        [HttpPost]
        public IActionResult PostScheduledEvent([FromBody] ScheduledEvent scheduledEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.ScheduledEvent.Add(scheduledEvent);
            _unitOfWork.Save();

            return CreatedAtAction("GetScheduledEvent", new { id = scheduledEvent.Id }, scheduledEvent);
        }

        // DELETE: api/ScheduledEvents/5
        [HttpDelete("{id}")]
        public IActionResult DeleteScheduledEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scheduledEvent = _unitOfWork.ScheduledEvent.Get(id);
            if (scheduledEvent == null)
            {
                return NotFound();
            }

            _unitOfWork.ScheduledEvent.Remove(scheduledEvent.Id);
            _unitOfWork.Save();

            return Ok(scheduledEvent);
        }

        private bool ScheduledEventExists(int id)
        {
            return _unitOfWork.ScheduledEvent.GetAll().Any(c => c.Id == id);
        }
    }
}
