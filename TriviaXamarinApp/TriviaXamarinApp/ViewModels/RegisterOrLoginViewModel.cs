using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Views;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;

namespace TriviaXamarinApp.ViewModels
{
    class RegisterOrLoginViewModel: ModelViewBase
    {
        public ICommand NavigateToPageCommand { get; set; }
        public RegisterOrLoginViewModel()
        {
            this.NavigateToPageCommand = new Command<string>(NavigateToPage);
        }
        private async void NavigateToPage(string obj)
        {
            Page p = null;
            switch (obj)
            {
                case "Login":
                    {
                        p = new Login();
                        p.Title = "Login";
                        p.BindingContext = new LoginViewModel();
                    }
                    break;
                case "Register":
                    {
                        p = new Register();
                        p.Title = "Register";
                        p.BindingContext = new RegisterViewModel();
                    }
                    break;
                default: break;
            }
            await App.Current.MainPage.Navigation.PushAsync(p);
        }
    }
}
