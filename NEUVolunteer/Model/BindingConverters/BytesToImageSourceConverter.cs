using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace NEUVolunteer.Model.BindingConverters
{
    /// <summary>
    /// 比特数组到图片源转换器。
    /// </summary>
    class BytesToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture) =>
            !(value is byte[] bytes)
                ? null
                : ImageSource.FromStream(() => new MemoryStream(bytes));

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}