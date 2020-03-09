using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;

namespace DemoXamarin.ViewModel
{
    public class HomePageViewModel
    {
        INavigationService _navigationService;
        public DelegateCommand<string> OnNavigateCommand { get; set; }
        public HomePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            OnNavigateCommand = new DelegateCommand<string>(NavigateAync);
        }

        private async void NavigateAync(string obj)
        {
            await _navigationService.NavigateAsync(new Uri(obj, UriKind.Relative));
        }
    }
    
}
