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


using SQLite;
using SQLiteNetExtensions.Attributes;

namespace UsoSQLite.Modelo
{
    class Mensaje
    {
        [AutoIncrement, PrimaryKey, Column("id")]
        public int id { get; set; }

        [MaxLength(100), NotNull]
        public string usuario { get; set; }

        [MaxLength(100), NotNull]
        public string mensaje { get; set; }

        public bool recibido { get; set; }
    }
}