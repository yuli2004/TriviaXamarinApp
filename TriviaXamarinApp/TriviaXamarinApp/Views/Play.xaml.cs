using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaXamarinApp.ViewModels;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using System;

namespace TriviaXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Play : ContentPage
    {
        public Play() { }
        public Play(AmericanQuestion a, int score)
        {
            PlayViewModel context = new PlayViewModel(a, score);
            //Register to the event so the view model will be able to navigate
            context.NavigateToPageEvent += NavigateToAsync;
            this.BindingContext = context;
            InitializeComponent();
        }

        public async void NavigateToAsync(Page p)
        {

            await Navigation.PushAsync(p);

        }

    }
}