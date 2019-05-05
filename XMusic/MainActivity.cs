using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

using XMusic.Audio;
using Android.Support.Design.Widget;
using Android.Util;
using Android;
using Android.Support.V4.App;
using Xamarin.Forms;

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
        public static int REQUEST_PERMISSIONS = 1;

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
            //Check for permissions
            if (CheckSelfPermission(Manifest.Permission.ReadExternalStorage) != Permission.Granted)
            {
                // Should we show an explanation?
                if (ShouldShowRequestPermissionRationale(
                        Manifest.Permission.ReadExternalStorage))
                {
                    // Explain to the user why we need to read the internal storage
                }
                RequestPermissions(new String[] { Manifest.Permission.ReadExternalStorage },
                          REQUEST_PERMISSIONS);

                // MY_PERMISSIONS_REQUEST_READ_EXTERNAL_STORAGE is an
                // app-defined int constant that should be quite unique

                //return;

            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if(requestCode == REQUEST_PERMISSIONS){
                // Received permission result for storage permission.
                Log.Info("MainActivity", "Received response for storage permission request.");
                if ((grantResults.Length > 0) && (grantResults[0] == Permission.Granted)){
                    Log.Info("MainActivity", "Storage permission has now been granted.");
                }
                else
                {
                    Log.Info("MainActivity", "Storage permission was NOT granted.");
                }

            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
                Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        protected override void OnDestroy()
        {
            StartService(new Intent(Audio.AudioService.ActionTryKill));
            base.OnDestroy();
        }

    }
}