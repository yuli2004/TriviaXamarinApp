using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Views;

namespace TriviaXamarinApp.ViewModels
{
    class LoginViewModel : ModelViewBase
    {
        private string email, password;

        public string Email
        {
            get { return this.email; }
            set { if (this.email != value) { this.email = value; OnPropertyChange(nameof(Email)); } }
        }
        public string Password
        {
            get { return this.password; }
            set { if (this.password != value) { this.password = value; OnPropertyChange(nameof(Password)); } }
        }

        public ICommand LoginCommand { get; set; }
        public Page NextPage { get; set; }

        public LoginViewModel()
        {
            this.email = "";
            this.password = "";

            LoginCommand = new Command(this.Login);
        }

        public async void Login()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            User u = await proxy.LoginAsync(Email, Password);

            if (u != null)
            {
                App a = (App)App.Current;
                a.CurrentUser = u;
                AmericanQuestion amricanQuestion = await proxy.GetRandomQuestion();
                Page p = new Play(amricanQuestion, 0);
                PlayViewModel game = (PlayViewModel)p.BindingContext;
                game.Score = 0;
                p.Title = "Game";
                Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
                await App.Current.MainPage.Navigation.PushAsync(p);
            }
        }
    }
}
