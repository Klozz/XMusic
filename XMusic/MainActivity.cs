using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

using XMusic.Audio;

namespace XMusic
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true,
        LaunchMode = LaunchMode.SingleInstance, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IJavaObject
    {
        public static MainActivity Instance;

        public bool IsBound = false;
        public Intent AudioServiceIntent;
        public static AudioServiceBinder Binder;

        private AudioServiceConnection _connection;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.activity_main);

            Instance = this;
            AudioServiceIntent = new Intent(Audio.AudioService.ActionStart);
            //AudioServiceIntent = new Intent("mx.xperience.START");
            AudioServiceIntent.SetPackage("mx.xperience.XMusic");//this needs to be explicit declared
            ComponentName name = StartService(AudioServiceIntent);
            _connection = new AudioServiceConnection(this);
            bool binded = BindService(AudioServiceIntent, _connection, Bind.AutoCreate);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new Aplicacion());

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnDestroy()
        {
            StartService(new Intent(Audio.AudioService.ActionTryKill));
            base.OnDestroy();
        }

    }
}