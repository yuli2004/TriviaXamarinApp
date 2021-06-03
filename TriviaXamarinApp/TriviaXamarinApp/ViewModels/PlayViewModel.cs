using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Views;

namespace TriviaXamarinApp.ViewModels
{
    class ChangeColor:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string color;
        public string Color
        {
            get { return this.color; }
            set { this.color = value; OnPropertyChanged(nameof(color)); }
        }

        private string aText;
        public string AText
        {
            get { return this.aText; }
            set { this.color = value; OnPropertyChanged(nameof(color)); }
        }

    }
    class QuestionsViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<ChangeColor> Answers { get; set; }
        private string qText;
        public string QText
        {
            get { return this.qText; }
            set { this.qText = value; OnPropertyChanged(nameof(QText)); }
        }

        private bool click;
        public bool Click
        {
            get { return this.click; }
            set { this.click = value; OnPropertyChanged(nameof(Click)); }
        }
    }
    
    class PlayViewModel : ModelViewBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string message;
        public string Message
        {
            get { return this.message; }
            set { this.message = value; OnPropertyChanged(nameof(Message)); }
        }
        //ergtrwsyert
        //gjhjft
    }
}
