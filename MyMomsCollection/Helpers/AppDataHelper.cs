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
using Firebase;
using Firebase.Database;

namespace MyMomsCollection.Helpers
{
   public static class AppDataHelper
    {
        public static FirebaseDatabase GetDatabase()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseDatabase database;
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetApplicationId("mymomscollection-876ef")
                    .SetApiKey("AIzaSyBc8ekpNapVJcpl8svRTqmZFmpaIbJjUD8")
                    .SetDatabaseUrl("https://mymomscollection-876ef.firebaseio.com")
                    .SetStorageBucket("mymomscollection-876ef.appspot.com")
                    .Build();
                app = FirebaseApp.InitializeApp(Application.Context, options);
                database = FirebaseDatabase.GetInstance(app);
            }
            else
            {
                database = FirebaseDatabase.GetInstance(app);
            }
            return database;
          //  DatabaseReference dbref = database.GetReference("Test");
           // dbref.SetValue("testval");
        }
    }
}