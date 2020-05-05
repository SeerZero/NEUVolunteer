using System;
using System.Collections.Generic;
using System.Text;
using NEUVolunteer.Services.interfaces;
using Xamarin.Essentials;

namespace NEUVolunteer.Services.implements
{
    public class PreferenceStorage : IPreferenceStorage
    {
        public int Get(string key, int defaultValue) =>
            Preferences.Get(key, defaultValue);

        public void Set(string key, int value) => 
            Preferences.Set(key, value);
    }
}
