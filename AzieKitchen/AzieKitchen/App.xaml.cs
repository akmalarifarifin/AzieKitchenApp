using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AzieKitchen.Services;
using AzieKitchen.Views;
using Xamarin.Essentials;
using SQLite;
using System.IO;

namespace AzieKitchen
{
    public partial class App : Application
    {
        private static string databasePath;
        public static string DatabasePath
        {
            get
            {
                if (string.IsNullOrEmpty(databasePath))
                {
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "shoppingcart.db3");
                }
                return databasePath;
            }
        }
        public App ()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            //MainPage = new NavigationPage(new LoginPage());
            //MainPage = new NavigationPage(new SettingsPage());


        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

