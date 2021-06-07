using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TriviaXamarinApp.ViewModels;

namespace TriviaXamarinApp.ViewModels
{
    public class ShowQuestionViewModel : ModelViewBase, INotifyPropertyChanged
    {
        public string QText { get; set; }
        public string QAnswer { get; set; }
        public string[] QNotAnswers { get; set; }
    }
}
