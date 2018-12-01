using Microsoft.AspNetCore.Mvc;
using System;

namespace PalTracker
{
    [Route("/")]
    public class WelcomeController : ControllerBase
    {
        private readonly WelcomeMessage _message;

        public WelcomeController(WelcomeMessage message){
            _message = message ?? throw new ArgumentNullException(nameof(message));
        }

        [HttpGet]
        public string SayHello() => _message.Message;
    }
}