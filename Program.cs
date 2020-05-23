using System;
using System.Threading.Tasks;
using System.Text;
using Azure.Storage.Blobs;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;

namespace ProcessEventHub
{

    class Program
    {

        private const string ehubNamespaceConnectionString = "Endpoint=sb://srvlss-workshop-ehns.servicebus.windows.net/;SharedAccessKeyName=srvls-workshop-ehbrl;SharedAccessKey=JgSFCRluQgddAQZ5UldJaE4gMjaI8HHdZtUt6AtusYo=;EntityPath=srvlss-workshop-ehb";
        //private const string eventHubName = "reljev";
        //private const string blobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=reljblob;AccountKey=Z5PJbVAaJ8rVMMUjiRvU+rDl+Q2P+TDOMrfRW9uaIQNbUO8jLGjiNdSIUjmEfWVjWVIYXqNbQ2pK2+AzLuNQnA==;EndpointSuffix=core.windows.net";
        //private const string blobContainerName = "azure-eh-checkpoint";


        static async Task Main(string[] args)
        {

            
            var processor = new EventHubConsumerClient("srvlss-workshop-enbcg", ehubNamespaceConnectionString);
            var partitionEvents = processor.ReadEventsAsync(true);
            await foreach (var pe in partitionEvents)
            {

                foreach (var prop in pe.Data.Properties)
                    Console.WriteLine($"Key:{prop.Key} - Value:{prop.Value}");                

                Console.WriteLine($"Recevied event: {Encoding.UTF8.GetString(pe.Data.Body.ToArray())}");
                Console.WriteLine($"PartitionKey: {pe.Data.PartitionKey}");

            }
        }
    }
}
