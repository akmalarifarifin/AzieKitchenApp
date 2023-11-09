using System;
using AzieKitchen.Models;
using Firebase.Database;
using System.Collections.Generic;
using Firebase.Database.Query;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AzieKitchen.Helpers
{
	public class AddCategoryData
	{
        FirebaseClient client;

        public List<Category> Categories { get; set; }
        public AddCategoryData()
        {
            client = new FirebaseClient("https://aziekitchen1-873b3-default-rtdb.asia-southeast1.firebasedatabase.app/");
            Categories = new List<Category>()
            {
                new Category(){
                    CategoryID = 1,
                    CategoryName = "Ala-Carte",
                    CategoryPoster = "AlaCarte",
                    ImageUrl = "alacarte.jpeg"
                },
                new Category(){
                    CategoryID = 2,
                    CategoryName = "Package",
                    CategoryPoster = "Package",
                    ImageUrl = "packagemelayu.jpeg"
                },
            };
         }
        public async Task AddCategoriesAsync()
        {
            try
            {
                foreach (var category in Categories)
                {
                    await client.Child("Categories").PostAsync(new Category()
                    {
                        CategoryID = category.CategoryID,
                        CategoryName = category.CategoryName,
                        CategoryPoster = category.CategoryPoster,
                        ImageUrl = category.ImageUrl
                    });
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}

