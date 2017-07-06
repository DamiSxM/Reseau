using System.Collections.Generic;
using System.Windows;
using System;
using System.Runtime.Serialization;

namespace Labyrinthe
{
    [Serializable]
    public class PositionsJoueurs : Dictionary<Point,string>
    {
        public PositionsJoueurs():base() { }
        public PositionsJoueurs(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}