using System;
using Firebase.Database;
using Firebase.Database.Query;
using Xamarin.Forms;

namespace AzieKitchen.Views
{
    public partial class RegisterPage : ContentPage
    {
        private FirebaseClient firebase;

        public RegisterPage()
        {
            InitializeComponent();

            // Initialize Firebase Realtime Database
            firebase = new FirebaseClient("https://aziekitchen1-873b3-default-rtdb.asia-southeast1.firebasedatabase.app/");
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            string name = NameEntry.Text;
            string email = EmailEntry.Text;
            string phone = PhoneEntry.Text;
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            // Validate user input
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword))
            {
                await DisplayAlert("Error", "Please fill out all fields.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            // Create a new user object with auto-increment ID
            var response = await firebase.Child("users").PostAsync(new User
            {
                Name = name,
                Email = email,
                Phone = phone,
                Password = password
            });

            string key = response.Key;
            User newUser = new User
            {
                Id = key,
                Name = name,
                Email = email,
                Phone = phone,
                Password = password
            };

            try
            {
                // Update the user object with the auto-increment ID
                await firebase.Child("users").Child(key).PutAsync(newUser);

                await DisplayAlert("Success", "Registration successful!", "OK");

                // Navigate to the main page
                await Navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to register user: {ex.Message}", "OK");
            }
        }
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
