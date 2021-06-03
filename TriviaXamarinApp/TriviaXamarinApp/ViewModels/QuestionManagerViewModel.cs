using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Views;

namespace TriviaXamarinApp.ViewModels
{
    class QuestionManagerViewModel
    {
        public ICommand NavigateToPageCommand { get; set; }
        public QuestionManagerViewModel()
        {
            NavigateToPageCommand = new Command<string>(NavigateToPage);
        }

        private void NavigateToPage(string obj)
        {
            Page p = null;
            switch (obj)
            {
                case "Update / delete Question":
                    {
                        p = new Login();
                        p.Title = "Login";
                        p.BindingContext = new LoginViewModel();
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
