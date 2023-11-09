﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AzieKitchen.Helpers;

namespace AzieKitchen.Views
{	
	public partial class SettingsPage : ContentPage
	{	
		public SettingsPage ()
		{
			InitializeComponent ();
		}
        async void ButtonCategories_Clicked(System.Object sender, System.EventArgs e)
        {
            var acd = new AddCategoryData();
            await acd.AddCategoriesAsync();
        }

        async void ButtonProducts_Clicked(System.Object sender, System.EventArgs e)
        {
            var afd = new AddFoodItemData();
            await afd.AddFoodItemAsync();
        }

        /*private void ButtonCart_Clicked(object sender, EventArgs e)
        {
            var cct = new CreateCartTable();
            if (cct.CreateTable())
                DisplayAlert("Operation Reussie", "table de carte créer", "Ok");
            else
                DisplayAlert("Error", "erreur lors de la creation de la table", "Ok");
        }*/

    }
}

