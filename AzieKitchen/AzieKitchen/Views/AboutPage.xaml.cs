using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AzieKitchen.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }
        private void OnImage1Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Package());
        }

        private void OnImage2Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Package1());
        }
        private void CartClicked(object sender, EventArgs e)
        {
            // Do something when the button is clicked
            Navigation.PushAsync(new CartPage());
        }


    }
}
