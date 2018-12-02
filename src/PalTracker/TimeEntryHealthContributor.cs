using Steeltoe.Common.HealthChecks;
using System;
using System.Linq;

namespace PalTracker
{
    public class TimeEntryHealthContributor : IHealthContributor
    {
        private readonly ITimeEntryRepositoryFactory _repositoryFactory;
        public const int MaxTimeEntries = 5;
        
        public TimeEntryHealthContributor(ITimeEntryRepositoryFactory timeEntryRepository)
        {
            _repositoryFactory = timeEntryRepository ?? throw new ArgumentNullException(nameof(timeEntryRepository));
        }

        public HealthCheckResult Health()
        {
            var repo = _repositoryFactory.Create();

            var count = repo.List().Count();
            var status = count < MaxTimeEntries ? HealthStatus.UP : HealthStatus.DOWN;

            var health = new HealthCheckResult {Status = status};

            health.Details.Add("threshold", MaxTimeEntries);
            health.Details.Add("count", count);
            health.Details.Add("status", status.ToString());

            return health;
        }

        public string Id { get; } = "timeEntry";
    }
}
