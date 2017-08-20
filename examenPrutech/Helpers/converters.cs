using System;
using Xamarin.Forms;
using System.Globalization;
using System.Linq;

namespace GMX.Helpers
{
	public class AlternatingHighlightColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Color rowcolor = Color.Transparent;
			if (value == null || parameter == null) return Color.White;
			var index = ((ListView)parameter).ItemsSource.Cast<object>().ToList().IndexOf(value);
			if (index % 2 == 0)
			{
                rowcolor = Color.FromHex("c8dade");
			}
			return rowcolor;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
