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
using UsoSQLite.Modelo;

namespace UsoSQLite
{
    class DetalleMensajeFragment : DialogFragment
    {
        private Mensaje objMensaje;
        public DetalleMensajeFragment(Mensaje objMsj)
        {
            objMensaje = objMsj;
            //DetalleMensajeFragment fragment = new DetalleMensajeFragment();
            //return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.layout_detalle, container, false);
            TextView txtUsuario = view.FindViewById<TextView>(Resource.Id.txtUsuario);
            TextView txtFecha   = view.FindViewById<TextView>(Resource.Id.txtFecha);
            TextView txtHora    = view.FindViewById<TextView>(Resource.Id.txtHora);
            TextView txtMensaje = view.FindViewById<TextView>(Resource.Id.txtMensaje);

            txtUsuario.Text = objMensaje.usuario;
            txtFecha.Text = objMensaje.fecha.Date.ToString();
            txtHora.Text = objMensaje.fecha.TimeOfDay.ToString();
            txtMensaje.Text = objMensaje.mensaje;

            Button button = view.FindViewById<Button>(Resource.Id.CloseButton);
            button.Click += delegate {
                Dismiss();
            };

            return view;
        }
    }
}