using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace entregaDos
{
    [Activity(Label = "Home")]
    internal class Home : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);
            menu();

            string uriServicio = "https://jsonplaceholder.typicode.com/posts";

            //Text
            EditText idFormHome = FindViewById<EditText>(Resource.Id.idFormHome);

            EditText userIdHome = FindViewById<EditText>(Resource.Id.userIdHome);
            EditText titleHome = FindViewById<EditText>(Resource.Id.titleHome);
            EditText bodyHome = FindViewById<EditText>(Resource.Id.bodyHome);

            //Button
            Button btnAllData = FindViewById<Button>(Resource.Id.btnAllData);
            btnAllData.Click += BtnAllData_Click;

            Button btnSend = FindViewById<Button>(Resource.Id.btnSend);
            btnSend.Click += async (sender, e) =>
            {
                try
                {
                    Cliente cliente = new Cliente();
                    if (!string.IsNullOrWhiteSpace(idFormHome.Text))
                    {
                        int t = 0;
                        if (int.TryParse(idFormHome.Text.Trim(), out t))
                        {
                            var resultado = await cliente.Get<Entrada>(uriServicio + "/" + idFormHome.Text);
                            if (cliente.codigoHTTP == 200)
                            {
                                int id = resultado.userId;
                                userIdHome.Text = id.ToString();
                                titleHome.Text = resultado.title;
                                bodyHome.Text = resultado.body;
                                Toast.MakeText(this, "Consulta realizada con éxito", ToastLength.Long).Show();
                            }

                        }
                    }

                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "Error: " + ex.Message, ToastLength.Long).Show();
                }
            };

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

        private void BtnAllData_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(DataWeb));
            StartActivity(i);
            
        }
    }
}