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

namespace UsoSQLite
{
    public class Datos
    {
        public string type { get; set; }
        public string usuario { get; set; }
        public string contenido { get; set; }
        public Datos(string type, string usuario, string contenido)
        {
            this.type = type;
            this.usuario = usuario;
            this.contenido = contenido;
        }
    }

    public class MyEventArgsRecibirMensaje
    {
        public Datos mensaje { get; set; }
        //public string mensaje { get; set; }

        public MyEventArgsRecibirMensaje(Datos msg)
        //public MyEventArgsRecibirMensaje(string msg)
        {
            mensaje = msg;
        }
    }

    public delegate void MyEventHandlerRecibirMensaje(object sender, MyEventArgsRecibirMensaje e);
    interface IRecibirMensaje
    {
        event MyEventHandlerRecibirMensaje myEventHandlerRecibirMensaje;
    }
}