using System;
using Prism.Mvvm;

namespace DemoXamarin
{
    public class MainPageViewModel:BindableBase
    {
        private string _lbTest="abc";
        public string LbTest
        {
            get => _lbTest;
            set => SetProperty(ref _lbTest, value);
        }
        public MainPageViewModel()
        {
        }
    }
}
