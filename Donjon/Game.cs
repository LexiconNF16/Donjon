using System;

namespace Donjon
{
    internal class Game
    {
        private Map map;
        private Hero hero;
        private string log = "";


        public Game(int width, int height)
        {
            map = new Map(width, height);
        }

        internal void Run()
        {
            // init game
            hero = new Hero(health: 100);
            PopulateMap();

            // game loop
            do
            {
                Console.Clear();
                PrintStatus();
                PrintMap();
                PrintLog();
                PrintVisible();

                Console.WriteLine("(arrow=movement space=wait)");
                Console.WriteLine("What do you do?");
                ConsoleKey key = GetInput();
                var cell = map.Cells[hero.X, hero.Y];

                // process actions
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (hero.Y > 0) hero.Y -= 1;
                        break;
                    case ConsoleKey.DownArrow:
                        if (hero.Y < map.Height - 1) hero.Y += 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (hero.X > 0) hero.X -= 1;
                        break;
                    case ConsoleKey.RightArrow:
                        if (hero.X < map.Width - 1) hero.X += 1;
                        break;
                    case ConsoleKey.P:
                        if (cell.Item != null)
                        {
                            PickUp(cell);
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        var monster = cell.Monster;
                        if (monster != null)
                        {
                            Fight(monster);
                            if (monster.Health <= 0) cell.Monster = null;
                        }
                        break;
                }
            } while (hero.Health > 0);
            Console.Clear();
            PrintStatus();
            PrintMap();
            PrintLog();

            Console.WriteLine("All ur base are belong to us!");
            Console.WriteLine("Gmae ovr!");

        }

        private void PickUp(Cell cell)
        {
            if (hero.Pickup(cell.Item))
            {
                Log($"You picked up the {cell.Item}");
                cell.Item = null;
            }
            else
            {
                Log($"You picked up NOTHING! In your face!");
            }
        }

        private void PrintLog()
        {
            Console.Write(log);
            log = "";
        }

        private void Fight(Monster monster)
        {
            Log(hero.Fight(monster));

            if (monster.Health > 0)
            {
                Log(monster.Fight(hero));
            }
        }

        private void Log(string message)
        {
            log += message + "\n";
        }

        private void PrintVisible()
        {
            var cell = map.Cells[hero.X, hero.Y];
            var monster = cell.Monster;
            if (monster != null)
            {
                Console.WriteLine();
                Console.WriteLine($"You see a {monster.Name} ({monster.Health} hp)");
            }
        }

        private void PopulateMap()
        {
            map.Cells[7, 4].Monster = new Goblin();
            map.Cells[7, 4].Item = new Coin();

            map.Cells[4, 7].Monster = new Goblin();
            map.Cells[9, 7].Monster = new Orc();
            map.Cells[7, 9].Monster = new Orc();

            map.Cells[4, 7].Item = new Coin();
            map.Cells[9, 7].Item = new Coin();
            map.Cells[5, 8].Item = new Coin();
            map.Cells[2, 5].Item = new Coin();
            map.Cells[1, 9].Item = new Coin();
            map.Cells[1, 6].Item = new Coin();
            map.Cells[7, 4].Item = new Coin();
            map.Cells[2, 6].Item = new Coin();
        }

        private void PrintStatus()
        {
            Console.WriteLine();
            Console.WriteLine($"Health: {hero.Health} hp");
            // Console.WriteLine("Health: " + hero.Health.ToString() + " hp");
        }

        private ConsoleKey GetInput()
        {
            Console.WriteLine("Press a key");
            var keyInfo = Console.ReadKey(intercept: true);
            var key = keyInfo.Key;
            return key;
        }

        private void PrintMap()
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    var cell = map.Cells[x, y];
                    Console.Write(" "); // horizontal margin

                    IVisible entity = null;
                    if (hero.X == x && hero.Y == y)
                    {
                        entity = hero;
                    }
                    else if (cell.Monster != null)
                    {
                        entity = cell.Monster;
                    }
                    else if (cell.Item != null)
                    {
                        entity = cell.Item;
                    }
                    else
                    {
                        Console.Write(".");
                    }

                    if (entity != null)
                    {
                        Console.ForegroundColor = entity.Color;
                        Console.Write(entity.MapSymbol);
                        Console.ResetColor();
                    }


                }
                Console.WriteLine();
            }
        }
    }
}