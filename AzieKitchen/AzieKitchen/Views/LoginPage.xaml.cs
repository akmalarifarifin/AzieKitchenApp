using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzieKitchen.ViewModels;
using Firebase.Database;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AzieKitchen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private FirebaseClient _firebase;

        public LoginPage()
        {
            InitializeComponent();
            //this.BindingContext = new LoginViewModel();
            _firebase = new FirebaseClient("https://aziekitchen1-873b3-default-rtdb.asia-southeast1.firebasedatabase.app/");
        }
        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // Retrieve user data from the Firebase Realtime Database
                var users = await _firebase.Child("users").OnceAsync<User>();
                var user = users.FirstOrDefault(u => u.Object.Email == EmailEntry.Text)?.Object;

                // Check if the password is correct
                if (user != null && user.Password == PasswordEntry.Text)
                {
                    // Navigate to the next page
                    App.Current.Properties["UserId"] = user.Id;

                    await Shell.Current.GoToAsync("//AboutPage");

                }
                else
                {
                    ErrorMessageLabel.Text = "Invalid email or password";
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "Failed to log in";
            }


        }
    }
}
