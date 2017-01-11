using System;

namespace Donjon
{
    public class Item : IVisible
    {
        public string MapSymbol { get; }
        public ConsoleColor Color { get; protected set; }

        public Item(string mapSymbol)
        {
            MapSymbol = mapSymbol;
            Color = ConsoleColor.Magenta;
        }
    }

    public class Coin : Item {
        public Coin() : base("c")
        {
            Color = ConsoleColor.Yellow;
        }
    }
}