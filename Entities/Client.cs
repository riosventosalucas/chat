using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Entities
{
    public abstract class Client
    {
        #region Atributos

        /// <summary>
        /// Representa el nombre del cliente
        /// </summary>
        private string nickName;

        /// <summary>
        /// Objeto para establecer la conexion con el servidor
        /// </summary>
        private TcpClient socket;

        /// <summary>
        /// Objeto que genera el "canal" para poder enviar y recibir datos
        /// </summary>
        private NetworkStream stream;

        /// <summary>
        /// Objeto que sirve para enviar datos al servidor
        /// </summary>
        private BinaryWriter writer;

        /// <summary>
        /// Objeto que sirve para leer datos del servidor
        /// </summary>
        private BinaryReader reader;

        #endregion

        #region Propiedades

        /// <summary>
        /// Representa el nickname del cliente
        /// </summary>
        public string NickName 
        {
            get {return this.nickName; }
            set { this.nickName = value; }
        }

        /// <summary>
        /// Representa el socket del cliente
        /// </summary>
        public TcpClient Socket
        {
            get { return this.socket; }
            set { this.socket = value; }
        }

        /// <summary>
        /// Representa el stream del cliente
        /// </summary>
        public NetworkStream Stream 
        {
            get { return this.stream; }
            set { this.stream = value; }
        }

        /// <summary>
        /// Representa el obj Writer del cliente
        /// </summary>
        public BinaryWriter Writer
        {
            get { return this.writer; }
            set { this.writer = value; }
        }

        /// <summary>
        /// Representa el obj Reader del cliente
        /// </summary>
        public BinaryReader Reader
        {
            get { return this.reader; }
            set { this.reader = value; }
        }
        #endregion

        #region Constructores
        
        /// <summary>
        /// Inicializa los atributos necesarios para poder realizar la conexion y comunicacion con el servidor
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="stream"></param>
        /// <param name="writer"></param>
        /// <param name="reader"></param>
        public Client(string nickname, TcpClient socket, NetworkStream stream, BinaryWriter writer, BinaryReader reader)
        {
            this.NickName = nickName;
            this.Socket   = socket;
            this.Stream   = stream;
            this.Writer   = writer;
            this.reader   = reader;            
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Metodo para enviar mensajes
        /// </summary>
        /// <param name="message"></param>
        public abstract void SendMessage(string message);

        /// <summary>
        /// Metodo para recibir mensajes
        /// </summary>
        /// <returns>string Mensaje</returns>
        public abstract string GetMessage();

        #endregion
    }

}
