using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Gigamole.Infinitecycleviewpager;
using MyMomsCollection.Adapter;
using MyMomsCollection.Models;

namespace MyMomsCollection
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class AfterSplashActivity  : AppCompatActivity
    {
        List<CarouselModel> lstImages = new List<CarouselModel>();
        TextView txtVersion;
        Button btnStartApp;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_AfterSplash);
            txtVersion = FindViewById<TextView>(Resource.Id.txtVersion);
           // btnStartApp = FindViewById<Button>(Resource.Id.btnStartApp);
            txtVersion.Text = "v " + Application.Context.ApplicationContext.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionName;

            InitData();
           HorizontalInfiniteCycleViewPager viewPager = FindViewById<HorizontalInfiniteCycleViewPager>(Resource.Id.horizontal_viewpager);
            CarouselAdapter carouselAdapter = new CarouselAdapter(lstImages, BaseContext);
            viewPager.Adapter = carouselAdapter;
            //btnStartApp.Click += async (sender, e) =>
            //{
                
            //     var intent = new Intent(this, typeof(MainActivity));
            //    StartActivity(intent);
            //};
            // Create your application here
        }

        private void InitData()
        {
            lstImages.Add(new CarouselModel("Golda","Gracias",Resource.Drawable.goldaMain));
            lstImages.Add(new CarouselModel("Curie","Pereira" ,Resource.Drawable.curieMain));
        }
    }
}
 //<Button
 //           android:id="@+id/btnStartApp"
 //           android:layout_height="wrap_content"
 //           android:layout_width="match_parent"
 //           android:text="LET'S GET STARTED"
 //           android:padding="20dp"
 //           android:textColor="#ffffff"
	//		  android:background="@color/colorPrimary"
	//	android:layout_centerHorizontal="true"
	// android:layout_alignParentBottom="true"
	//		 />