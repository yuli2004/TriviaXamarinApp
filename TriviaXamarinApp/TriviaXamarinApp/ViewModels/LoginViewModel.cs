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

        public string Email { get { return this.email; } set { if (this.email != value) { this.email = value; OnPropertyChange("Email"); } } }
        public string Password { get { return this.password; } set { if (this.password != value) { this.password = value; OnPropertyChange("Password"); } } }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            this.email = "";
            this.password = "";

            LoginCommand = new Command(Login);
        }

        public async void Login()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            User u = await proxy.LoginAsync(Email, Password);

            if (u != null)
            {
                App a = (App)App.Current;
                a.CurrentUser = u;
                Page p = new QuestionManager();
                p.Title = "QuestionManager";
                p.BindingContext = new QuestionManagerViewModel();
                await Application.Current.MainPage.Navigation.PushAsync(p);
            }
        }
    }
}
