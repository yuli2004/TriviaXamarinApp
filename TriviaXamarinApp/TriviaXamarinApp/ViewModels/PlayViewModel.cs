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
using TriviaXamarinApp.ViewModels;

namespace TriviaXamarinApp.ViewModels
{
    class AnswerViewModel : ModelViewBase, INotifyPropertyChanged
    {
        private string s;
        private Color c;
        public string Answer
        {
            get { return this.s; }
            set { if (this.s != value) { this.s = value; OnPropertyChange("Answer"); } }
        }
        public Color BackgroundColor
        {
            get { return this.c; }
            set { if (this.c != value) { this.c = value; OnPropertyChange("BackgroundColor"); } }
        }

    }
    class PlayViewModel : ModelViewBase, INotifyPropertyChanged
    {
        public PlayViewModel() { }
        private AmericanQuestion a;
        public AmericanQuestion Question
        {
            get { return this.a; }
            set { if (this.a != value) { this.a = value; OnPropertyChange("Question"); } }
        }

        private int s;
        public int Score
        {
            get { return this.s; }
            set { if (this.s != value) { this.s = value; OnPropertyChange("Score"); } }
        }

        private string q;
        public string QuestionText
        {
            get { return this.q; }
            set { if (this.q != value) { this.q = value; OnPropertyChange("QuestionText"); } }
        }
        private Answer[] arr;
        public Answer[] Options
        {
            get { return this.arr; }
            set { if (this.arr != value) { this.arr = value; OnPropertyChange("Options"); } }
        }
        public string[] Colors { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public PlayViewModel(AmericanQuestion question, int score)
        {
            try
            {
                AmericanQuestion a = question;
                Answer[] options = new Answer[4];
                Random r = new Random();
                int num = r.Next(0, 4);
                options[num] = new Answer
                {
                    text = a.CorrectAnswer,
                    color = new Color(33, 205, 47),
                };
                for (int i = 0, optionNum = 0; i < options.Length; i++)
                {
                    if (options[i] == null)
                    {
                        options[i] = new Answer
                        {
                            text = a.OtherAnswers[optionNum],
                            color = new Color(252, 13, 13),
                        };
                        optionNum++;
                    }
                }

                this.Options = options;
                this.Question = a;
                this.QuestionText = a.QText;
                this.CorrectAnswerIndex = num;
                this.Score = score;
            }
            catch (Exception e) { }
        }

        public ICommand OptionClicked => new Command<Object>(OptionClick);

        public async void OptionClick(Object o)
        {
            if (o is Answer)
            {
                if (((Answer)o).text.Equals(Question.CorrectAnswer))
                {
                    Score++;
                }
            }

            if (Score >= 3)
            {
                bool isLoggedIn = App.Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(App.Current.Properties["IsLoggedIn"]) : false;
                Page p;
                if (!isLoggedIn)
                {
                    p = new Login();
                    p.Title = "Login";
                    p.BindingContext = new LoginViewModel();
                    //log.NextPage = new AddQuestion();
                }
                else
                {
                    p = new AddQuestion();
                    p.Title = "Add question";
                    AddQuestionViewModel add = (AddQuestionViewModel)p.BindingContext;
                    TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                    AmericanQuestion amricanQuestion = await proxy.GetRandomQuestion();
                    add.NextPage = new Play(amricanQuestion, 0);
                }

                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(p);
            }
            else
            {
                TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                AmericanQuestion amricanQuestion = await proxy.GetRandomQuestion();
                Page p = new Play(amricanQuestion, Score);
                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(p);
            }
        }

        public ICommand NavigateToPageCommand { get; set; }
        //public ICommand Home => new Command(GoHome);
        //void GoHome()
        //{
        //    Page p;
        //    if ((string)App.Current.Properties["IsLoggedIn"] == Boolean.TrueString)
        //    {
        //        p = new Play();
        //    }
        //    else
        //    {
        //        p = new Home();
        //    }
        //    if (NavigateToPageEvent != null)
        //        NavigateToPageEvent(p);
        //}
        public Action<Page> NavigateToPageEvent;
    }
}

