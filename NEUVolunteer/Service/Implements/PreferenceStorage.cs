using System;
using System.Collections.Generic;
using System.Text;
using NEUVolunteer.Service;
using Xamarin.Essentials;

namespace NEUVolunteer.Service.Implements
{
    public class PreferenceStorage : IPreferenceStorage
    {
        public int Get(string key, int defaultValue) =>
            Preferences.Get(key, defaultValue);

        public void Set(string key, int value) =>
            Preferences.Set(key, value);
    }
}