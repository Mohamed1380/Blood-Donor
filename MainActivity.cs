﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using Blood_Donor.Adapters;
using System.Collections.Generic;
using Blood_Donor.DataModels;
using Android.Content;
using Android.Support.Design.Widget;
using Blood_Donor.Fragments;

namespace Blood_Donor
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        RecyclerView donorsRecyclerView;
        DonorsAdapter donorsAdapter;
        List<Donor> listOfDonors;
        NewDonorFragment newDonorFragment;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SupportActionBar.Title = "Blood Donors";
            donorsRecyclerView = (RecyclerView)FindViewById(Resource.Id.donorsRecyclerView);
            FloatingActionButton fab = (FloatingActionButton)FindViewById(Resource.Id.fab);
            fab.Click += Fab_Click;
            CreateData();
            SetupRecyclerView();
        }

        private void Fab_Click(object sender, System.EventArgs e)
        {
            newDonorFragment = new NewDonorFragment();
            var trans = SupportFragmentManager.BeginTransaction();
            newDonorFragment.Show(trans, "new donor");
            newDonorFragment.OnDonorRegistered += NewDonorFragment_OnDonorRegistered;
        }

        private void NewDonorFragment_OnDonorRegistered(object sender, NewDonorFragment.DonorDetailsEventArgs e)
        {
            if(newDonorFragment != null)
            {
                newDonorFragment.Dismiss();
                newDonorFragment = null;
            }
            listOfDonors.Insert(0, e.Donor);
            donorsAdapter.NotifyItemInserted(0);
        }

        void CreateData()
        {
            listOfDonors = new List<Donor>();
            listOfDonors.Add(new Donor { BloodGroup = "AB+", City = "Delaware", Country = "USA", Email = "ufinixacademy@gmail.com", Fullname = "Ufinix Academy", Phone = "+01 76376 883" });
            listOfDonors.Add(new Donor { BloodGroup = "O+", City = "Munich", Country = "Germany", Email = "dechosenuchenna@gmail.com", Fullname = "Uchenna Nnodim", Phone = "889289828" });
            listOfDonors.Add(new Donor { BloodGroup = "O-", City = "Lagos", Country = "Nigeria", Email = "Uzoma@gmail.com", Fullname = "Uzoma Anthony", Phone = "749892897" });
           
        }

        void SetupRecyclerView()
        {
            donorsRecyclerView.SetLayoutManager(new LinearLayoutManager(donorsRecyclerView.Context));
            donorsAdapter = new DonorsAdapter(listOfDonors);
            donorsAdapter.ItemClick += DonorsAdapter_ItemClick;
            donorsAdapter.CallClick += DonorsAdapter_CallClick;
            donorsAdapter.EmailClick += DonorsAdapter_EmailClick;
            donorsAdapter.DeleteClick += DonorsAdapter_DeleteClick;
            donorsRecyclerView.SetAdapter(donorsAdapter);
        }

        private void DonorsAdapter_DeleteClick(object sender, DonorsAdapterClickEventArgs e)
        {
            var donor = listOfDonors[e.Position];
            Android.Support.V7.App.AlertDialog.Builder DeletAlert = new Android.Support.V7.App.AlertDialog.Builder(this);
            DeletAlert.SetMessage("Are you sure");
            DeletAlert.SetTitle("Delete Donor");

            DeletAlert.SetPositiveButton("Delete", (alert, args) =>
            {
                listOfDonors.RemoveAt(e.Position);
                donorsAdapter.NotifyItemRemoved(e.Position);
            });

            DeletAlert.SetNegativeButton("Cancel", (alert, args) =>
            {
                DeletAlert.Dispose();
            });

            DeletAlert.Show();

        }

        private void DonorsAdapter_EmailClick(object sender, DonorsAdapterClickEventArgs e)
        {

            var donor = listOfDonors[e.Position];
            Android.Support.V7.App.AlertDialog.Builder EmailAlert = new Android.Support.V7.App.AlertDialog.Builder(this);
            EmailAlert.SetMessage("Send Mail to " + donor.Fullname);

            EmailAlert.SetPositiveButton("Send", (alert, args) =>
            {
                // Send Email
                Intent intent = new Intent();
                intent.SetType("plain/text");
                intent.SetAction(Intent.ActionSend);
                intent.PutExtra(Intent.ExtraEmail, new string[] { donor.Email });
                intent.PutExtra(Intent.ExtraSubject, "Enquiry on your availability for blood donation");
                StartActivity(intent);
            });

            EmailAlert.SetNegativeButton("Cancel", (alert, args) =>
            {
                EmailAlert.Dispose();
            });

            EmailAlert.Show();
        }

        private void DonorsAdapter_CallClick(object sender, DonorsAdapterClickEventArgs e)
        {
            var donor = listOfDonors[e.Position];

            Android.Support.V7.App.AlertDialog.Builder CallAlert = new Android.Support.V7.App.AlertDialog.Builder(this);
            CallAlert.SetMessage("Call " + donor.Fullname);

            CallAlert.SetPositiveButton("Call", (alert, args) =>
            {
                var uri = Android.Net.Uri.Parse("tel:" + donor.Phone);
                var intent = new Intent(Intent.ActionDial, uri);
                StartActivity(intent);
            });

            CallAlert.SetNegativeButton("Cancel", (alert, args) =>
            {
                CallAlert.Dispose();
            });

            CallAlert.Show();
        }

        private void DonorsAdapter_ItemClick(object sender, DonorsAdapterClickEventArgs e)
        {
            Toast.MakeText(this, "Row was clicked", ToastLength.Short).Show();

        }
    }
}