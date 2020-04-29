using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace RecycleGame
{
    [Activity(Label = "RecycleGame"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize | ConfigChanges.ScreenLayout)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var g = new Game1();
            Xamarin.Essentials.Platform.Init(this, bundle);
            SetContentView((View)g.Services.GetService(typeof(View)));
            
            g.Run();
        }
    }
}

