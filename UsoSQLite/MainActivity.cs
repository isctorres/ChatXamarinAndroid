using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

using SQLite;
using System.IO;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using UsoSQLite.Modelo;
using SQLiteNetExtensions.Extensions;
using Android.Preferences;
using Android.Content;
using Android.Support.Design.Widget;
using System;
using Android.Views;

namespace UsoSQLite
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        private FloatingActionButton fabEnviar;
        private EditText edtMensaje;
        private ListView ltvMensajes;
        private List<Mensaje> lstMensajes;
        private string pathBaseDeDatos;
        private Android.Content.ISharedPreferences preferences;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            fabEnviar = FindViewById<FloatingActionButton>(Resource.Id.fabEnviar);
            edtMensaje = FindViewById<EditText>(Resource.Id.edtMensaje);
            ltvMensajes = FindViewById<ListView>(Resource.Id.ltvMensajes);

            // Instanciamos el objeto de las preferencias para poder recuperar los datos almacenados en las preferencias
            preferences = GetSharedPreferences("PreferenciasUsuario", Android.Content.FileCreationMode.Private);

            pathBaseDeDatos = Path.Combine(FilesDir.AbsolutePath, "mensajes.sqlite");
            lstMensajes = new List<Mensaje>();

            initList();
            fabEnviar.Click += (sender, e) => insMensaje();
            
            ltvMensajes.ItemLongClick += (sender, e) =>
            {
                var objMensaje = lstMensajes[e.Position];

                FragmentTransaction ft = FragmentManager.BeginTransaction();
                //Remove fragment else it will crash as it is already added to backstack
                Fragment prev = FragmentManager.FindFragmentByTag("dialog");
                if (prev != null)
                {
                    ft.Remove(prev);
                }

                ft.AddToBackStack(null);

                // Create and show the dialog.
                DetalleMensajeFragment newFragment = new DetalleMensajeFragment(objMensaje);

                //Add fragment
                newFragment.Show(ft, "dialog");
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_chat, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return base.OnOptionsItemSelected(item);
        }

        private void initList()
        {
            SQLiteConnection connection = new SQLiteConnection(pathBaseDeDatos);
            using (connection)
            {
                //connection.DropTable<Mensaje>();
                connection.CreateTable<Mensaje>();

                if (connection.Table<Mensaje>().Count() == 0)
                {
                    List<Mensaje> mensajes = new List<Mensaje> {
                        new Mensaje {
                            usuario = "Luis", mensaje="hola como estas?", recibido=true, fecha = DateTime.Now
                        },
                        new Mensaje {
                            usuario = "Pedro", mensaje="bien y tu que tal, que cuentas?", recibido=false, fecha = DateTime.Now
                        },
                        new Mensaje {
                            usuario = "Luis", mensaje="hola 2", recibido=true, fecha = DateTime.Now
                        },
                        new Mensaje {
                            usuario = "Pedro", mensaje="hola 2", recibido=false, fecha = DateTime.Now
                        },
                        new Mensaje {
                            usuario = "Luis", mensaje="hola 3", recibido=true, fecha = DateTime.Now
                        },
                        new Mensaje {
                            usuario = "Pedro", mensaje="hola 3", recibido=false, fecha = DateTime.Now
                        },
                        new Mensaje {
                            usuario = "Luis", mensaje="hola 4", recibido=true, fecha = DateTime.Now
                        },
                        new Mensaje {
                            usuario = "Pedro", mensaje="hola 4", recibido=false, fecha = DateTime.Now
                        },
                        new Mensaje {
                            usuario = "Luis", mensaje="hola 5", recibido=true, fecha = DateTime.Now
                        },
                        new Mensaje {
                            usuario = "Pedro", mensaje="hola 5", recibido=false, fecha = DateTime.Now
                        },
                    };
                    connection.InsertAll(mensajes);
                }

                lstMensajes = connection.Table<Mensaje>().ToList();
                ltvMensajes.Adapter = new AdaptadorMensaje(this, lstMensajes);
                connection.Close();
                connection.Dispose();
            }
            ltvMensajes.SetSelection(ltvMensajes.Adapter.Count - 1);

        }

        private void insMensaje()
        {
            SQLiteConnection connection = new SQLiteConnection(pathBaseDeDatos);
            using (connection)
            {
                Mensaje newMensaje = new Mensaje        // Construimos el objeto de tipo mensaje que insertaremos
                {                                       // en la tabla Mensaje, para actualizar el chat
                    usuario = preferences.GetString("Usuario","PiriGuapi"),
                    mensaje = edtMensaje.Text,
                    recibido = false
                };

                edtMensaje.Text = "";                   // Limpiamos la caja de texto
                connection.Insert(newMensaje);          // Insertamos el objeto de tipo mensaje creado previamente
                lstMensajes = connection.Table<Mensaje>().ToList();     // Listamos todos los registros de la tabla
            }
            
            ltvMensajes.Adapter = new AdaptadorMensaje(this, lstMensajes);
            ltvMensajes.SetSelection(ltvMensajes.Adapter.Count - 1);
            connection.Close();
            connection.Dispose();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}