﻿using Steeltoe.Common.HealthChecks;
using System;
using System.Linq;

namespace PalTracker
{
    public class TimeEntryHealthContributor : IHealthContributor
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        public const int MaxTimeEntries = 5;
        
        public TimeEntryHealthContributor(ITimeEntryRepository timeEntryRepository)
        {
            _timeEntryRepository = timeEntryRepository ?? throw new ArgumentNullException(nameof(timeEntryRepository));
        }

        public HealthCheckResult Health()
        {
            var count = _timeEntryRepository.List().Count();
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