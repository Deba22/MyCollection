using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyMomsCollection.Models
{
   public class CarouselModel
    {
        public string FirstName { get; set; }
        public string Surname  { get; set; }
        public int Image { get; set; }
        public CarouselModel(string FirstName,string Surname, int Image)
        {
            this.FirstName = FirstName;
            this.Surname = Surname;
            this.Image = Image;
        }
    }

  
}