using DestroyerGUI.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DestroyerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Board board = new Board();
        private const double Scale = 100000;
        private const double FPS = 100;
        private DateTime previousInvocation = DateTime.Now;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            Team team1 = new Team("NATO", Color.FromRgb(0, 0, 255));
            Team team2 = new Team("Russia", Color.FromRgb(255, 0, 0));
            Team team3 = new Team("Neutral", Color.FromRgb(255, 255, 255));

            Ship ship1 = new Ship(40000, 40000, Math.PI - 0.5, team1, 154, 20, 5000);
            Ship ship2 = new Ship(0, 0, 0, team2, 130, 15, 100);
            Ship ship3 = new Ship(20, -20000, Math.PI / 2, team3, 350, 50, 30);

            board.Ships.Add(ship1);
            board.Ships.Add(ship2);
            board.Ships.Add(ship3);

            var timer = new DispatcherTimer(DispatcherPriority.Send);
            timer.Interval = TimeSpan.FromMilliseconds(1000 / FPS);
            timer.Tick += Tick;
            timer.Start();
        }

        protected override void OnRender(DrawingContext rabarbar)
        {
            base.OnRender(rabarbar);
            sigma.Children.Clear();
            board.Ships.ForEach(ship => sigma.Children.Add(RenderShip(ship)));
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
            double cx = sigma.ActualWidth / Scale * x + sigma.ActualWidth / 2;
            double cy = -sigma.ActualHeight / Scale * y + sigma.ActualHeight / 2;
            return new Point(cx, cy);   
        }

        private void Tick(object sender, EventArgs e)
        {
            DateTime currentInvocation = DateTime.Now;
            TimeSpan delay = currentInvocation.Subtract(previousInvocation);
            previousInvocation = currentInvocation;

            double deltaT = delay.TotalSeconds;
            foreach (var ship in board.Ships)
            {
                double vx = ship.Speed * Math.Sin(ship.Heading);
                double vy = ship.Speed * Math.Cos(ship.Heading);

                ship.X += vx * deltaT;
                ship.Y += vy * deltaT;

                Point shipPos = Transform(ship.X, ship.Y);
                Canvas.SetLeft(ship.Ellipse, shipPos.X);
                Canvas.SetTop(ship.Ellipse, shipPos.Y);
            }
        }
    }
}