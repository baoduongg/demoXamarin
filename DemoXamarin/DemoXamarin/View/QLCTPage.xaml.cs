using System;
using System.Collections.Generic;
using DemoXamarin.ViewModel;
using Xamarin.Forms;

namespace DemoXamarin.View
{
    public partial class QLCTPage : ContentPage
    {
        public QLCTPage()
        {
            InitializeComponent();
         
        }
        public QLCTPageViewModel ViewModel { get { return BindingContext as QLCTPageViewModel; } }
        void ListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectedItemExcute(e);
        }
    }
}
