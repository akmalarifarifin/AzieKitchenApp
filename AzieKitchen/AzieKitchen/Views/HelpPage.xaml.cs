using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace AzieKitchen.Views
{	
	public partial class HelpPage : ContentPage
	{	
		public HelpPage ()
		{
			InitializeComponent ();
		}
        private void OnWhatsAppTapped(object sender, EventArgs e)
        {
            //string phoneNumber = "0123456789"; // Replace with your desired phone number
           // string message = "Hello!"; // Replace with your desired message
            Launcher.OpenAsync($"https://wa.link/btyfnn");
        }
     

    }
}

