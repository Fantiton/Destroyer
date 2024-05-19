using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestroyerGUI.Model
{
    internal class Board
    {
        public List<Ship> Ships {  get; private set; } = new List<Ship>();
    }
}
