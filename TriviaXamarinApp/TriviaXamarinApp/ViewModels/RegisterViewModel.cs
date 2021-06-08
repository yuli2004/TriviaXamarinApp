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
    class RegisterViewModel : ModelViewBase
    {
        private string email, username, password;

        public string Email
        {
            get { return this.email; }
            set { if (this.email != value) { this.email = value; OnPropertyChange(nameof(Email)); } }
        }
        public string UserName
        {
            get { return this.username; }
            set { if (this.username != value) { this.username = value; OnPropertyChange(nameof(UserName)); } }
        }
        public string Password
        {
            get { return this.password; }
            set { if (this.password != value) { this.password = value; OnPropertyChange(nameof(Password)); } }
        }

        public ICommand RegisterCommand { get; set; }

        public RegisterViewModel()
        {
            this.email = "";
            this.username = "";
            this.password = "";

            RegisterCommand = new Command(this.Register);
        }

        public async void Register()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            User u = new User { Email = Email, NickName = UserName, Password = Password };
            bool res = await proxy.RegisterUser(u);

            if (res)
            {
                App a = (App)App.Current;
                a.CurrentUser = u;
                AmericanQuestion amricanQuestion = await proxy.GetRandomQuestion();
                Play p = new Play(amricanQuestion, 0);
                PlayViewModel game = (PlayViewModel)p.BindingContext;
                game.Score = 0;
                p.Title = "Game";
            }
        }
    }
}
