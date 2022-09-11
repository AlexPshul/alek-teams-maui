using System.Collections.ObjectModel;
using System.Windows.Input;
using AlekTeams.Models;
using AlekTeams.Services;

namespace AlekTeams.ViewModels
{
    public class ChatPageViewModel : BaseViewModel
    {
        private CancellationTokenSource _typerIndicatorCancellationTokenSource;

        private string _messageToSendToSend;
        public string MessageToSend
        {
            get => _messageToSendToSend;
            set => Set(ref _messageToSendToSend, value);
        }

        private string _typerName;
        public string TyperName
        {
            get => _typerName;
            set => Set(ref _typerName, value);
        }

        public ObservableCollection<Message> Messages { get; }

        public ICommand SendMessageCommand { get; }
        public ICommand NotifyTypingCommand { get; }

        public ChatPageViewModel()
        {
            Messages = new ObservableCollection<Message>();

            SendMessageCommand = new Command(async () =>
            {
                var newMessage = new Message { Content = MessageToSend, From = MessagesService.CurrentUser };
                Messages.Add(newMessage);
                MessageToSend = string.Empty;
                
                await MessagesService.SendMessage(newMessage);
            });

            NotifyTypingCommand = new Command(async () =>
            {
                if (!string.IsNullOrEmpty(MessageToSend))
                    await MessagesService.SendTypingIndicator();
            });

            MessagesService.NewMessage += newMessage =>
            {
                Messages.Add(newMessage);
                if (newMessage.From == TyperName)
                    TyperName = null;
            };
            MessagesService.Typing += async name =>
            {
                TyperName = name;
                if (_typerIndicatorCancellationTokenSource != null)
                {
                    _typerIndicatorCancellationTokenSource.Cancel();
                }

                _typerIndicatorCancellationTokenSource = new CancellationTokenSource();
                try
                {
                    var typerIndicatorClearTask = Task.Delay(5000, _typerIndicatorCancellationTokenSource.Token);
                    await typerIndicatorClearTask;
                    if (!typerIndicatorClearTask.IsCanceled)
                    {
                        TyperName = null;
                    }
                }
                catch (TaskCanceledException)
                {
                }
            };
        }
    }
}
