using System.Net.Http.Json;
using System.Text.Json;
using AlekTeams.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace AlekTeams.Services
{
    public static class MessagesService
    {
        private static readonly HttpClient HttpClient = new();
        private static HubConnection _signalRConnection;

        public static string CurrentUser { get; set; }

        public static event Action<string> Typing;
        public static event Action<Message> NewMessage;

        public static async Task Connect()
        {
            if (_signalRConnection != null) return;

            HttpClient.BaseAddress = new Uri("");
            var response = await HttpClient.GetAsync("/api/Negotiate");
            var connectionInfo = await response.Content.ReadFromJsonAsync<SignalRConnectionInfo>();

            _signalRConnection = new HubConnectionBuilder()
                .WithUrl(connectionInfo?.Url ?? "", options => options.AccessTokenProvider = () => Task.FromResult(connectionInfo?.AccessToken ?? ""))
                .Build();
            _signalRConnection.On<TypingModel>("typing", (typing) =>
            {
                if (typing.Name != CurrentUser)
                    Typing?.Invoke(typing.Name);
            });
            _signalRConnection.On<Message>("messages", message =>
            {
                if (message.From != CurrentUser)
                    NewMessage?.Invoke(message);
            });

            await _signalRConnection.StartAsync();
        }

        public static async Task SendTypingIndicator()
        {
            await HttpClient.PostAsJsonAsync("/api/SendTyping", new TypingModel { Name = CurrentUser });
        }

        public static async Task SendMessage(Message message)
        {
            var result = await HttpClient.PostAsJsonAsync("/api/SendMessage", message, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        }
    }
}
