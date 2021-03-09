using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Com.Airbnb.Lottie;
using System.Threading.Tasks;
using Android.Content.PM;
using Refractored.Controls;

namespace MyMomsCollection
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        Button btnStartUp;
        ImageView  backbtn;
        LottieAnimationView animationView;
        TextView txtVersion, txtFirstName, txtSurname, txtAppIntro;
        CircleImageView profile_image;
        string AuthorFirstName = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
          SetContentView(Resource.Layout.activity_Home);
            animationView = FindViewById<LottieAnimationView>(Resource.Id.animation_view);
            btnStartUp = FindViewById<Button>(Resource.Id.btnStart);
            backbtn = FindViewById<ImageView>(Resource.Id.backbtn);
            txtVersion = FindViewById<TextView>(Resource.Id.txtVersion);
            txtFirstName = FindViewById<TextView>(Resource.Id.txtFirstName);
            txtSurname = FindViewById<TextView>(Resource.Id.txtSurname);
            txtAppIntro = FindViewById<TextView>(Resource.Id.txtAppIntro);
            profile_image = FindViewById<CircleImageView>(Resource.Id.profile_image);
          //  txtVersion.Text = "v " + Application.Context.ApplicationContext.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionName;

             AuthorFirstName = Intent.GetStringExtra("AuthorFirstName");
            var AuthorSurname = Intent.GetStringExtra("AuthorSurname");
            var AuthorIntro = Intent.GetStringExtra("AuthorIntro");

            txtFirstName.Text = AuthorFirstName;
            txtSurname.Text = AuthorSurname;
            txtAppIntro.Text = AuthorIntro;

            if(AuthorFirstName+" "+ AuthorSurname=="Golda Gracias")
            {
                profile_image.SetImageDrawable(GetDrawable(Resource.Drawable.golda));
            }
            else
            {
                profile_image.SetImageDrawable(GetDrawable(Resource.Drawable.curie));
            }


            btnStartUp.Click += async (sender, e) =>
            {
                animationView.PlayAnimation();
                await Task.Delay(800);
                var intent = new Intent(this, typeof(ArcticleListActivity));
                intent.PutExtra("Author", AuthorFirstName);
              
                StartActivity(intent);
            };
            backbtn.Click += delegate
            {
                this.Finish();
            };
        }

        
    }
}

