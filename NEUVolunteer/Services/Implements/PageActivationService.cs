using System;
using System.Collections.Generic;
using System.Text;
using NEUVolunteer.Services;
using Xamarin.Forms;

namespace NEUVolunteer.Services.Implements
{
    public class PageActivationService : IPageActivationService
    {

        private Dictionary<string, ContentPage> pageCache = new Dictionary<string, ContentPage>();

        public ContentPage Activate(string pageKey) =>
            pageCache.ContainsKey(pageKey)
                ? pageCache[pageKey]
                : pageCache[pageKey] =
                    (ContentPage)Activator.CreateInstance(
                        NavigationServiceConstants.PageKeyTypeDictionary[
                            pageKey]);
    }
}
