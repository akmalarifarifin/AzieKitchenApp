using System;
using AzieKitchen.Models;
using Firebase.Database;
using System.Collections.Generic;
using Firebase.Database.Query;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace AzieKitchen.Helpers
{
	public class AddFoodItemData
	{
        FirebaseClient client;

        public List<FoodItem> FoodItems { get; set; }

        public AddFoodItemData()
		{
            client = new FirebaseClient("https://aziekitchen1-873b3-default-rtdb.asia-southeast1.firebasedatabase.app/");
            FoodItems = new List<FoodItem>()
            {
                new FoodItem
                {
                    ProductID = 1,
                    CategoryID = 1,
                    ImageUrl = "nasiminyak.jpg",
                    Name = "Nasi Minyak Ayam Merah",
                    Description = "Nasi Minyak Aroma, yang digandingkan dengan ayam measak merah",
                    Price = 12
                },
                new FoodItem
                {
                    ProductID = 2,
                    CategoryID = 1,
                    ImageUrl = "nasibriani.jpeg",
                    Name = "Nasi Beriani Gam Ayam",
                    Description = "Briani Batu Pahat menjadi idaman selalu",
                    
                    Price = 15
                },
                 new FoodItem
                {
                    ProductID = 3,
                    CategoryID = 1,
                    ImageUrl = "nasikerabu.jpeg",
                    Name = "Nasi Kerabu Ayam Berempah",
                    Description = "Nasi Kerabu hok buke klate pon suke make",

                    Price = 12
                },

                   new FoodItem
                {
                    ProductID = 4,
                    CategoryID = 1,
                    ImageUrl = "ayamkicap.jpeg",
                    Name = "Nasi Putih Ayam Kicap",
                    Description = "Nasi Putih ayam kicap sedap bak hang",

                    Price = 12
                },
                    new FoodItem
                {
                    ProductID = 5,
                    CategoryID = 1,
                    ImageUrl = "nasiayam.jpeg",
                    Name = "Nasi Ayam Hainan",
                    Description = "Bukan sekadar nasi ayam, pekoqqqqqqq",

                    Price = 13
                },
                     new FoodItem
                {
                    ProductID = 6,
                    CategoryID = 1,
                    ImageUrl = "kungpao.jpeg",
                    Name = "Nasi Putih Ayam Kung Pao",
                    Description = "Nasi ditambah ayam kung pao, walaooowehh sedap",

                    Price = 12
                },
                      new FoodItem
                {
                    ProductID = 7,
                    CategoryID = 1,
                    ImageUrl = "chickenchop.jpeg",
                    Name = "Chicken chop and fries",
                    Description = "Chicken, Chop plus fries",

                    Price = 15
                },
                       new FoodItem
                {
                    ProductID = 8,
                    CategoryID = 1,
                    ImageUrl = "fishandchip.jpeg",
                    Name = "Fish and chip + fires",
                    Description = "Dory Chicken deep fried with tartar sauce",

                    Price = 16
                },
                        new FoodItem
                {
                    ProductID = 9,
                    CategoryID = 2,
                    ImageUrl = "package1.jpeg",
                    Name = "Package 1",
                    Description = "- Nasi Minyak Ayam Merah" +
                    "- Papadom" +
                    "- Kuih melayu" +
                    "- Air Sirap",

                    Price = 20
                },
                         new FoodItem
      
                {
                    ProductID = 10,
                    CategoryID = 2,
                    ImageUrl = "package2.jpeg",
                    Name = "Package 2",
                    Description = "- Nasi Ayam Hainan " +
                    "- Ulam" +
                    "- Kuih Merah Kacang" +
                      "- Air Sirap",

                    Price = 20
                },

            };
        }
        public async Task AddFoodItemAsync()
        {
            try
            {
                foreach (var item in FoodItems)
                {
                    await client.Child("FoodItems").PostAsync(new FoodItem()
                    {
                        CategoryID = item.CategoryID,
                        ProductID = item.ProductID,
                        Description = item.Description,
                
                        ImageUrl = item.ImageUrl,
                        Name = item.Name,
                        Price = item.Price,
                       
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

