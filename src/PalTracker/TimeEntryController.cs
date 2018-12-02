using Microsoft.AspNetCore.Mvc;
using System;

namespace PalTracker
{
    [Route("/time-entries")]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly IOperationCounter<TimeEntry> _operationCounter;

        public TimeEntryController(ITimeEntryRepository timeEntryRepository, IOperationCounter<TimeEntry> operationCounter)
        {
            _timeEntryRepository = timeEntryRepository ?? throw new ArgumentNullException(nameof(timeEntryRepository));
            _operationCounter = operationCounter  ?? throw new ArgumentNullException(nameof(operationCounter));
        }

        [HttpGet]
        public IActionResult List()
        {
            _operationCounter.Increment(TrackedOperation.List);

            var entries = _timeEntryRepository.List();
            
            return Ok(entries);
        }

        [HttpGet, Route("{id:long}", Name = "GetTimeEntry")]
        public IActionResult Read(long id)
        {
            _operationCounter.Increment(TrackedOperation.Read);

            if (!_timeEntryRepository.Contains(id))
                return NotFound();
            var entry = _timeEntryRepository.Find(id);
            return Ok(entry);
        }

        [HttpPost]
        public IActionResult Create([FromBody]TimeEntry entry)
        {
            _operationCounter.Increment(TrackedOperation.Create);

            if (entry.Id.HasValue && _timeEntryRepository.Contains(entry.Id.Value))
                return BadRequest();
            entry = _timeEntryRepository.Create(entry);
            return CreatedAtRoute("GetTimeEntry", new {id = entry.Id}, entry);
        }

        [HttpPut, Route("{id:long}")]
        public IActionResult Update(long id, [FromBody] TimeEntry entry)
        {
            _operationCounter.Increment(TrackedOperation.Update);

            if (!_timeEntryRepository.Contains(entry.Id.GetValueOrDefault(id)))
                return NotFound();

            entry = _timeEntryRepository.Update(id, entry);
            return Ok(entry);
        }

        [HttpDelete, Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            _operationCounter.Increment(TrackedOperation.Delete);

            if (!_timeEntryRepository.Contains(id))
                return NotFound();

            _timeEntryRepository.Delete(id);
            return NoContent();
        }
    }
}