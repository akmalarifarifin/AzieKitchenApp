using System;
using System.Collections.Generic;
using System.Linq;
using AzieKitchen.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Xamarin.Forms;

namespace AzieKitchen.Views
{
    public partial class OrderListPage : ContentPage
    {
        private readonly FirebaseClient firebase;
        private readonly List<Order> orders;

        public OrderListPage()
        {
            InitializeComponent();

            firebase = new FirebaseClient("https://aziekitchen1-873b3-default-rtdb.asia-southeast1.firebasedatabase.app/");
            orders = new List<Order>();

            // Load orders from Firebase database
            //LoadOrders();

            MessagingCenter.Subscribe<PlaceOrderPage>(this, "OrderPlaced", (sender) =>
            {
                LoadOrders();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Reload orders from Firebase database
            LoadOrders();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Clear the list of orders
            orders.Clear();
            orderListView2.ItemsSource = null;
        }

        private async void ShowOrderDetails(Order order)
        {
            // Create a string to display the food items in the order
            string foodItemsString = "";
            foreach (var foodItem in order.CartItems)
            {
                foodItemsString += $"{foodItem.Quantity}x {foodItem.Name} (RM{foodItem.Price:F2})\n";
            }

            // Display the order details in an alert message
            await DisplayAlert($"Order {order.OrderId}",
                $"Address: {order.Address}\nTotal Price: RM{order.TotalPrice:F2}\nStatus: {order.Timestamp}\n\nFood Items:\n{foodItemsString}",
                "OK");
        }


        public async void LoadOrders()
        {
            try
            {
                string userId = (string)App.Current.Properties["UserId"];

                // Get orders from Firebase database
                var orderList = await firebase.Child("Orders").OrderBy("UserId").EqualTo(userId).OnceAsync<Order>();

                // Clear existing orders
                orders.Clear();

                // Add new orders to list
                foreach (var order in orderList)
                {
                    orders.Add(order.Object);
                }

                // Bind orders to ListView
                orderListView2.ItemsSource = orders.OrderByDescending(o => o.Timestamp);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error loading orders: {ex.Message}", "OK");
            }
        }

        private void OrderSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem != null)
                {
                    var selectedOrder = (Order)e.SelectedItem;

                    // Display order details in a popup
                    ShowOrderDetails(selectedOrder);

                    // Deselect item
                    orderListView2.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"Error displaying order details: {ex.Message}", "OK");
            }
        }
    }
}
