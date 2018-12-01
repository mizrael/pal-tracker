using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PalTracker
{
    public class MySqlTimeEntryRepository : ITimeEntryRepository
    {
        private readonly TimeEntryContext _dbContext;

        public MySqlTimeEntryRepository(TimeEntryContext dbContext)
        {
            _dbContext = dbContext;

            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public TimeEntry Create(TimeEntry timeEntry)
        {
            var entity = timeEntry.ToRecord();
            _dbContext.TimeEntryRecords.Add(entity);
            _dbContext.SaveChanges();
            timeEntry.Id = entity.Id;
            return timeEntry;
        }

        public TimeEntry Find(long id)
        {
            var entity = _dbContext.TimeEntryRecords.FirstOrDefault(e => e.Id == id);
            return entity?.ToEntity() ?? default(TimeEntry);
        }

        public bool Contains(long id)
        {
            return _dbContext.TimeEntryRecords.Any(e => e.Id == id);
        }

        public IEnumerable<TimeEntry> List()
        {
            return _dbContext.TimeEntryRecords.Select(e => e.ToEntity()).ToArray();
        }

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            var entity = _dbContext.TimeEntryRecords.FirstOrDefault(e => e.Id == id);
            if (null == entity) 
                return timeEntry;

            timeEntry.Id = id;

            _dbContext.TimeEntryRecords.Update(timeEntry.ToRecord());
            _dbContext.SaveChanges();

            return timeEntry;
        }

        public void Delete(long id)
        {
            var entity = _dbContext.TimeEntryRecords.FirstOrDefault(e => e.Id == id);
            if (null == entity)
                return;
            _dbContext.TimeEntryRecords.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
