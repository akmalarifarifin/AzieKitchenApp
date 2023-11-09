using System;
using Firebase.Database;
using Firebase.Database.Query;
using Xamarin.Forms;

namespace AzieKitchen.Views
{
    public partial class ProfilePage : ContentPage
    {
        private FirebaseClient firebase;
        public User CurrentUser { get; set; }

        public ProfilePage()
        {
            InitializeComponent();

            // Initialize Firebase Realtime Database
            firebase = new FirebaseClient("https://aziekitchen1-873b3-default-rtdb.asia-southeast1.firebasedatabase.app/");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            string userId = (string)App.Current.Properties["UserId"];

            // Fetch the current user's data from the database
            var user = await firebase.Child("users").Child(userId).OnceSingleAsync<User>();
            CurrentUser = user;

            // Set the binding context for the page
            BindingContext = CurrentUser;
        }

        private async void SaveChangesButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Update the current user's data in the database
                await firebase.Child("users").Child(CurrentUser.Id).PutAsync(CurrentUser);

                // Display a success message
                await DisplayAlert("Success", "Profile updated successfully!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to update profile: {ex.Message}", "OK");
            }
        }
    }
}
