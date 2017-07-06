using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReseauDLL;
using System.Threading;

namespace TestReseau_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(1000);
            Reseau rezo = new Reseau(Etat.SERVER);
            rezo.DataReceived += Rezo_DataReceived;
            rezo.FinRechercheServer += Rezo_FinRechercheServer;

            while (true)
            {
                rezo.SendData(Console.ReadLine());
            }
        }

        private static void Rezo_FinRechercheServer(bool isserver)
        {
            if (isserver) Console.WriteLine("SERVER !");
            else Console.WriteLine("CLIENT !");
        }

        private static void Rezo_DataReceived(string sender, object data)
        {
            Console.WriteLine("sender : {0}", sender.ToString());
            Console.WriteLine("data : {0}", data.ToString());
        }
    }
}
