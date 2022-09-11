using System.Globalization;
using AlekTeams.Models;
using AlekTeams.Services;

namespace AlekTeams.Converters
{
    public class MessageToGridColumnDefinitionsConverter : IValueConverter
    {
        public ColumnDefinitionCollection CurrentUserColumns { get; set; }
        public ColumnDefinitionCollection OtherUserColumns { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string sender = value as string;
            if (sender == null) return CurrentUserColumns;

            return sender == MessagesService.CurrentUser ? CurrentUserColumns : OtherUserColumns;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
