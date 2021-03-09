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
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Firebase;
using Firebase.Database;
using MyMomsCollection.Adapter;
using MyMomsCollection.EventListners;
using MyMomsCollection.Models;
using Plugin.Connectivity;
using Toolbar = Android.Support.V7.Widget.Toolbar;
namespace MyMomsCollection
{
    [Activity(Label = "ArcticleListActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ArcticleListActivity : AppCompatActivity
    {
        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        ArcticleAdapter mAdapter;
        EditText Searchtext;
        ImageView SearchButton;
        ArcticleListners arcticleListners;
        List<Arcticle> lstArcticles = new List<Arcticle>();
        List<Arcticle> lstSegregatedArcticles = new List<Arcticle>();
        LinearLayout LoaderView;
        TextView BarTitle;
        string AuthorName = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.activity_ArticleList);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            //var currentContext = Android.App.Application.Context;
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            LoaderView = FindViewById<LinearLayout>(Resource.Id.LoaderView);
            Searchtext = FindViewById<EditText>(Resource.Id.Searchtext);
            SearchButton = FindViewById<ImageView>(Resource.Id.btnSearch);
            BarTitle = FindViewById<TextView>(Resource.Id.BarTitle);

            AuthorName = Intent.GetStringExtra("Author");
            Searchtext.TextChanged += Searchtext_TextChanged;
            SearchButton.Click += SearchButton_Click;
            RetrieveData();
        }

  
        private void Searchtext_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                List<Arcticle> lstSearchresult = (from arcticle in lstSegregatedArcticles
                                                  where arcticle.ArcticleName.ToLower().Contains(Searchtext.Text.ToLower()) ||
                                                  arcticle.PublishDate.ToLower().Contains(Searchtext.Text.ToLower())
                                                  select arcticle).ToList();
                mAdapter = new ArcticleAdapter(lstSearchresult,this);
                mRecyclerView.SetAdapter(mAdapter);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Toast.MakeText(this, "Oops something went wrong", ToastLength.Long).Show();
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
           if(Searchtext.Visibility==Android.Views.ViewStates.Gone)
            {

                //  Searchtext.Animate().Alpha(1.0f).SetDuration(2000); 
                Animation slide_left_in = AnimationUtils.LoadAnimation(ApplicationContext,Resource.Animation.slide_left_in);
                Searchtext.Visibility = ViewStates.Visible;
                Searchtext.StartAnimation(slide_left_in);
                BarTitle.Visibility = ViewStates.Gone;

            }
            else
            {
                Searchtext.ClearFocus();

                Animation slide_right_out = AnimationUtils.LoadAnimation(ApplicationContext,Resource.Animation.slide_right_out);

                Searchtext.StartAnimation(slide_right_out);
                Searchtext.Visibility = ViewStates.Gone;
                BarTitle.Visibility = ViewStates.Visible;
            }
        }

        public void RetrieveData()
        {
            LoaderView.Visibility = ViewStates.Visible;
            arcticleListners = new ArcticleListners();
            arcticleListners.Create();
            arcticleListners.ArticleRetrieved += ArcticleListners_ArticleRetrieved;
        }

        private void ArcticleListners_ArticleRetrieved(object sender, ArcticleListners.ArcticleDataEventArgs e)
        {
            if(!CrossConnectivity.Current.IsConnected)
            {
                LoaderView.Visibility = ViewStates.Gone;
                Toast.MakeText(this, "Please check your internet connection", ToastLength.Long).Show();
                return;
            }
            try
            {
                lstArcticles = e.Arcticle;
                lstSegregatedArcticles = lstArcticles.Where(x => x.Author == AuthorName).ToList();
                SetUpRecyclerView();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                LoaderView.Visibility = ViewStates.Gone;
                Toast.MakeText(this, "Oops something went wrong", ToastLength.Long).Show();
            }

        }

        private void SetUpRecyclerView()
        {
            try
            {
                // Plug in the linear layout manager:
                mLayoutManager = new LinearLayoutManager(this);
                mRecyclerView.SetLayoutManager(mLayoutManager);

                // Plug in my adapter:
                mAdapter = new ArcticleAdapter(lstSegregatedArcticles, this);
                mRecyclerView.SetAdapter(mAdapter);
                LoaderView.Visibility = ViewStates.Gone;
            }
            catch(Exception ex)
            {
                LoaderView.Visibility = ViewStates.Gone;
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