using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Airbnb.Lottie;

namespace MyMomsCollection
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;
        LottieAnimationView animationView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.activity_Splash);
               // animationView = FindViewById<LottieAnimationView>(Resource.Id.splashanimview);
               // if (animationView != null)
               // {
                //    animationView.PlayAnimation();
               // }
               
            }
            catch(Exception ex)
            {
            }
        }

      

       

        

        // Launches the startup task
        protected override void OnResume()
        {
            try
            {
                base.OnResume();

                //if (animationView == null)
                //{
                //    animationView = FindViewById<LottieAnimationView>(Resource.Id.splashanimview);
                //    animationView.PlayAnimation();
                //}

               
                Task startupWork = new Task(() => { SimulateStartup(); });
                startupWork.Start();
            }
            catch(Exception ex)
            {

            }

        }
        async void SimulateStartup()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            await Task.Delay(2000); // Simulate a bit of startup work.
            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            //  StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            StartActivity(new Intent(Application.Context, typeof(AfterSplashActivity)));
        }
    }
}