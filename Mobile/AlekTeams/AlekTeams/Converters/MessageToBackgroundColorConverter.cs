using System.Globalization;
using AlekTeams.Models;
using AlekTeams.Services;

namespace AlekTeams.Converters
{
    public class MessageToBackgroundColorConverter : IValueConverter
    {
        public Color CurrentUserColor { get; set; } = Colors.DarkSlateGray;
        public Color OtherUserColor { get; set; } = Colors.Purple;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string sender = value as string;
            if (sender == null) return CurrentUserColor;

            return sender == MessagesService.CurrentUser ? CurrentUserColor : OtherUserColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
