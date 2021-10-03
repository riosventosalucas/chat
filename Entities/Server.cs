using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;

namespace Entities
{
    public class Server
    {
        #region Atributos
        
        /// <summary>
        /// Puerto en el que va a escuchar el servidor.
        /// </summary>
        private int port;

        /// <summary>
        /// Socket donde va a escuchar el servidor.
        /// </summary>
        private TcpListener socket;

        /// <summary>
        /// Lista de clientes.
        /// </summary>
        private List<Client> clientList;

        /// <summary>
        /// Cola de mensajes.
        /// </summary>
        public static Queue<string> messagesQueue;

        /// <summary>
        /// Cola de mensajes del servidor.
        /// </summary>
        private Queue<string> serverMessages;

        private Thread acceptclients;

        #endregion

        #region Propiedades

        /// <summary>
        /// Representa el puerto donde escucha el servidor.
        /// </summary>
        public int Port
        {
            get { return this.port; }
        }

        /// <summary>
        /// Representa el socket del servidor.
        /// </summary>
        public TcpListener Socket
        {
            get { return this.socket; }
        }

        /// <summary>
        /// Representa la lista de clientes.
        /// </summary>
        public List<Client> ClientList
        {
            get { return this.clientList; }
        }

        /// <summary>
        /// Representa la cola de mensajes del servidor.
        /// </summary>
        public Queue<string> ServerMessage
        {
            get { return this.serverMessages; }
        }
        #endregion

        #region Constructores

        /// <summary>
        /// Inicializa el puerto y el socket del servidor. Para fines practicos
        /// hardcodeamos para que escuche en cualquier ip.
        /// </summary>
        /// <param name="port"></param>
        public Server(int port):this(IPAddress.Any, port) {}

        /// <summary>
        /// Inicializa el puerto y el socket del servidor
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public Server (IPAddress ip, int port)
        {
            this.port = port;
            this.socket = new TcpListener(ip, this.Port);
            this.serverMessages = new Queue<string>();
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Metodo que inicializa el socket para poder empezar a escuchar conexiones entrantes.
        /// </summary>
        /// <returns></returns>
        private bool StartServer()
        {
            bool started;

            try
            {
                this.socket.Start();
                started = true;
            }
            catch (Exception)
            {
                started = false;
            }

            return started;
        }

        private void AcceptClient()
        {
            this.serverMessages.Enqueue("[ INFO ] Waiting for clients...");
            while (true)
            {
                this.ConfigureClient(this.socket.AcceptTcpClient());
            }
        }

        private void ConfigureClient(TcpClient clientSocket)
        {
            this.serverMessages.Enqueue("[ DONE ] New client connected.");
            string nickname;

            NetworkStream stream;
            BinaryWriter w;
            BinaryReader r;
            stream = clientSocket.GetStream();
            w = new BinaryWriter(stream);
            r = new BinaryReader(stream);

            //Lo primero que tenemos que leer es el nick del usuario.
            nickname = r.ReadString();

            this.ClientList.Add(new User(nickname, clientSocket, stream, w, r));
        }

        public void Run()
        {
            if (this.StartServer())
            {
                this.serverMessages.Enqueue("[ DONE ] Server StartUp.");
                this.acceptclients = new Thread(this.AcceptClient);
                this.acceptclients.Start();
            }
            else
            {
                this.serverMessages.Enqueue("[ ERROR ] Cannot start server on that port.");
            }
        }
        #endregion


    }
}
