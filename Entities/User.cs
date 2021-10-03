using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Entities
{
    public sealed class User: Client
    {

        #region Atributos

        #endregion

        #region Propiedades

        #endregion

        #region Constructores
        /// <summary>
        /// Inicializa el nickname y llama al constructor base para inicializar los demas atributos.
        /// </summary>
        /// <param name="nickname"></param>
        /// <param name="socket"></param>
        /// <param name="stream"></param>
        /// <param name="writer"></param>
        /// <param name="reader"></param>
        public User(string nickname, TcpClient socket, NetworkStream stream, BinaryWriter writer, BinaryReader reader):
            base(nickname, socket, stream, writer, reader) {}
        #endregion

        #region Metodos

        /// <summary>
        /// Metodo para enviar Mensajes al servidor.
        /// </summary>
        /// <param name="message"></param>
        public override void SendMessage(string message)
        {
            Writer.Write(message);
        }

        /// <summary>
        /// Metodo para recibier mensajes del servidor
        /// </summary>
        /// <returns></returns>
        public override string GetMessage()
        {
            /*
             Utilizo read string porq no nos vamos a hacer los hardcores
             usando read bytes y esas cosas
            */
            return Reader.ReadString();
        }
        #endregion

    }
}
