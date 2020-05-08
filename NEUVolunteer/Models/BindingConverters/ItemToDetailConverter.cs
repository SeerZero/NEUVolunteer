using System;
using System.Globalization;
using Xamarin.Forms;

namespace NEUVolunteer.Models.BindingConverters
{
    public class ItemToDetailConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (value as ItemTappedEventArgs)?.Item as ApplyDetail;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
