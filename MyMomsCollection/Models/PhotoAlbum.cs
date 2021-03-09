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
   public class PhotoAlbum
    {
        static Photo[] mBuiltInPhotos = {
            new Photo { mPhotoID = Resource.Drawable.golda,
                        mCaption = "Buckingham Palace" },
            new Photo { mPhotoID = Resource.Drawable.golda,
                        mCaption = "The Eiffel Tower" },
          
            new Photo { mPhotoID = Resource.Drawable.golda,
                        mCaption = "Arc de Triomphe" },
            new Photo { mPhotoID = Resource.Drawable.golda,
                        mCaption = "Inside the Louvre" },
            new Photo { mPhotoID = Resource.Drawable.golda,
                        mCaption = "Versailles fountains" },
            new Photo { mPhotoID = Resource.Drawable.golda,
                        mCaption = "Modest accomodations" },
            new Photo { mPhotoID = Resource.Drawable.golda,
                        mCaption = "Notre Dame" },
            new Photo { mPhotoID = Resource.Drawable.golda,
                        mCaption = "Inside Notre Dame" },
            new Photo { mPhotoID = Resource.Drawable.golda,
                        mCaption = "The Seine" },
            new Photo { mPhotoID = Resource.Drawable.golda,
                        mCaption = "Rue Cler" },
            
            new Photo { mPhotoID = Resource.Drawable.golda,
                        mCaption = "Portcullis Gate" },
            new Photo { mPhotoID = Resource.Drawable.golda,
                        mCaption = "Left or right?" },
            new Photo { mPhotoID = Resource.Drawable.golda,
                        mCaption = "Pompidou Centre" },
            new Photo { mPhotoID = Resource.Drawable.golda,
                        mCaption = "Here's Lookin' at Ya!" },
            };



        // Array of photos that make up the album:
        private Photo[] mPhotos;

        // Random number generator for shuffling the photos:
        Random mRandom;
        // Create an instance copy of the built-in photo list and
        // create the random number generator:
        public PhotoAlbum()
        {
            mPhotos = mBuiltInPhotos;
            mRandom = new Random();
        }

        // Return the number of photos in the photo album:
        public int NumPhotos
        {
            get { return mPhotos.Length; }
        }

        // Indexer (read only) for accessing a photo:
        public Photo this[int i]
        {
            get { return mPhotos[i]; }
        }
    }
    // Photo: contains image resource ID and caption:
    public class Photo
    {
        // Photo ID for this photo:
        public int mPhotoID;

        // Caption text for this photo:
        public string mCaption;

        // Return the ID of the photo:
        public int PhotoID
        {
            get { return mPhotoID; }
        }

        // Return the Caption of the photo:
        public string Caption
        {
            get { return mCaption; }
        }
    }
}