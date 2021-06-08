using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Views;
using TriviaXamarinApp.ViewModels;
using TriviaXamarinApp;

namespace TriviaXamarinApp.ViewModels
{
    class AddQuestionViewModel : ModelViewBase, INotifyPropertyChanged
    {
        public AddQuestionViewModel() { }
        public void AddQuestion()
        {
            Label = "";
        }
        private string ques;
        public string Question
        {
            get { return this.ques; }
            set { if (this.ques != value) { this.ques = value; OnPropertyChange("Question"); } } 
        }
        private string l;
        public string Label
        {
            get { return this.l; }
            set { if (this.l != value) { this.l = value; OnPropertyChange("Label"); } }
        }

        private string correct;
        public string CorrectAnswer
        {
            get { return this.correct; }
            set { if (this.correct != value) { this.correct = value; OnPropertyChange("CorrectAnswer"); } }
        }
        private string option1;
        public string Option1
        {
            get { return this.option1; }
            set { if (this.option1 != value) { this.option1 = value; OnPropertyChange("Option1"); } }
        }
        private string option2;
        public string Option2
        {
            get { return this.option2; }
            set { if (this.option2 != value) { this.option2 = value; OnPropertyChange("Option2"); } }
        }
        private string option3;
        public string Option3
        {
            get { return this.option3; }
            set { if (this.option3 != value) { this.option3 = value; OnPropertyChange("Option3"); } }
        }
        public Page NextPage { get; set; }
        public ICommand Add => new Command(AddQ);

        public async void AddQ()
        {
            string[] arr = new string[3];
            arr[0] = Option1;
            arr[1] = Option2;
            arr[2] = Option3;

            App app = (App)App.Current;
            User u = app.CurrentUser;
            AmericanQuestion a = new AmericanQuestion
            {
                CorrectAnswer = this.CorrectAnswer,
                QText = this.Question,
                OtherAnswers = arr,
                CreatorNickName = u.NickName,
            };
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            bool res = await proxy.PostNewQuestion(a);

            u.Questions.Add(a);
            if (res)
            {
                AmericanQuestion question = await proxy.GetRandomQuestion();
                Page p = new Play(question, 0);

                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(p);
            }
            else
            {
                Label = "Something went wrong! Please try again";
            }
        }
        public Action<Page> NavigateToPageEvent;
    }
}
