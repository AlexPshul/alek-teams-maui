using System.Windows.Input;
using AlekTeams.Pages;
using AlekTeams.Services;

namespace AlekTeams.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string _name = "Alex Pshul";
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private bool _connecting;
        public bool Connecting
        {
            get => _connecting;
            set => Set(ref _connecting, value);
        }


        public ICommand GoToChatCommand { get; }

        public MainPageViewModel()
        {
            GoToChatCommand = new Command(async () =>
            {
                if (string.IsNullOrWhiteSpace(Name)) return;

                MessagesService.CurrentUser = Name;
                Connecting = true;
                await MessagesService.Connect();
                Connecting = false;

                Application.Current?.MainPage?.Navigation.PushAsync(new ChatPage());
            });
        }
    }
}
