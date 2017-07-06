﻿using System;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace ReseauDLL
{
    public delegate void DataReceiveTCP(ConnexionClient sender, object data);
    public class ConnexionClient
    {
        TcpClient _client;
        string _clientname;

        public string Nom
        {
            get { return _clientname; }
            set { _clientname = value; }
        }

        public event DataReceiveTCP DataReceived;

        public ConnexionClient(TcpClient client)
        {
            _client = client;
            _clientname = _client.Client.RemoteEndPoint.ToString().Split(':')[0];

            Thread th = new Thread(Lecture);
            th.Start(_client);
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
                    System.Diagnostics.Debug.WriteLine(string.Format("ConnexionClient.Lecture : Exception : {0}", ex.Message));
                    lectureLoop = false;
                }
            } while (lectureLoop);
        }
        private void GestionDataFromServer(object data)
        {
            DataReceived(this, data);
        }

        public void SendData(object data)
        {
            try
            {
                lock (_client.GetStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(_client.GetStream(), data);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("ConnexionClient.SendData : Exception : {0}", ex.Message));
            }
        }
    }
}
