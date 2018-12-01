using Microsoft.AspNetCore.Mvc;
using System;

namespace PalTracker
{
    [Route("/env")]
    public class EnvController : ControllerBase
    {
        private readonly CloudFoundryInfo _info;

        public EnvController(CloudFoundryInfo info){
            _info = info ?? throw new ArgumentNullException(nameof(info));
        }

        [HttpGet]
        public CloudFoundryInfo Get() => _info;
    }
}