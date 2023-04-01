using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace entregaDos
{
    [Activity(Label = "ListUsers")]
    internal class ListUsers : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListUsers);
            menu();
        }

        private void menu()
        {
            BottomNavigationView navigationMenu = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigationMenu.NavigationItemSelected += (sender, e) =>
            {

                switch (e.Item.ItemId)
                {
                    case Resource.Id.idHome:
                        Intent i = new Intent(this, typeof(Home));
                        StartActivity(i);
                        Finish();
                        break;

                    case Resource.Id.idForm:
                        Intent a = new Intent(this, typeof(ListUsers));
                        StartActivity(a);
                        Finish();
                        break;

                    case Resource.Id.idExit:
                        Intent o = new Intent(this, typeof(MainActivity));
                        StartActivity(o);
                        Finish();
                        break;

                }
            };

        }
    }
}