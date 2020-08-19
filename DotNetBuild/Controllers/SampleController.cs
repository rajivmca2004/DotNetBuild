using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using DotNetBuild.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using DotNetBuild.Services.Interfaces;
using DotNetBuild.Domain.Models;
using DotNetBuild.Services.Interfaces;


namespace DotNetBuild.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ILogger<SampleController> _logger;
   
                 private readonly ISampleService _sampleService;        
                 private readonly ICacheService _cacheService;        
                 private readonly IMessageSubscriberService _messageSubscriberService;
            private readonly IMessagePublisherService _messagePublisherService;     

        public SampleController( ILogger<SampleController> logger
             ,ISampleService postgresService             
                ,ICacheService cacheService           
                  ,IMessageSubscriberService messageSubscriberService
                                , IMessagePublisherService messagePublisherService            
             )
            {
               _logger = logger;
                _sampleService = postgresService;              
                _cacheService = cacheService;      
                          _messageSubscriberService = messageSubscriberService;
               _messagePublisherService = messagePublisherService;   
            }

            [Route("GetAllSamples")]
            [HttpGet]
            public IEnumerable<Sample> GetAllSamples()
            {
                return _sampleService.Get();
            }

            [Route("GetSample/Id")]
            [HttpGet]
            public Sample GetSample(int id)
            {
                return _sampleService.Get(id);
            }

            [Route("AddSample")]
            [HttpPost]
            public string AddSample([FromBody] Sample Data)
            {
                return _sampleService.Post(Data);
            }

            [Route("UpdateSample")]
            [HttpPut]
            public string UpdateSample([FromBody] Sample Data)
            {
                return _sampleService.Put(Data);
            }

             [Route("DeleteSample")]
            [HttpDelete]
            public string DeleteSample(int id)
            {
                return _sampleService.Delete(id);
            }


            [Route("GetCacheData")]
            [HttpGet]
            public async Task<string> GetCacheData(string Key)
            {
                return await _cacheService.Get(Key);
            }

            [Route("AddCacheData")]
            [HttpPost]
            public async Task<string> AddCacheData([FromBody] Dictionary<string, string> Data)
            {
                return await _cacheService.Post(Data);
            }

             [Route("DeleteCacheData")]
            [HttpDelete]
            public async Task<string> DeleteCacheData(string Key)
            {
                return await _cacheService.Delete(Key);
            }

        [Route("Subscribe")]   
        [HttpGet]
           public ActionResult<string> Subscribe()
            {
               return _messageSubscriberService.Subscribe();
            }

            [Route("Publish")] 
            [HttpPost]
            public void Publish([FromQuery] string publishMessage)
            {
                _messagePublisherService.Publish(publishMessage);
            }

              

        }
}
