using System;
using System.Collections.Generic;
using AzieKitchen.Models;
using AzieKitchen.ViewModels;
using AzieKitchen.Views;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace AzieKitchen
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        ShoppingCartDatabase database;
        public AppShell()
        {
            InitializeComponent();
            
        }

        private async void OnLoggedOutClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirm Logout", "Are you sure you want to logout? All items in your cart will be deleted upon logout.", "Yes", "No");

            if (answer)
            {
                App.Current.Properties.Remove("UserId");
                database = new ShoppingCartDatabase(App.DatabasePath);
                database.DeleteAllItems();
                MessagingCenter.Send(this, "OrderPlaced");
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }

    }
}

