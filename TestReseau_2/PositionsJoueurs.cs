using System.Collections.Generic;
using System.Windows;
using System;

namespace Labyrinthe
{
    [Serializable]
    public class PositionsJoueurs : Dictionary<Point,string>
    {
        public PositionsJoueurs() : base() { }
    }
}