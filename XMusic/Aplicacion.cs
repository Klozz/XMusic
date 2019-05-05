using System;
using Xamarin.Forms;
using XMusic.View;

namespace XMusic
{
    class Aplicacion : Application
    {
        public Aplicacion()
        {
            MainPage = new NavigationPage(new Paginas());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}