using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TitleViewSample
{
    public class CustomPage : ContentPage
    {
        public CustomPage()
        {
            var navbar = new NavigationBar();

            try
            {
                if (Application.Current.MainPage.Navigation.NavigationStack.Count > 0)
                {
                    navbar.ShowBackButton = true;
                }
                else
                {
                    navbar.ShowBackButton = false;
                }
            }
            catch (Exception)
            {

                navbar.ShowBackButton = false;
            }
            
                            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetTitleView(this, navbar);
        }
    }
}