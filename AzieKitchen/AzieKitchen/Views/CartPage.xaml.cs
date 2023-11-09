using System;
using System.Collections.Generic;
using System.Linq;
using AzieKitchen;
using Xamarin.Forms;
using AzieKitchen.Models;
using AzieKitchen.ViewModels;

namespace AzieKitchen.Views
{
    public partial class CartPage : ContentPage
    {
        ShoppingCartDatabase database;
        public CartPage()
        {
            InitializeComponent();
            database = new ShoppingCartDatabase(App.DatabasePath);


            List<ShoppingCartItem> cartItems = database.GetItems();
            double totalPrice = 0;

            foreach (ShoppingCartItem item in cartItems)
            {
                totalPrice += item.Quantity * item.Price;
            }

            totalPriceLabel.Text = $"Total Price: RM{totalPrice:F2}";


            cartListView.ItemsSource = cartItems;
        }

        public void UpdateTotalPriceLabel()
        {
            List<ShoppingCartItem> cartItems = database.GetItems();

            double totalPrice = 0;

            foreach (ShoppingCartItem item in cartItems)
            {
                totalPrice += item.Quantity * item.Price;
            }

            totalPriceLabel.Text = $"Total Price: {totalPrice:C}";
        }
        private async void DeleteItemClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button.BindingContext as ShoppingCartItem;

            bool answer = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {item.Name}?", "Yes", "No");

            if (answer)
            {
                database.DeleteItem(item);
                UpdateTotalPriceLabel();
                cartListView.ItemsSource = database.GetItems();
            }
        }


        private async void CheckoutClicked(object sender, EventArgs e)
        {
            var cartItems = database.GetItems();

            // Calculate the total price of the order
            double totalPrice = cartItems.Sum(item => item.Quantity * item.Price )+25;

            // Navigate to the place order page with order summary
            await Navigation.PushAsync(new PlaceOrderPage(cartItems, totalPrice));
        }


    }
}
