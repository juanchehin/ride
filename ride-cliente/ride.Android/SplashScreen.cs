using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace ride.Droid
{
    [Activity(Label = "ride", Icon = "@mipmap/logo",
        Theme = "@style/nuevotema",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(new Intent(Application.Context, typeof(SplashScreen)));
            // Create your application here
        }
    }
}