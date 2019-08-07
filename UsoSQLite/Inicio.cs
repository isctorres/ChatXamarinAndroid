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
using Android.Support.V7.App;

namespace UsoSQLite
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class Inicio : AppCompatActivity
    {
        EditText edtUsuario;
        Button btnEntrar;
        Android.Content.ISharedPreferences preferences;
        string preferenciaUsuario;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_inicio);
            // Create your application here

            edtUsuario = FindViewById<EditText>(Resource.Id.edtUsuario);
            btnEntrar = FindViewById<Button>(Resource.Id.btnEntrar);

            preferences = GetSharedPreferences("PreferenciasUsuario", Android.Content.FileCreationMode.Private);
            preferenciaUsuario = preferences.GetString("Usuario", "PiriGuapi");
            edtUsuario.Text = preferenciaUsuario;

            btnEntrar.Click += (sender, e) =>
            {
                Android.Content.ISharedPreferencesEditor editor = preferences.Edit();
                editor.PutString("Usuario", edtUsuario.Text);
                editor.Commit();
                var objIntent = new Intent(this, typeof(MainActivity));
                StartActivity(objIntent);
            };
        }
    }
}