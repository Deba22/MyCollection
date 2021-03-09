using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Plugin.Connectivity;
using System.Net.Http;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using System.IO;
using Newtonsoft.Json;
using MyMomsCollection.Models;
using FFImageLoading;
using FFImageLoading.Views;
using Android.Webkit;
using Android.Text;
using Android.Animation;
using System.Threading.Tasks;

namespace MyMomsCollection
{
    [Activity(Label = "ArcticleDescriptionActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ArcticleDescriptionActivity : AppCompatActivity
    {
        ImageViewAsync expandedImage;
        TextView ArcticleDesctxt,  Arcticletitle, toolbar_title;
        WebView ArcticleDescWV;
        CollapsingToolbarLayout collapsingToolbar;
        AppBarLayout appBarLayout;
        bool isShow = false;
        int scrollRange = -1;
        string ArcticleName = "";
        string DownloadUrl = "";


        private FloatingActionButton fabMain;
      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_ArcticleDescription);

            ArcticleDesctxt = FindViewById<TextView>(Resource.Id.ArcticleDesctxt);
            ArcticleDescWV = FindViewById<WebView>(Resource.Id.ArcticleDescWV);
            var ArcticleDesc = Intent.GetStringExtra("ArcticleDesc");
            ArcticleDesctxt.SetText(Html.FromHtml(ArcticleDesc), TextView.BufferType.Normal);
            var ArcticleDescNew = ArcticleDesc.Replace("<br />", "<br/><br/>");
            String htmlText = $"<html><body style=\"text-align:justify\"> {ArcticleDescNew}</body></html>";
            ArcticleDescWV.LoadData(htmlText, "text/html; charset=utf-8", "UTF-8");
           
            ArcticleName = Intent.GetStringExtra("ArcticleName").ToString();
            DownloadUrl = Intent.GetStringExtra("DownloadUrl").ToString();

            expandedImage = FindViewById<ImageViewAsync>(Resource.Id.expandedImage);
            LoadImage();

           
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsingToolbar);
           
            //This is the most important when you are putting custom textview in CollapsingToolbar
            collapsingToolbar.SetTitle(" ");
            appBarLayout = FindViewById<AppBarLayout>(Resource.Id.appBarLayout);
            appBarLayout.OffsetChanged += AppBarLayout_OffsetChanged;
            Arcticletitle = FindViewById<TextView>(Resource.Id.Arcticletitle);
            Arcticletitle.Text = ArcticleName;

            toolbar_title= FindViewById<TextView>(Resource.Id.toolbar_title);
            toolbar_title.Text = "";

          fabMain = FindViewById<FloatingActionButton>(Resource.Id.fab_main);
          
            fabMain.Click += (o, e) =>
            {
               
                    ShowFabMenu();
                
            };

           


        }
        private async void ShowFabMenu()
        {
            
            fabMain.Animate().Rotation(360f);
           await Task.Delay(300);

            Intent sharingIntent = new Intent(Intent.ActionSend);
            sharingIntent.SetType("text/plain");
            //sharingIntent.PutExtra(Intent.ExtraTitle, ArcticleName);
            sharingIntent.PutExtra(Intent.ExtraText,"Click on this link to download and read the content "+ DownloadUrl);
            StartActivity(Intent.CreateChooser(sharingIntent, "Share"));
            
            fabMain.Animate().Rotation(0f);

        }

      
        private void AppBarLayout_OffsetChanged(object sender, AppBarLayout.OffsetChangedEventArgs e)
        {
            if (scrollRange == -1)
            {
                scrollRange = e.AppBarLayout.TotalScrollRange;
            }
            if (scrollRange + e.VerticalOffset == 0)
            {
               
                Arcticletitle.Text = " ";
                toolbar_title.Text = ArcticleName;
                isShow = true;
            }
            else if (isShow)
            {
                //carefull there must a space between double quote otherwise it dose't work
                toolbar_title.Text = " ";
                Arcticletitle.Text = ArcticleName;
                isShow = false;
            }
          
        }

        private async void LoadImage()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
               
                Toast.MakeText(this, "Please check your internet connection", ToastLength.Long).Show();
                return;
            }
            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMilliseconds(20000);
              
                var msg = await client.GetAsync("https://api.unsplash.com/photos/random/?query=wanderlust&client_id=3e0d69a858b7f1c0619ad644072fcff3a8da383798cafea09c382b344df31d21");

                if (msg.IsSuccessStatusCode)
                {
                    using (var stream = await msg.Content.ReadAsStreamAsync())
                    {
                        using (var streamreader = new StreamReader(stream))
                        {
                            var str = await streamreader.ReadToEndAsync();
                            var oResponse = JsonConvert.DeserializeObject<ExternalImageModel>(str);

                           
                            ImageService.Instance.LoadUrl(oResponse.urls.regular).Preload();
                            ImageService.Instance.LoadUrl(oResponse.urls.regular).LoadingPlaceholder("BackHeader.jpg", FFImageLoading.Work.ImageSource.CompiledResource).ErrorPlaceholder("BackHeader.jpg").Into(expandedImage);

                        }
                    }
                }
                else
                {
                    Toast.MakeText(this, "Oops something went wrong", ToastLength.Long).Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
                Toast.MakeText(this, "Oops something went wrong", ToastLength.Long).Show();
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}