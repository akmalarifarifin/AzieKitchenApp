using System;
using Xamarin.Forms;
using AzieKitchen.Models;

namespace AzieKitchen.Views
{
    public partial class FoodDetailsPage : ContentPage
    {
        private FoodItem foodItem;
        public FoodItem FoodItem { get; set; }
        public FoodDetailsPage(FoodItem foodItem)
        {
            InitializeComponent();
            FoodItem = foodItem;

            foodImage.Source = foodItem.ImageUrl;
            foodNameLabel.Text = foodItem.Name;
            foodDescriptionLabel.Text = $"Description: {foodItem.Description}";
            foodPriceLabel.Text = string.Format("Price: RM{0:N2}", foodItem.Price);

        }
        private void OnAddToCartClicked(object sender, EventArgs e)
        {
           // Get the selected food item
    var foodItem = new FoodItem
    {
        CategoryID = this.FoodItem.CategoryID,
        ImageUrl = this.FoodItem.ImageUrl,
        Name = this.FoodItem.Name,
        Description = this.FoodItem.Description,
        Price = this.FoodItem.Price,
        ProductID = this.FoodItem.ProductID
    };

            var quantity = (int)quantityStepper.Value;
            

            var shoppingCartItem = new ShoppingCartItem
            {
                ProductID = foodItem.ProductID,
                Name = foodItem.Name,
                Price = foodItem.Price,
                Quantity = quantity
            };
            // Add the selected food item to the shopping cart
            var database = new ShoppingCartDatabase(App.DatabasePath);
            database.AddItem(shoppingCartItem);

            // Display a message to confirm that the item has been added to the cart
             DisplayAlert("Success", $"{quantity} {foodItem.Name}(s) have been added to the cart", "OK");
            var cartPage = new CartPage();
            cartPage.UpdateTotalPriceLabel();
        }
            
        
        private async void OnViewCartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartPage());
        }
        }
}
