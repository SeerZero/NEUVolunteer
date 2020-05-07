using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NEUVolunteer.Service
{
    public class NavigationContext
    {
        public static readonly BindableProperty NavigationParameterProperty =
            BindableProperty.CreateAttached("NavigationParameter",
                typeof(object), typeof(NavigationContext), null,
                BindingMode.OneWayToSource);

        public static void
            SetParameter(BindableObject page, object parameter) =>
            page.SetValue(NavigationParameterProperty, parameter);
    }
}
