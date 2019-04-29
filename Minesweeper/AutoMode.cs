using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Minesweeper
{
    public partial class Form1
    {
        void AutoGame()
        {
            mouseArgs = new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 0);
            int xFirst, yFirst;
            xFirst = random.Next(0, width);
            yFirst = random.Next(0, height);
            //---------------------------------------------------

            if (isFirstClick)
            {
                FieldClick(allFields[xFirst, yFirst], mouseArgs);
            }
            foreach (ButtonExtend field in allFields)
            {
                if (field.value > 0 && field.value < 9)
                {
                    if (field.value == CountClosedAround(field.xPosition, field.yPosition) + CountFlagsAround(field.xPosition, field.yPosition))
                    {
                        flagAround(field);
                    }
                }
            }
            foreach (ButtonExtend field in allFields)
            {
                if (field.value > 0 && field.value < 9)
                {
                    if (field.value == CountFlagsAround(field.xPosition, field.yPosition))
                    {
                        OpenAround(field);
                    }
                }
            }
        }
        int CountFlagsAround(int xPos, int yPos)
        {
            int flagsCounter = 0;
            for (int y = yPos - 1; y <= yPos + 1; y++)
            {
                for (int x = xPos - 1; x <= xPos + 1; x++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        if (allFields[x, y].value == -1)
                        {
                            flagsCounter++;
                        }
                    }
                }
            }
            return flagsCounter;
        }
        int CountClosedAround(int xPos, int yPos)
        {
            int closedCounter = 0;
            for (int y = yPos - 1; y <= yPos + 1; y++)
            {
                for (int x = xPos - 1; x <= xPos + 1; x++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        if (allFields[x, y].value == 9)
                        {
                            closedCounter++;
                        }
                    }
                }
            }
            return closedCounter;
        }
        void OpenAround(ButtonExtend field)
        {
            mouseArgs = new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 0);
            for (int y = field.yPosition - 1; y <= field.yPosition + 1; y++)
            {
                for (int x = field.xPosition - 1; x <= field.xPosition + 1; x++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        if (allFields[x, y].value == 9)
                        {
                            FieldClick(allFields[x, y], mouseArgs);
                        }
                    }
                }
            }
        }
        void flagAround(ButtonExtend field)
        {
            mouseArgs = new MouseEventArgs(MouseButtons.Right, 1, 1, 1, 0);
            for (int y = field.yPosition - 1; y <= field.yPosition + 1; y++)
            {
                for (int x = field.xPosition - 1; x <= field.xPosition + 1; x++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        if (allFields[x, y].value == 9)
                        {
                            FieldClick(allFields[x, y], mouseArgs);
                        }
                    }
                }
            }
        }
    }
}
