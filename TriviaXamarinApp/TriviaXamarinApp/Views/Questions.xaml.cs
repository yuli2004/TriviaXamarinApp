﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaXamarinApp.ViewModels;
namespace TriviaXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Questions : ContentPage
    {
        public Questions()
        {
            QuestionsViewModel context = new QuestionsViewModel();
            //Register to the event so the view model will be able to navigate to the monkeypage
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