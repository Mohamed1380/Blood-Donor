using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Blood_Donor.DataModels;

namespace Blood_Donor.Adapters
{
    class DonorsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<DonorsAdapterClickEventArgs> ItemClick;
        public event EventHandler<DonorsAdapterClickEventArgs> ItemLongClick;
        public event EventHandler<DonorsAdapterClickEventArgs> CallClick;
        public event EventHandler<DonorsAdapterClickEventArgs> EmailClick;
        public event EventHandler<DonorsAdapterClickEventArgs> DeleteClick;

        List<Donor> DonorsList;

        public DonorsAdapter(List<Donor> data)
        {
           DonorsList = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.donor_row, parent, false);

            var vh = new DonorsAdapterViewHolder(itemView, OnClick, OnLongClick, OnCallClick, OnEmailClick, OnDeletClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var donor = DonorsList[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as DonorsAdapterViewHolder;
            holder.donorNameTextView.Text = donor.Fullname;
            holder.donorLocationTextView.Text = donor.City + ", " + donor.Country;

            // Assign appropriate Images to Donors Blood Group
            if(donor.BloodGroup == "O+")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.o_ppositive);
            }
            else if (donor.BloodGroup == "O-")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.o_negative);
            }
            else if (donor.BloodGroup == "AB-")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.ab_negative);
            }
            else if (donor.BloodGroup == "AB+")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.ab_positive);
            }
            else if (donor.BloodGroup == "B-")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.b_negative);
            }
            else if (donor.BloodGroup == "B+")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.b_positive);
            }
            else if (donor.BloodGroup == "A-")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.a_negative);
            }
            else if (donor.BloodGroup == "A+")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.a_positive);
            }

        }

        public override int ItemCount => DonorsList.Count;

        void OnClick(DonorsAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(DonorsAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

        void OnCallClick(DonorsAdapterClickEventArgs args) => CallClick?.Invoke(this, args);
        void OnEmailClick(DonorsAdapterClickEventArgs args) => EmailClick?.Invoke(this, args);
        void OnDeletClick(DonorsAdapterClickEventArgs args) => DeleteClick?.Invoke(this, args);
        

    }

    public class DonorsAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }

        public TextView donorNameTextView;
        public TextView donorLocationTextView;
        public ImageView bloodGroupImageView;
        public RelativeLayout callLayout;
        public RelativeLayout emailLayout;
        public RelativeLayout deleteLayout;

        public DonorsAdapterViewHolder(View itemView, Action<DonorsAdapterClickEventArgs> clickListener,
                            Action<DonorsAdapterClickEventArgs> longClickListener, Action<DonorsAdapterClickEventArgs> callClickListener,
                            Action<DonorsAdapterClickEventArgs> emailClickListener, Action<DonorsAdapterClickEventArgs> deleteClickListener) : base(itemView)
        {
            //TextView = v;
            donorNameTextView = (TextView)itemView.FindViewById(Resource.Id.donorNameTextView);
            donorLocationTextView = (TextView)itemView.FindViewById(Resource.Id.donorLocationTextView);
            bloodGroupImageView = (ImageView)itemView.FindViewById(Resource.Id.bloodGroupImageView);
            callLayout = (RelativeLayout)itemView.FindViewById(Resource.Id.callLayout);
            emailLayout = (RelativeLayout)itemView.FindViewById(Resource.Id.emailLayout);
            deleteLayout = (RelativeLayout)itemView.FindViewById(Resource.Id.deleteLayout);

            itemView.Click += (sender, e) => clickListener(new DonorsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new DonorsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            callLayout.Click += (sender, e) => callClickListener(new DonorsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            emailLayout.Click += (sender, e) => emailClickListener(new DonorsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            deleteLayout.Click += (sender, e) => deleteClickListener(new DonorsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });

        }
    }

    public class DonorsAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}