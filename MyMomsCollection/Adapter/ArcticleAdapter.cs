using System;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using MyMomsCollection.Models;
using Android.Content;
using MyMomsCollection.Interface;

namespace MyMomsCollection.Adapter
{
    class ArcticleAdapter : RecyclerView.Adapter, IItemClickListener
    {
       // public event EventHandler<ArcticleAdapterClickEventArgs> ItemClick;
       // public event EventHandler<ArcticleAdapterClickEventArgs> ItemLongClick;
        private Context context;
        List<Arcticle> Items;
        public ArcticleAdapter(List<Arcticle> data, Context context)
        {
            Items = data;
            this.context = context;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ArticleListTemplate, parent, false);
            // var vh = new ArcticleAdapterViewHolder(itemView, OnClick, OnLongClick);
            // return vh;
            ArcticleAdapterViewHolder vh = new ArcticleAdapterViewHolder(itemView);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            //var item = Items[position];
            ArcticleAdapterViewHolder vh = viewHolder as ArcticleAdapterViewHolder;
            vh.SetItemClickListener(this);
            // Replace the contents of the view with that element
            var holder = viewHolder as ArcticleAdapterViewHolder;
            holder.txtArcticleName.Text = Items[position].ArcticleName;
            holder.txtArcticleAuthor.Text = Items[position].Author;
            holder.txtArcticleDate.Text = Items[position].PublishDate;
        }

        public override int ItemCount => Items.Count;

        // void OnClick(ArcticleAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        // void OnLongClick(ArcticleAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
        public void OnClick(View v, object adapterPosition)
        {
            int _position = (int)adapterPosition;
            //ChefsModel objChef = ChefsModel._lstChefsModel[_position];
            Arcticle arcticle= Items[_position];
            // Settings.HomeView.NavigateToMain = false;  // SJ 01/07/2019 If Dish Description Visited then, on Back Pressed Navigates to MyOrderDetails.
            Intent intent = new Intent(context, typeof(ArcticleDescriptionActivity));
            intent.PutExtra("ArcticleName", arcticle.ArcticleName.ToString());
            intent.PutExtra("ArcticleDesc", arcticle.ArcticleDesc.ToString());
            intent.PutExtra("DownloadUrl", arcticle.DownloadUrl.ToString());
            context.StartActivity(intent);

        }
    }

    class ArcticleAdapterViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
    {
        public TextView txtArcticleName { get; set; }
        public TextView txtArcticleAuthor { get; set; }
        public TextView txtArcticleDate { get; set; }
        private IItemClickListener itemClickListener;
        public ArcticleAdapterViewHolder(View itemView) : base(itemView)
        {
            //TextView = v;

            txtArcticleName = (TextView)itemView.FindViewById(Resource.Id.txtArcticleName);
            txtArcticleAuthor = (TextView)itemView.FindViewById(Resource.Id.txtArcticleAuthor);
            txtArcticleDate = (TextView)itemView.FindViewById(Resource.Id.txtArcticleDate);
            // itemView.Click += (sender, e) => clickListener(new ArcticleAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            // itemView.LongClick += (sender, e) => longClickListener(new ArcticleAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.SetOnClickListener(this);
        }
        public void SetItemClickListener(IItemClickListener itemClickListener)
        {
            this.itemClickListener = itemClickListener;
        }

        public void OnClick(View v)
        {
            //if (Settings.HomeView.IsListItemClicked)
            //    return;
            //Settings.HomeView.IsListItemClicked = true;

            itemClickListener.OnClick(v, AdapterPosition);
        }
    }

    //public class ArcticleAdapterClickEventArgs : EventArgs
    //{
    //    public View View { get; set; }
    //    public int Position { get; set; }
    //}
}