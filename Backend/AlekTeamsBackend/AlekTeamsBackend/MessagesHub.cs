using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace AlekTeamsBackend
{
    public static class MessagesHub
    {
        private const string HubName = "messages";

        [FunctionName(nameof(Negotiate))]
        public static SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [SignalRConnectionInfo(HubName = HubName)] SignalRConnectionInfo connectionInfo) => connectionInfo;

        [FunctionName(nameof(SendTyping))]
        public static async Task SendTyping(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequest req,
            [SignalR(HubName = HubName)] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            var typingModel = await req.ReadFromJsonAsync<TypingModel>();

            await signalRMessages.AddAsync(new SignalRMessage { Target = "typing", Arguments = new object[] { typingModel } });
        }

        [FunctionName(nameof(SendMessage))]
        public static async Task SendMessage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequest req,
            [SignalR(HubName = HubName)] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            var messageModel = await req.ReadFromJsonAsync<MessageModel>();

            await signalRMessages.AddAsync(new SignalRMessage { Target = "messages", Arguments = new object[] { messageModel } });
        }
    }
}
