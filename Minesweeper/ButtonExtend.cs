using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    class ButtonExtend : Button
    { 
        //For Manual Game
        public bool isBomb;
        public bool isClickable;
        public bool isAdded;
        public int xPosition;
        public int yPosition;

        //For Auto Game
        public int value;
        public bool toCheck;

    }
}
