using System;
namespace Donjon
{
    interface IVisible
    {
        string MapSymbol { get; }
        ConsoleColor Color { get; }
    }

    public abstract class Creature : IVisible
    {
        public string MapSymbol { get; }
        public ConsoleColor Color { get; protected set; }

        public Creature(string mapSymbol)
        {
            MapSymbol = mapSymbol;
        }
    }

    public abstract class Monster : Creature
    {
        public int Health { get; set; }
        public string Name { get; }

        protected int Damage = 20;

        public Monster(string mapSymbol, string name, int health) : base(mapSymbol)
        {
            Name = name;
            Health = health;
            Color = ConsoleColor.Red;
        }

        internal string Fight(Hero hero)
        {
            hero.Health -= Damage;
            if (hero.Health > 0)
            {
                return $"The {Name} attacked you for {Damage} hp damage.";
            }
            else
            {
                return $"You were killed by the {Name}!!!1!||1one";
            }
        }
    }

    public class Goblin : Monster
    {
        public Goblin() : base("G", "goblin", 20)
        {
            Color = ConsoleColor.Green;
        }
    }
    public class Orc : Monster
    {
        public Orc() : base("O", "orc", 40)
        {
            Damage = 40;
        }
    }
}