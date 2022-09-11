using System.Globalization;
using AlekTeams.Models;

namespace AlekTeams.Converters
{
    public class TextToCapitalsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = value as string;
            if (text == null) return string.Empty;

            return string.Join(null, text.Split(' ').Select(part => part.ToUpper()).Select(part => part[0]));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
