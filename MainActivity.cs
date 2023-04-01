using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace entregaDos
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button btnLogin;
        Button btnSignUp;

        EditText txtUserName;
        EditText txtPassword;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            btnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            btnSignUp.Click += BtnSignUp;

            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnLogin.Click += BtnLogin;


        }

        private void BtnSignUp(object sender, System.EventArgs e)
        {
            Intent i = new Intent(this, typeof(SignUp));
            StartActivity(i);
        }

        private void BtnLogin(object sender, System.EventArgs e)
        {
            txtUserName = FindViewById<EditText>(Resource.Id.txtUserName);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);

            try
            {
                User resultado = null;
                if (!string.IsNullOrEmpty(txtUserName.Text.Trim()) && !string.IsNullOrEmpty(txtPassword.Text.Trim()) )
                {
                    resultado = new Auxiliar().SelecionarUno(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    if (resultado != null)
                    {
                        txtUserName.Text = resultado.UserName.ToString();
                        Toast.MakeText(this, "successful login", ToastLength.Short).Show();

                        var home = new Intent(this, typeof(Home));
                        home.PutExtra("User", FindViewById<EditText>(Resource.Id.txtUserName).Text);
                        StartActivity(home);
                        Finish();

                    }
                    else
                    {
                        Toast.MakeText(this, "Invalid username and/or password", ToastLength.Long).Show();

                    }
                }else
                {
                    Toast.MakeText(this, "username and/or password are empty", ToastLength.Long).Show();
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error: " + ex.Message, ToastLength.Long).Show();
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}