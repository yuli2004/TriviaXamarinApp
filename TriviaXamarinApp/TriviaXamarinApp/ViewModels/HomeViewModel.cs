using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Views;

namespace TriviaXamarinApp.ViewModels
{
    class HomeViewModel : ModelViewBase
    {
        public ICommand NavigateToPageCommand { get; set; }
        public HomeViewModel()
        {
            NavigateToPageCommand = new Command<string>(NavigateToPage);
        }

        private void NavigateToPage(string obj)
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
                case "Play":
                    {
                        p = new Play();
                        p.Title = "Game";
                        p.BindingContext = new PlayViewModel();
                    }
                    break;
                default: break;


            }
            Application.Current.MainPage.Navigation.PushAsync(p);
        }
    }
}
