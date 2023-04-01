using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace entregaDos
{
    [Activity(Label = "SignUp" )]
    internal class SignUp : Activity
    {
        Button btnBack;
        Button btnSUSignUp;

        EditText txtSUSuerName;
        EditText txtSUSuerEmail;
        EditText txtSUPassword;
        EditText txtSUCPassword;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SignUp);
            
            //Button back
            btnBack = FindViewById<Button>(Resource.Id.btnBack);
            btnBack.Click += BtnBack_Click;

            btnSUSignUp = FindViewById<Button>(Resource.Id.btnSUSignUp);
            btnSUSignUp.Click += BtnSUSignUp_Click;
        }

        private void BtnSUSignUp_Click(object sender, System.EventArgs e)
        {
            if (Validate_Data())
            {
                txtSUSuerName = FindViewById<EditText>(Resource.Id.txtSUSuerName);
                txtSUPassword = FindViewById<EditText>(Resource.Id.txtSUPassword);
                txtSUSuerEmail = FindViewById<EditText>(Resource.Id.txtSUEmail);
                try
                {
                    new Auxiliar().Save(new User() { Id = 0, UserName = txtSUSuerName.Text.Trim(), Email = txtSUSuerEmail.Text.Trim(), Password = txtSUPassword.Text.Trim() });
                    Toast.MakeText(this, "Registered Successfully", ToastLength.Long).Show();

                    var main = new Intent(this, typeof(MainActivity));
                    StartActivity(main);
                    Finish();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
                }
            }

        }

        public Boolean Validate_Data()
        {
            txtSUSuerName = FindViewById<EditText>(Resource.Id.txtSUSuerName);
            txtSUPassword = FindViewById<EditText>(Resource.Id.txtSUPassword);
            txtSUCPassword = FindViewById<EditText>(Resource.Id.txtSUCPassword);
            txtSUSuerEmail = FindViewById<EditText>(Resource.Id.txtSUEmail);

            if (string.IsNullOrEmpty(txtSUSuerName.Text.Trim()))
            {
                Toast.MakeText(this, "Error: Please enter a valid user name" , ToastLength.Long).Show();
            }
            else if (string.IsNullOrEmpty(txtSUSuerEmail.Text.Trim()))
            {
                Toast.MakeText(this, "Error: Please enter a valid user email", ToastLength.Long).Show();   
            }
            else if (string.IsNullOrEmpty(txtSUPassword.Text.Trim()))
            {
                Toast.MakeText(this, "Error: Please enter a valid password", ToastLength.Long).Show();
            }
            else if (string.IsNullOrEmpty(txtSUCPassword.Text.Trim()))
            {
                Toast.MakeText(this, "Error:Please enter a valid confirm password", ToastLength.Long).Show();
            }
            else
            {
                string passwordOne = txtSUCPassword.Text.Trim();
                string passwordTwo = txtSUPassword.Text.Trim();

                if (passwordOne == passwordTwo)
                {
                    return true;
                }
                else
                {
                    Toast.MakeText(this, "Error: Passwords do not match", ToastLength.Long).Show();
                }

            }
            return false;
        }

        private void BtnBack_Click(object sender, System.EventArgs e)
        {
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }


    }
}