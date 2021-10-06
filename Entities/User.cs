using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace Entities
{
    public sealed class User: Client
    {
        /// <summary>
        /// Representa el nombre del cliente
        /// </summary>
        private string nickName;

        #region Atributos
        /// <summary>
        /// Thread para recibir mensajes.
        /// </summary>
        private Thread getMessages;
        #endregion

        #region Propiedades
        /// <summary>
        /// Representa el nickname del cliente
        /// </summary>
        public string NickName
        {
            get { return this.nickName; }
            set { this.nickName = value; }
        }
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
            base(socket, stream, writer, reader) 
        {
            this.NickName = nickname;
            this.getMessages = new Thread(this.GetMessage);
            this.getMessages.Start();
        }
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
        protected override void GetMessage()
        {
            string message;

            while (true)
            {
                /*
                    Utilizo read string porq no nos vamos a hacer los hardcores
                    usando read bytes y esas cosas
                */
                message = Reader.ReadString();
                if (message[0] == '/')
                {
                    //Aca podemos configurar comandos
                }
                Server.messagesQueue.Enqueue($"[{this.NickName}]: {message}");
            }
        }

        #endregion

    }
}
