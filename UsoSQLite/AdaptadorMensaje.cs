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
    class AdaptadorMensaje : BaseAdapter<Mensaje>
    {
        private Context context;
        private List<Mensaje> listaMensajes;

        public AdaptadorMensaje(Context context, List<Mensaje> data)
        {
            this.context = context;
            this.listaMensajes = data;
        }

        public override Mensaje this[int position] {
            get { return listaMensajes[position]; }
        }

        public override int Count
        {
            get { return listaMensajes.Count; } 
        }

        public override long GetItemId(int position)
        {
            return listaMensajes[position].id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Mensaje objMensaje = listaMensajes[position];
            //if( convertView == null)  
            //{
                var inflater = LayoutInflater.From(context);
                if (objMensaje.recibido)
                {
                    convertView = inflater.Inflate(Resource.Layout.MensajeRecibido, parent, false);
                    TextView txtUsuario = convertView.FindViewById<TextView>(Resource.Id.txtUsuarioRecibido);
                    txtUsuario.Text = objMensaje.usuario;
                    TextView txtMensaje = convertView.FindViewById<TextView>(Resource.Id.txtMensajeRecibido);
                    txtMensaje.Text = objMensaje.mensaje;
                }
                else
                {
                    convertView = inflater.Inflate(Resource.Layout.MensajeEnviado, parent, false);
                    TextView txtUsuario = convertView.FindViewById<TextView>(Resource.Id.txtUsuario);
                    txtUsuario.Text = objMensaje.usuario;
                    TextView txtMensaje = convertView.FindViewById<TextView>(Resource.Id.txtMensaje);
                    txtMensaje.Text = objMensaje.mensaje;
                }
            //}
            return convertView;
        }
    }
}