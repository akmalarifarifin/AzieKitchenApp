using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using Firebase.Database.Query;
using Xamarin.Forms;
using AzieKitchen.Models;


namespace AzieKitchen.Views

{

    

    public partial class Package1 : ContentPage
    {
        public Package1()
        {
            InitializeComponent();

            LoadFoodItems();
        }

        private async void LoadFoodItems()
        {
            FirebaseClient firebaseClient = new FirebaseClient("https://aziekitchen1-873b3-default-rtdb.asia-southeast1.firebasedatabase.app/");

            List<FoodItem> FoodItems = (await firebaseClient.Child("FoodItems").OnceAsync<FoodItem>()).Where(item => item.Object.CategoryID == 2)
     .Select(item => new FoodItem
     {
         CategoryID = item.Object.CategoryID,
         ImageUrl = item.Object.ImageUrl,
         Name = item.Object.Name,
         Description = item.Object.Description,
         Price = item.Object.Price,
         ProductID = item.Object.ProductID
     })
     .ToList();

            BindingContext = new { FoodItems };
        }
        private async void OnFoodItemTapped(object sender, EventArgs e)
        {
            var foodItem = ((sender as Image).BindingContext as FoodItem);
            var foodDetailsPage = new FoodDetailsPage(foodItem);
            foodDetailsPage.FoodItem = foodItem; // Store the FoodItem as a property on the page
            await Navigation.PushAsync(foodDetailsPage);
        }

    }
}