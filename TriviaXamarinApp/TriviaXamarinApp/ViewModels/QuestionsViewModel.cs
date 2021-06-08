using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Views;
using TriviaXamarinApp.Services;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.ViewModels;

namespace TriviaXamarinApp.ViewModels
{
    class QuestionsViewModel : ModelViewBase
    {
        public ObservableCollection<AmericanQuestion> QuestionList { get; }
        private int c;
        public int Counter
        { 
            get { return this.c; }
            set { if (this.c != value) { this.c = value; OnPropertyChange("Counter"); } }
        }

        private bool able;
        public bool Able
        {
            get { return this.able; }
            set { if (this.able != value) { this.able = value; OnPropertyChange("Able"); } }
        }

        public QuestionsViewModel()
        {
            App a = (App)App.Current;
            User u = a.CurrentUser;
            QuestionList = new ObservableCollection<AmericanQuestion>();
            for (int i = 0; i < u.Questions.Count; i++)
            {
                QuestionList.Add(u.Questions[i]);
            }
            Counter = 0;
            Able = false;
        }

        public ICommand SelctionChanged => new Command<Object>(OnSelectionChanged);
        public void OnSelectionChanged(Object obj)
        {
            if (obj is AmericanQuestion)
            {
                AmericanQuestion chosenQuestion = (AmericanQuestion)obj;
                Page questionPage = new ShowQuestion();
                ShowQuestionViewModel qContext = new ShowQuestionViewModel
                {
                    QText = chosenQuestion.QText,
                    QAnswer = chosenQuestion.CorrectAnswer,
                    QNotAnswers = chosenQuestion.OtherAnswers
                };
                questionPage.BindingContext = qContext;
                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(questionPage);
            }
        }

        public ICommand DeleteCommand => new Command<AmericanQuestion>(RemoveQuestion);
        async void RemoveQuestion(AmericanQuestion a)
        {
            if (QuestionList.Contains(a))
            {
                try
                {

                    TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                    await proxy.DeleteQuestion(a);
                    QuestionList.Remove(a);
                }
                catch (Exception e) { }
            }
            Counter++;
            Able = true;
        }

        public ICommand Add => new Command(AddQ);
        void AddQ()
        {

            Counter--;
            if (Counter <= 0)
                Able = false;
            Page p = new AddQuestion();
            AddQuestionViewModel a = (AddQuestionViewModel)p.BindingContext;
            a.NextPage = new Questions();


            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);

        }
        public Action<Page> NavigateToPageEvent;
    }

}
