

using System;
using System.Collections.Generic;
using AzieKitchen.Models;
using Xamarin.Forms;
using Firebase.Database;
using SQLite;
using System.IO;

using Firebase.Database.Query;

namespace AzieKitchen.Views
{
    public partial class PlaceOrderPage : ContentPage
    {
        ShoppingCartDatabase database;
        private List<ShoppingCartItem> cartItems;
        private readonly double totalPrice;

        public PlaceOrderPage(List<ShoppingCartItem> cartItems, double totalPrice)
        {
            InitializeComponent();

            this.cartItems = cartItems;
            this.totalPrice = totalPrice;

            // Bind the cart items to the ListView
            cartListView2.ItemsSource = cartItems;

            // Set the total price label
            totalPriceLabel.Text = $"Total Price: RM{totalPrice:F2}";


        }


        private async void PlaceOrderclicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(addressEditor.Text))
            {
                await DisplayAlert("Error", "Please enter a valid address.", "OK");
                return;
            }

            if (cartItems.Count == 0)
            {
                await DisplayAlert("Error", "Your cart is empty.", "OK");
                return;
            }
            string userId = (string)App.Current.Properties["UserId"];
            // Save order details to Firebase
            var firebase = new FirebaseClient("https://aziekitchen1-873b3-default-rtdb.asia-southeast1.firebasedatabase.app/");
            var order = new Order
            {
                OrderId = Guid.NewGuid().ToString(), // Generate a unique ID for the order
                UserId = userId,
                CartItems = cartItems,
                TotalPrice = totalPrice,
                Address = addressEditor.Text,
                Timestamp = DateTime.Now
            };
            await firebase.Child("Orders").PostAsync(order);
         
            await DisplayAlert("Order Created", $"Your order with ID {order.OrderId} has been created.", "OK");
            database = new ShoppingCartDatabase(App.DatabasePath);
            database.DeleteAllItems();

            MessagingCenter.Send(this, "OrderPlaced");

            // Navigate to the confirmation page
            await Shell.Current.GoToAsync("//AboutPage");

        }
    }
}

public class Order
{
    public string OrderId { get; set; }
    public string UserId { get; set; }
    public List<ShoppingCartItem> CartItems { get; set; }
    public double TotalPrice { get; set; }
    public string Address { get; set; }
    public DateTime Timestamp { get; set; }

}
