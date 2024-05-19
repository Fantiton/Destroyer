using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DestroyerGUI.Model
{
    internal class Team
    {
        public string Name { get; set; }
        public Color Color {  get; set; }

        public List<Ship> Ships { get; set; }

        public Team(string name, Color color)
        {
            Name = name;    
            Color = color;
            Ships = new List<Ship>();
        }
    }
}
