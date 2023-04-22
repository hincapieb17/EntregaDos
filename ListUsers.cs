using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
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
        Button btnSendUser;
        Button btnUpDate;
        Button btnSave;
        Button btnDeleteUser;
        Button btnShowPassword;
        Button btnCancel;

        EditText idUserName;
        EditText idPassword;
        EditText idCorreoUser;
        EditText userIdHome;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListUsers);
            menu();
            idUserName = FindViewById<EditText>(Resource.Id.idUserName);
            idPassword = FindViewById<EditText>(Resource.Id.idPassword);
            idCorreoUser = FindViewById<EditText>(Resource.Id.idCorreoUser);
            userIdHome = FindViewById<EditText>(Resource.Id.userIdHome);

            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            btnSave.Click += BtnSave_Click;

            btnCancel = FindViewById<Button>(Resource.Id.btnCancel);
            btnCancel.Click += BtnCancel_Click;

            btnSendUser = FindViewById<Button>(Resource.Id.btnSendUser);
            btnSendUser.Click += BtnSendUser_Click;

            btnDeleteUser = FindViewById<Button>(Resource.Id.btnDeleteUser);
            btnDeleteUser.Click += BtnDeleteUser_Click;

            btnUpDate = FindViewById<Button>(Resource.Id.btnUpDate);
            btnUpDate.Click += BtnUpDate_Click;

            Button showPasswordButton = FindViewById<Button>(Resource.Id.btnShowPassword);

            // Agrega un controlador de eventos para el botón
            showPasswordButton.Click += (sender, e) => {
                // Cambia el atributo de texto de la etiqueta de contraseña a "texto visible"
                idPassword.InputType = InputTypes.TextVariationVisiblePassword;
            };

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(userIdHome.Text.Trim()) && !string.IsNullOrEmpty(idUserName.Text.Trim()) && !string.IsNullOrEmpty(idCorreoUser.Text.Trim()) && !string.IsNullOrEmpty(idPassword.Text.Trim()))
            {
                int iduser = Int32.Parse(userIdHome.Text.Trim());

                try
                {
                    new Auxiliar().Save(new User() { Id = iduser, UserName = idUserName.Text.Trim(), Email = idCorreoUser.Text.Trim(), Password = idPassword.Text.Trim() });
                    Toast.MakeText(this, "Updated successfully", ToastLength.Long).Show();

                    var home = new Intent(this, typeof(Home));
                    StartActivity(home);
                    Finish();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "Fill all the fields", ToastLength.Short).Show();
            }
            

            
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            idCorreoUser.Enabled = false;
            idPassword.Enabled = false;


            btnSave.Visibility = ViewStates.Gone;
            btnCancel.Visibility = ViewStates.Gone;

            btnDeleteUser.Visibility = ViewStates.Visible;
            btnUpDate.Visibility = ViewStates.Visible;

            userIdHome.Text = "";
            idUserName.Text = "";
            idCorreoUser.Text = "";
            idPassword.Text = "";

        }


        private void BtnUpDate_Click(object sender, EventArgs e)
        {
            btnSave.Visibility = ViewStates.Visible;
            btnCancel.Visibility = ViewStates.Visible;

            idCorreoUser.Enabled = true;
            idPassword.Enabled = true;

            btnDeleteUser.Visibility = ViewStates.Gone;
            btnUpDate.Visibility = ViewStates.Gone;
        }

        private void BtnDeleteUser_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(userIdHome.Text))
                {
                    int id = Int32.Parse(userIdHome.Text);
                    int validar = new Auxiliar().EliminarPorId(id);

                    if (validar != 0)
                    {
                        Toast.MakeText(this, "User Deleted", ToastLength.Long).Show();

                        idUserName.Text = "";
                        idPassword.Text = "";
                        idCorreoUser.Text = "";
                        userIdHome.Text = "";
                    }
                    else
                    {
                        Toast.MakeText(this, "Error", ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Error. Empty field", ToastLength.Long).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void BtnSendUser_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(idUserName.Text))
                {
                    
                    User aux = new Auxiliar().GetUser(idUserName.Text.Trim());
                    userIdHome.Text = aux.Id.ToString();
                    idCorreoUser.Text = aux.Email;
                    idPassword.Text = aux.Password;
                    Toast.MakeText(this, "Correct search", ToastLength.Long).Show();
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "User not found: " + ex.ToString(), ToastLength.Short).Show();
            }
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