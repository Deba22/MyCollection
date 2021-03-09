using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Hanks.Library.Bang;
using Java.Lang;
using MyMomsCollection.Models;

namespace MyMomsCollection.Adapter
{
    public class CarouselAdapter : PagerAdapter
    {
        List<CarouselModel> listImages;
        Context context;
        LayoutInflater layoutInflater;
        
        public CarouselAdapter(List<CarouselModel> listImages,Context context)
        {
            this.listImages = listImages;
            this.context = context;
            layoutInflater = LayoutInflater.From(context);
        }


        public  override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            View view = layoutInflater.Inflate(Resource.Layout.Card_Item, container, false);
            ImageView imageView = view.FindViewById<ImageView>(Resource.Id.imageView);
            TextView authorName = view.FindViewById<TextView>(Resource.Id.authorName);
            SmallBangView smallbang = view.FindViewById<SmallBangView>(Resource.Id.like_text);
            imageView.SetImageResource(listImages[position].Image);
            authorName.Text = listImages[position].FirstName+" "+ listImages[position].Surname;

            authorName.Click += async delegate
            {

                var intent = new Intent(context, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.NewTask);
                smallbang.LikeAnimation();
                smallbang.Selected = true;
                if (listImages[position].FirstName + " " + listImages[position].Surname == "Golda Gracias")
                {
                    intent.PutExtra("AuthorFirstName", listImages[position].FirstName.ToString());
                    intent.PutExtra("AuthorSurname", listImages[position].Surname.ToString());
                    intent.PutExtra("AuthorIntro", "My name is Golda Gracias and I live in the beautiful village of Cansaulim located in Goa. I am an amateur writer, and this is ‘My Collection’ of various articles written in Roman Konkani. Get set to dive into an extensive range of realistic topics with captivating content. Happy Reading!!!");
                }
                else
                {

                    intent.PutExtra("AuthorFirstName", listImages[position].FirstName.ToString());
                    intent.PutExtra("AuthorSurname", listImages[position].Surname.ToString());
                    intent.PutExtra("AuthorIntro", "I am Curie Pereira born and brought up amidst the scenically beautiful land of Goa.But destiny took me miles away from it and now I am a resident of vadodara, Gujarat.After my 22 years of teaching in a general school now God has blessed me to serve the special children.Writing has been my passion and the wonderful experiences of the teacher student relationship has been the driving force behind my every story.So enjoy my subtle creations as they gently unfold themselves.");
                }

                await Task.Delay(350);
                context.StartActivity(intent);
            };
            container.AddView(view);
            return view;
        }

        public override int Count => listImages.Count;

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view.Equals(@object);
        }

       
        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            container.RemoveView((View)@object);
        }
    }
}