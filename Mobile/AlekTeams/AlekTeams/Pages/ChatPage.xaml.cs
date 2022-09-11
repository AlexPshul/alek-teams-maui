namespace AlekTeams.Pages;

public partial class ChatPage : ContentPage
{
	public ChatPage()
	{
		InitializeComponent();
        SendButton.Clicked += (_, _) => MessagesView.ScrollToAsync(0, MessagesView.ContentSize.Height, true);
    }
}