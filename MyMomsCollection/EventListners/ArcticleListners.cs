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
using Firebase.Database;
using MyMomsCollection.Helpers;
using MyMomsCollection.Models;

namespace MyMomsCollection.EventListners
{
    public class ArcticleListners : Java.Lang.Object, IValueEventListener
    {
        List<Arcticle> lstArcticle = new List<Arcticle>();

        public event EventHandler<ArcticleDataEventArgs> ArticleRetrieved;

        public class ArcticleDataEventArgs:EventArgs
        {
            public List<Arcticle> Arcticle { get; set; }

        }

        public void OnCancelled(DatabaseError error)
        {
           
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Value != null)
            {
                var child = snapshot.Children.ToEnumerable<DataSnapshot>();
                lstArcticle.Clear();
                
                foreach (DataSnapshot ArcticleData in child)
                {
                    Arcticle oArcticle = new Arcticle();
                    oArcticle.ID = ArcticleData.Key;
                    oArcticle.ArcticleName = ArcticleData.Child("ArcticleName").Value.ToString();
                    oArcticle.Author = ArcticleData.Child("Author").Value.ToString();
                    oArcticle.PublishDate = ArcticleData.Child("PublishDate").Value.ToString();
                    oArcticle.ArcticleDesc = ArcticleData.Child("ArcticleDesc").Value.ToString();
                    oArcticle.DownloadUrl = ArcticleData.Child("DownloadUrl").Value.ToString();
                    lstArcticle.Add(oArcticle);
                }
                ArticleRetrieved.Invoke(this, new ArcticleDataEventArgs { Arcticle = lstArcticle });
            }
            
        }

        public void Create()
        {
            DatabaseReference ArcticleRef = AppDataHelper.GetDatabase().GetReference("Arcticle");
            ArcticleRef.AddValueEventListener(this);
        }
    }
}