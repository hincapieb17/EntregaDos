using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entregaDos
{
    [Activity(Label = "DataWeb")]
    internal class DataWeb : Activity
    {
        Button btnBackWeb;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DataWeb);
            ObtenerDatos();

            btnBackWeb = FindViewById<Button>(Resource.Id.btnBackWeb);
            btnBackWeb.Click += BtnBackWeb;

        }

        private void BtnBackWeb(object sender, System.EventArgs e)
        {
            Intent i = new Intent(this, typeof(Home));
            StartActivity(i);
            Finish();
        }

        public async Task ObtenerDatos()
        {
            Cliente cliente = new Cliente();
            // Crear la lista de entradas
            List<Entrada> entradas = await cliente.Get<List<Entrada>>("https://jsonplaceholder.typicode.com/posts");
            List<EntradaViewModel> entradaViewModels = entradas.Select(e => new EntradaViewModel { Title = e.title, Body = e.body }).ToList();

            // Obtener la referencia al ListView
            ListView listView = FindViewById<ListView>(Resource.Id.listview);

            // Crear el adaptador de lista y asignarlo al ListView
            ArrayAdapter<EntradaViewModel> adapter = new ArrayAdapter<EntradaViewModel>(this, Android.Resource.Layout.SimpleListItem1, entradaViewModels);
            listView.Adapter = adapter;
        }

        public class EntradaViewModel
        {
            public string Title { get; set; }
            public string Body { get; set; }

            public override string ToString()
            {
                return $"TITLE: {Title} \n BODY:{Body}";
            }
        }



    }
}