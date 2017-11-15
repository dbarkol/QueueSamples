using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerUpdates.Models;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerUpdates.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        private static readonly string _queueName = "testqueue";
        private static readonly string _storageConnectionString = "";


        // POST api/values
        [HttpPost]
        public async Task PostAsync([FromBody]UpdateRequest request)
        {
            var storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
            var queueClient = storageAccount.CreateCloudQueueClient();

            var messageQueue = queueClient.GetQueueReference(_queueName);

            var message = new CloudQueueMessage(JsonConvert.SerializeObject(request));
            await messageQueue.AddMessageAsync(message);
        }

    }
}
