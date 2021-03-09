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
    public class Arcticle
    {
        public string ID { get; set; }
      public string  ArcticleName  {get;set;}
        public string Author { get; set; }
        public string PublishDate { get; set; }
        public string ArcticleDesc { get; set; }
        public string DownloadUrl { get; set; }
    }
}