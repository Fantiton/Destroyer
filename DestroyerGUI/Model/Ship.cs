using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace DestroyerGUI.Model
{
    internal class Ship
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Heading { get; private set; }
        public double Lenght { get; private set; }
        public double Width { get; private set; }

        public double Speed { get; private set; }   
        public Team Team { get; private set; }

        public Ellipse Ellipse { get; set; }    

        public Ship(double x, double y, double heading, Team team, double lenght, double width, double speed)
        {
            X = x;
            Y = y;
            Heading = heading;
            Team = team;
            team.Ships.Add(this);
            Lenght = lenght;
            Width = width;
            Speed = speed;  
            Team = team;    
        }
    }
}
