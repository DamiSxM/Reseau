﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace ReseauDLL
{
    class ClientTCP
    {
        int _port;
        string _ipServer;

        private TcpClient _client;
        private string _clientName;

        public string Nom { get { return _clientName; } }

        public ClientTCP(int port)
        {
            _port = port;
        }

        public event DataReceive DataReceived;

        public bool Connect(string ipserver)
        {
            try
            {
                _ipServer = ipserver;
                _client = new TcpClient(_ipServer, _port);
                _clientName = ((IPEndPoint)_client.Client.LocalEndPoint).Address.ToString();

                Thread th = new Thread(Lecture);
                th.Start(_client);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("ClientTCP.Connect : Exception connexion server : {0}", ex.Message));
                return false;
            }
        }

        void Lecture(object clientObj)
        {
            bool lectureLoop = true;
            TcpClient client = (TcpClient)clientObj;
            do
            {
                try
                {
                    if (client.GetStream().CanRead)
                    {
                        NetworkStream nstream = client.GetStream();
                        BinaryFormatter formatter = new BinaryFormatter();

                        object data = (object)formatter.Deserialize(nstream);
                        GestionDataFromServer(data);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("ClientTCP.Lecture : Exception : {0}", ex.Message));
                    lectureLoop = false;
                }
            } while (lectureLoop);
        }

        private void AttemptLogin(string nomClient)
        {
            SendDataServer(nomClient);
        }

        private void SendDataServer(object data)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("ClientTCP.SendDataServer : client -> Server : {0}", data));
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(_client.GetStream(), data);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("ClientTCP.SendDataServer : Exception : {0}", ex.Message));
            }
        }
        public void SendData(object data)
        {
            SendDataServer(data);
        }
        private void GestionDataFromServer(object data)
        {
            string ip = _client.Client.RemoteEndPoint.ToString().Split(':')[0];
            DataReceived(ip, data);
        }
    }
}
