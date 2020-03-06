using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using DemoXamarin.Models;
using DemoXamarin.Services;
using Prism.Mvvm;
using Xamarin.Forms;

namespace DemoXamarin.ViewModel
{
    public class QLCTPageViewModel:BindableBase
    {
        private readonly FireBaseService _firebaseService = new FireBaseService();
        public QLCTPageViewModel()
        {
            RefreshCommandExcute();
        }

        private ICommand _refreshList;
        public ICommand RefreshList => _refreshList ?? (_refreshList = new Command(RefreshCommandExcute));
        private ICommand _btncommand;
        public ICommand BtnCommand => _btncommand ?? (_btncommand = new Command(BtnCommandExcute));

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _withPeople;
        public string WithPeople
        {
            get => _withPeople;
            set => SetProperty(ref _withPeople, value);
        }
        private bool _unSelected = true;
        public bool UnSelected
        {
            get => _unSelected;
            set => SetProperty(ref _unSelected, value);
        }
        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        private string _money;
        public string Money
        {
            get => _money;
            set => SetProperty(ref _money, value);
        }
        private ObservableCollection<MoneyModel> _data = new ObservableCollection<MoneyModel>();
        public ObservableCollection<MoneyModel> Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }
        private bool _isrefreshing;
        public bool IsRefresing
        {
            get => _isrefreshing;
            set => SetProperty(ref _isrefreshing, value);
        }
        private MoneyModel _selectedItem;
        public MoneyModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
        private double _sumMoney;

        private async void ResetData()
        {

            Data.Clear();
            var reponse = await _firebaseService.GetListMoney();
            foreach (var item in reponse)
                Data.Add(item);
        }
        private ICommand _countMoney;
        public ICommand CountMoney => _countMoney ?? (_countMoney = new Command(CountMoneyExcute));
        private async void CountMoneyExcute()
        {
            double result;
            var reponse = await _firebaseService.GetListMoney();
            foreach (var item in reponse)
            {
                double.TryParse(item.Money, out result);
                _sumMoney += result;
            }
            await Application.Current.MainPage.DisplayAlert("Thong bao", $"Tong tien: {_sumMoney}", "Ok");
        }
        private string Key;
        private ICommand _updateMoney;
        public ICommand UpdateMoneyCommand => _updateMoney ?? (_updateMoney = new Command(UpdateMoneyExcute));
        public async void UpdateMoneyExcute()
        {
            try
            {
                await _firebaseService.UpdateMoney(Date, Money, Title, Key);
                await Application.Current.MainPage.DisplayAlert("Thong bao", $"Edit Thanh cong", "Ok");
                RefreshCommandExcute();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Thong bao", $"Edit that bai", "Ok");
            }
        }
        private ICommand _deleteMoney;
        public ICommand DeleteMoneyCommand => _deleteMoney ?? (_deleteMoney = new Command(DeleteMoneyExcute));
        public async void DeleteMoneyExcute()
        {
            try
            {
                await _firebaseService.DeleteMoney(Key);
                await Application.Current.MainPage.DisplayAlert("Thong bao", $"Xoa Thanh cong", "Ok");
                RefreshCommandExcute();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Thong bao", $"Xoa that bai", "Ok");
            }
        }
        public async Task SelectedItemExcute(SelectedItemChangedEventArgs e)
        {

            UnSelected = false;
            Money = SelectedItem.Money;
            Title = SelectedItem.Title;
            Date = SelectedItem.Date;
            Key = SelectedItem.Key;
        }
        private async void BtnCommandExcute()
        {
            if (Money != string.Empty && Title != string.Empty)
            {
                try
                {
                    await _firebaseService.AddMoney(Date, Money, Title);
                    await Application.Current.MainPage.DisplayAlert("Thong bao", $"Them Thanh cong", "Ok");
                    RefreshCommandExcute();
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Thong bao", $"Them that bai", "Ok");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Thong bao", $"Ban can nhap day du thong tin", "Ok");
            }
        }
        private async void RefreshCommandExcute()
        {
            IsRefresing = true;
            Title = string.Empty;
            Money = string.Empty;
            UnSelected = true;
            WithPeople = string.Empty;
            ResetData();
            IsRefresing = false;
        }

    }
}
