using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReseauDLL;
using System.Threading;

namespace TestReseau_1
{
    class Program
    {
        static Reseau rezo;
        static void Main(string[] args)
        {
            rezo = new Reseau(Etat.CLIENT);
            rezo.DataReceived += Rezo_DataReceived;
            rezo.FinRechercheServer += Rezo_FinRechercheServer;

            Labyrinthe.PositionsJoueurs pos = new Labyrinthe.PositionsJoueurs();

            Console.ReadLine();
            //rezo.SendData("ping");
            rezo.SendData(pos);
            /*zo.SendData(DateTime.Now);
            rezo.SendData(new int[] { 0, 1, 2, 3, 4, 5, 6 });*/

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
