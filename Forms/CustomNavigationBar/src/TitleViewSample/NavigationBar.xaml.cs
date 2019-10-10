using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TitleViewSample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public  partial class NavigationBar : StackLayout
    {
        public NavigationBar()
        {
            InitializeComponent();
            this.BindingContext = this;
         
            
        }

        public static readonly BindableProperty ShowBackButtonProperty =
        BindableProperty.Create(
            propertyName:   nameof(ShowBackButton), 
            returnType: typeof(bool),
            declaringType: typeof(NavigationBar),
            defaultValue: true);

        public bool ShowBackButton
        {
            get { return (bool)GetValue(ShowBackButtonProperty); }
            set { SetValue(ShowBackButtonProperty, value); }
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (Application.Current.MainPage.Navigation.NavigationStack.Count > 1)
            {
                await Navigation.PopAsync();
            }
            else
            {
                await Navigation.PopAsync();
            }
        }
    }
}