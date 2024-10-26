using DestroyerGUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DestroyerGUI.Gui
{
    public class DebugRadar : Canvas
    {
        private const double Scale = 100000;
        public void Paint(Board board)
        {
            this.Children.Clear();
            board.Ships.ForEach(ship => this.Children.Add(RenderShip(ship)));
            foreach (Ship ship in board.Ships)
            {
                Ellipse ellipse = ship.Ellipse;
                ellipse.Margin = new System.Windows.Thickness(ship.X, ship.Y, 0, 0);
                Children.Add(ellipse);
            }
        }
        private Ellipse RenderShip(Ship ship)
        {
            Ellipse myEllipse = new Ellipse();

            Point shipPos = Transform(ship.X, ship.Y);

            myEllipse.Stroke = new SolidColorBrush(ship.Team.Color);
            myEllipse.Width = ship.Width / 10;
            myEllipse.Height = ship.Lenght / 10;
            myEllipse.Fill = new SolidColorBrush(ship.Team.Color);

            RotateTransform rt = new RotateTransform();
            rt.Angle = ship.Heading * 180 / Math.PI;
            myEllipse.RenderTransform = rt;

            Canvas.SetLeft(myEllipse, shipPos.X);
            Canvas.SetTop(myEllipse, shipPos.Y);
            myEllipse.StrokeThickness = 2;
            ship.Ellipse = myEllipse;
            return myEllipse;
        }
        private Point Transform(double x, double y)
        {
            double cx = this.ActualWidth / Scale * x + this.ActualWidth / 2;
            double cy = -this.ActualHeight / Scale * y + this.ActualHeight / 2;
            return new Point(cx, cy);
        }

        public void UpdateShipPosition(Ship ship)
        {
            Point shipPos = Transform(ship.X, ship.Y);
            Canvas.SetLeft(ship.Ellipse, shipPos.X);
            Canvas.SetTop(ship.Ellipse, shipPos.Y);
        }
    }
}
