using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace UsoSQLite
{
    class WebSocketCliente
    {
        static ClientWebSocket webSocket;
        Task t, t1, t2;
        string uri;

        public WebSocketCliente(string uri)
        {

        }

        public static async Task iniciarCliente(string uri, string usuario)
        {
            webSocket = null;
            try
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>{
                    {"type","nuevo_usuario"},
                    {"nombre",usuario}
                };
                string jsonObj = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
                webSocket = new ClientWebSocket();
                await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
                await Task.WhenAll(recibirMensaje(webSocket), enviarMensaje(jsonObj));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }
            finally
            {
                if (webSocket != null)
                    webSocket.Dispose();
            }

        }

        public static event MyEventHandlerRecibirMensaje myEventHandlerRecibirMensaje;
        public static async Task recibirMensaje(ClientWebSocket cliente)
        {
            while (cliente.State == WebSocketState.Open)
            {
                ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
                WebSocketReceiveResult result = await cliente.ReceiveAsync(bytesReceived, CancellationToken.None);
                string msg = Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
                //Disparar evento con el mensaje recibido                

                Dictionary<string, string> datos = JsonConvert.DeserializeObject<Dictionary<string, string>>(msg);
                if (datos["type"] == "mensaje")
                {
                    //Disparar evento con el mensaje recibido
                    MyEventArgsRecibirMensaje myEventArgsRecibirMensaje = new MyEventArgsRecibirMensaje(new Datos(
                        datos["type"], datos["nombre_usuario"], datos["contenido"]));
                    myEventHandlerRecibirMensaje("", myEventArgsRecibirMensaje);
                }
            }
        }
        public static async Task enviarMensaje(string msg)
        {
            if (webSocket != null && webSocket.State == WebSocketState.Open)
            {
                ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
                await webSocket.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}