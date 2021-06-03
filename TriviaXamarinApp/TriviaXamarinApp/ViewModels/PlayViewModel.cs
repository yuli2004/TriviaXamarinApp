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
    class ChangeColor : ModelViewBase, INotifyPropertyChanged
    {
        private string color;
        public string Color
        {
            get { return this.color; }
            set { if (this.color != value) { this.color = value; OnPropertyChange(nameof(Color)); } }
        }

        private string answerText;
        public string AnswerText
        {
            get { return this.answerText; }
            set { if (this.answerText != value) { this.answerText = value; OnPropertyChange(nameof(Color)); } }
        }
    }
    class PlayViewModel : ModelViewBase, INotifyPropertyChanged
    {
        public ObservableCollection<ChangeColor> Answers { get; set; }
        private string questionText;
        public string QuestionText
        {
            get { return this.questionText; }
            set { this.questionText = value; OnPropertyChange(nameof(QuestionText)); }
        }

        private bool click;
        public bool Click
        {
            get { return this.click; }
            set { this.click = value; OnPropertyChange(nameof(Click)); }
        }
        private string message;
        public string Message
        {
            get { return this.message; }
            set { this.message = value; OnPropertyChange(nameof(Message)); }
        }

        private int counter = 0;
        private bool answerd;
        public AmericanQuestion Question { get; set; }

        public PlayViewModel()
        {
            Answers = new ObservableCollection<ChangeColor>();
            Question = new AmericanQuestion();
            this.GetQuestion();
            this.answerd = false;
            Click = false;
            Message = "";

        }

        public void GetQuestion()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            Question = new AmericanQuestion();
            if (Question != null)
                QuestionText = Question.QText;
            this.Show();
        }

        public void Show()
        {
            Random r = new Random();
            int index = r.Next(0, 5);
            string[] sArr = new string[4];
            sArr[index] = Question.CorrectAnswer;
            int countOther = 0;
            for (int i = 0; i < sArr.Length; i++)
            {
                if (sArr[i] == null)
                {
                    sArr[i] = Question.OtherAnswers[countOther];
                    countOther++;
                }
            }
            for (int i = 0; i < sArr.Length; i++)
            {
                ChangeColor c = new ChangeColor
                {
                    Color = "Black",
                    AnswerText = sArr[i]
                };
                Answers.Add(c);
            }
        }

        public ICommand IsCorrectCommand => new Command<ChangeColor>(ChangeColor);
        public void ChangeColor(ChangeColor clicked)
        {
            if(!answerd)
            {
                if (clicked.AnswerText == Question.CorrectAnswer)
                {
                    clicked.Color = "Green";
                    this.counter++;
                }
                else
                    clicked.Color = "Red";
                answerd = true;
            }
            if (this.counter != 0 && this.counter % 3 == 0)
                Click = true;
        }

        public ICommand NavigateToPageCommand => new Command<string>(NavigateToPage);
        private async void NavigateToPage(string obj)
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            App a = (App)App.Current;

            if (a != null)
            {
                Page p = new QuestionManager();
                p.Title = "QuestionManager";
                p.BindingContext = new QuestionManagerViewModel();
                await App.Current.MainPage.Navigation.PushAsync(p);
            }
        }
    }
}

