using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Data.Edm.Expressions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace QueueLookpConsole
{
    class Program
    {
        #region Data Members

        private static readonly string _queueName = "testqueue";
        private static readonly string _storageConnectionString = "";

        #endregion 

        static void Main(string[] args)
        {

            ReadFromQueue().Wait();

            Console.WriteLine("Press <enter> to exit.");
            Console.ReadLine();
            Console.WriteLine("Good bye!");
        }

        private static async Task ReadFromQueue()
        {
            var storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
            var queueClient = storageAccount.CreateCloudQueueClient();

            var messageQueue = queueClient.GetQueueReference(_queueName);

            while (true)
            {
                // Get the next message in the queue.
                //var retrievedMessage = await messageQueue.GetMessageAsync();
                var retrievedMessage = await messageQueue.GetMessageAsync();

                if (retrievedMessage != null)
                {

                    // Process the message in less than 30 seconds.
                    Console.WriteLine("Message received...");
                    // TODO: Implement

                    // Then delete the message.
                    await messageQueue.DeleteMessageAsync(retrievedMessage);
                }

                System.Threading.Thread.Sleep(1000);
            }
            



            return;
        }

    }
}
