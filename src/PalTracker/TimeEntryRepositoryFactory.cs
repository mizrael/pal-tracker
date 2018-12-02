using System;
using Microsoft.Extensions.DependencyInjection;

namespace PalTracker
{
    public class TimeEntryRepositoryFactory : ITimeEntryRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public TimeEntryRepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public ITimeEntryRepository Create()
        {
            var repository = _serviceProvider.GetService<ITimeEntryRepository>();
            return repository;
        }
    }
}