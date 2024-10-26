using DestroyerGUI.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace DestroyerGUI
{
    public class GameEngine
    {
        public Board Board { get; private set; } = new Board();
        private const double Scale = 100000;

        public void InitializeGame()
        {
            Team team1 = new Team("NATO", Color.FromRgb(0, 0, 255));
            Team team2 = new Team("Russia", Color.FromRgb(255, 0, 0));
            Team team3 = new Team("Neutral", Color.FromRgb(255, 255, 255));

            Ship ship1 = new Ship(40000, 40000, Math.PI - 0.5, team1, 154, 20, 5000);
            Ship ship2 = new Ship(0, 0, 0, team2, 130, 15, 100);
            Ship ship3 = new Ship(20, -20000, Math.PI / 2, team3, 350, 50, 30);

            Board.Ships.Add(ship1);
            Board.Ships.Add(ship2);
            Board.Ships.Add(ship3);
        }

        public void UpdatePositions(double deltaT)
        {
            foreach (var ship in Board.Ships)
            {
                double vx = ship.Speed * Math.Sin(ship.Heading);
                double vy = ship.Speed * Math.Cos(ship.Heading);

                ship.X += vx * deltaT;
                ship.Y += vy * deltaT;
            }
        }

        public Point Transform(double x, double y, double actualWidth, double actualHeight)
        {
            double cx = actualWidth / Scale * x + actualWidth / 2;
            double cy = -actualHeight / Scale * y + actualHeight / 2;
            return new Point(cx, cy);
        }
    }
}
