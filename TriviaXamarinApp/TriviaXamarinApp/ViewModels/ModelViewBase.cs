using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TriviaXamarinApp.ViewModels
{
   public class ModelViewBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChange(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
