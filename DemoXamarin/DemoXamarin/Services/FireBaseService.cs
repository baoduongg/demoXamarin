﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoXamarin.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace DemoXamarin.Services
{
    public class FireBaseService
    {
        private string _class = "Moneys";
        FirebaseClient firebase = new FirebaseClient("https://money-39dec.firebaseio.com/");
        public async Task<List<MoneyModel>> GetListMoney()
        {
            var reponse = await firebase.Child(_class).OnceAsync<MoneyModel>();
            return reponse.Select(item => new MoneyModel { Date=item.Object.Date,Money=item.Object.Money,Title=item.Object.Title,Key=item.Key }).ToList();
               
        }
        public async Task AddMoney(DateTime date, string money, string title)
        {

            await firebase.Child(_class).PostAsync(new MoneyModel() { Date = date, Money = money, Title = title });
        }
        public async Task UpdateMoney(DateTime date, string money, string title, string index)
        {

            var toUpdatePerson = (await firebase.Child(_class).OnceAsync<object>()).Where(a => a.Key == index).FirstOrDefault();
            await firebase.Child(_class).Child(toUpdatePerson.Key).PutAsync(new MoneyModel() { Date = date, Money = money, Title = title });
        }
        public async Task DeleteMoney(string index)
        {
            var toDeleteMoney = (await firebase.Child(_class).OnceAsync<object>()).Where(a => a.Key == index).FirstOrDefault();
            await firebase.Child(_class).Child(toDeleteMoney.Key).DeleteAsync();
        }
    }
}
