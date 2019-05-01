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
            int haveSolution = 0;
            List<ButtonExtend> closedFields = new List<ButtonExtend>();
            //---------------------------------------------------
            if (isFirstClick)
            {
                FieldClick(allFields[xFirst, yFirst], mouseArgs);
            }
            haveSolution = 0;
            foreach (ButtonExtend field in allFields)
            {
                if (field.value > 0 && field.value < 9)
                {
                    if (field.value == CountClosedAround(field.xPosition, field.yPosition) + CountFlagsAround(field.xPosition, field.yPosition) && field.toCheck)
                    {
                        flagAround(field);
                        haveSolution++;
                    }
                }
            }
            foreach (ButtonExtend field in allFields)
            {
                if (field.value > 0 && field.value < 9)
                {
                    if (field.value == CountFlagsAround(field.xPosition, field.yPosition) && field.toCheck)
                    {
                        OpenAround(field);
                        haveSolution++;
                    }
                }
            }
            checkSolution();
            if (haveSolution <= 0)
            {
                foreach (ButtonExtend field in allFields)
                {
                    if (field.value == 9)
                    {
                        closedFields.Add(field);
                    }
                }
                int randomIndex = random.Next(0, closedFields.Count());
                FieldClick(closedFields[randomIndex], mouseArgs);
                closedFields.Clear();
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
        int CountOpenAround(int xPos, int yPos)
        {
            int openCounter = -1;
            for (int y = yPos - 1; y <= yPos + 1; y++)
            {
                for (int x = xPos - 1; x <= xPos + 1; x++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        if (allFields[x, y].value != 9 && allFields[x, y].value != -1)
                        {
                            openCounter++;
                        }
                    }
                }
            }
            return openCounter;
        }
        int CountNeighboursAround(int xPos, int yPos)
        {
            int neighborsCounter = -1;
            for (int y = yPos - 1; y <= yPos + 1; y++)
            {
                for (int x = xPos - 1; x <= xPos + 1; x++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        neighborsCounter++;
                    }
                }
            }
            return neighborsCounter;
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
        bool checkSolution()
        {
            foreach (ButtonExtend field in allFields)
            {
                if (field.value > 0 && field.value < 9)
                {
                    if (field.value == CountFlagsAround(field.xPosition, field.yPosition) && field.value == CountNeighboursAround(field.xPosition, field.yPosition) - CountOpenAround(field.xPosition, field.yPosition))
                    {
                        field.toCheck = false;
                    }
                }
            }
            return false;
        }
    }
}
