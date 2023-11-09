using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using Firebase.Database.Query;
using Xamarin.Forms;
using AzieKitchen.Models;
using static System.Collections.Specialized.BitVector32;


namespace AzieKitchen.Views

{

    public class FoodItem
    {
        public int CategoryID { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int ProductID { get; set; }
    }

    public partial class Package : ContentPage
    {
        public Package()
        {
            InitializeComponent();

            LoadFoodItems();
        }

        private async void LoadFoodItems()
        {
            FirebaseClient firebaseClient = new FirebaseClient("https://aziekitchen1-873b3-default-rtdb.asia-southeast1.firebasedatabase.app/");
            
            List<FoodItem> FoodItems = (await firebaseClient.Child("FoodItems").OnceAsync<FoodItem>()).Where(item => item.Object.CategoryID == 1)
     .Select(item => new FoodItem
     {
         CategoryID = item.Object.CategoryID,
         ImageUrl = item.Object.ImageUrl,
         Name = item.Object.Name,
         Description=item.Object.Description,
         Price=item.Object.Price,
         ProductID=item.Object.ProductID
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

        private async void OnTab1Clicked(object sender, EventArgs e)
        {
            await scrollView.ScrollToAsync(0, 50, true);
        }

        private async void OnTab2Clicked(object sender, EventArgs e)
        {
            await scrollView.ScrollToAsync(0, 900, true);
        }

        private async void OnTab3Clicked(object sender, EventArgs e)
        {
            await scrollView.ScrollToAsync(0, 1400, true);
        }


    }
}