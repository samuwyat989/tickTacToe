using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace tickTacToe
{
    class PlayerMove
    {
        public int selectButtonIndex;
        public string xoVal;
        public bool xTurn, gameOver;

        public PlayerMove(ref bool _xTurn, int _selectButtonIndex)
        {
            xTurn = _xTurn;
            selectButtonIndex = _selectButtonIndex;

            if (xTurn)
            {
                xoVal = "X";
                xTurn = false;
            }
            else
            {
                xoVal = "O";
                xTurn = true;
            }
            _xTurn = xTurn;
        }

        public PlayerMove(bool _xTurn, int turnCount, int[] checkX, int[] checkO,
            Dictionary<int, bool> buttonGroup, Dictionary<string, int> buttonCheck, ref bool fourCorner, ref bool cornerStop,
            string[] checkXName, string[] checkOName, bool isCaps, List<Rectangle>allButtons, Rectangle[] corners, Rectangle[] nonCorners)
        {
            xoVal = "O";
            if (turnCount == 1)
            {
                if (buttonGroup[0] == false || buttonGroup[6] == false || buttonGroup[2] == false || buttonGroup[8] == false)
                {
                    fourCorner = true;//player chose one of the corners, important for counter stategy
                }
                if (buttonGroup[4])//Opening move
                {
                    selectButtonIndex = 4;
                    return;
                }
                else
                {
                    selectButtonIndex = 0;
                    return;
                }
            }
            else
            {
                if (turnCount == 3)//corner stop
                {
                    if (buttonGroup[7] == false && buttonGroup[5] == false
                        || buttonGroup[1] == false && buttonGroup[5] == false
                        || buttonGroup[3] == false && buttonGroup[7] == false)
                    {
                        cornerStop = true;
                    }
                }
                bool noThreat = false, compWin = false;
                string last3Char;

                for (int i = 0; i < checkO.Length; i++)//checks for each row/column/diagonal 
                {
                    if (checkO[i] == 2 && checkX[i] == 0)//checks all row/column for O 
                    {
                        last3Char = checkOName[i].Substring(checkOName[i].Length - 3);//takes part of the row/col/diagonal, starting from the third last letter and ehding on the last letter
                        FindComputerMove(last3Char, isCaps, buttonGroup, buttonCheck);
                        compWin = true;
                        break;
                    }
                }
                if (compWin == false)
                {
                    for (int i = 0; i < checkX.Length; i++)//checks for each row/column/diagonal 
                    {
                        if (checkX[i] == 2 && checkO[i] == 0)//checks all row/column for 2 X  
                        {
                            last3Char = checkXName[i].Substring(checkXName[i].Length - 3);//takes part of the row/col/diagonal, starting from the third last letter and ehding on the last letter
                            FindComputerMove(last3Char, isCaps, buttonGroup, buttonCheck);
                            noThreat = false;
                            break;
                        }
                        noThreat = true;
                    }
                    if (noThreat)
                    {
                        if (fourCorner)
                        {
                            FindComputerMove(nonCorners, corners, ref cornerStop, buttonGroup, allButtons);
                        }
                        else
                        {
                            FindComputerMove(corners, nonCorners, ref cornerStop, buttonGroup, allButtons);
                        }
                    }
                }
            }
        }
        public void FindComputerMove(string endString, bool isCaps, Dictionary<int, bool> buttonGroup, Dictionary<string, int> buttonCheck)
        {
            Char c;
            string endVal = endString.Substring(endString.Length - 1);

            for (int i = 1; i <= 3; i++)
            {
                if (endString.Contains("w"))//it is a row
                {
                    c = (Char)((isCaps ? 65 : 97) + (i - 1)); //converts the current i value to a letter, ex. 1 = a, 2 = b ...
                    if(buttonGroup[buttonCheck[c.ToString()+endVal]])
                    {
                        selectButtonIndex = buttonCheck[c.ToString() + endVal];
                        break;
                    }
                }
                else if (endString.Contains("al"))//it is a diagonal
                {
                    c = (Char)((isCaps ? 65 : 97) + (i - 1));

                    if (endString.Last().ToString() == "1")//diagonal 1
                    {
                        endVal = i.ToString();
                        if (buttonGroup[buttonCheck[c.ToString() + endVal]])
                        {
                            selectButtonIndex = buttonCheck[c.ToString() + endVal];
                            break;
                        }
                    }
                    else//diagonal 2
                    {
                        endVal = (4 - i).ToString();
                        if (buttonGroup[buttonCheck[c.ToString() + endVal]])
                        {
                            selectButtonIndex = buttonCheck[c.ToString() + endVal];
                            break;
                        }
                    }
                }
                else//it is a column
                {
                    if (buttonGroup[buttonCheck[endVal.ToLower() + i.ToString()]])
                    {
                        selectButtonIndex = buttonCheck[endVal.ToLower() + i.ToString()];
                        break;
                    }
                }
            }
        }
        public void FindComputerMove(Rectangle[] style1, Rectangle[] style2, ref bool cornerStop, Dictionary<int, bool> buttonGroup, List<Rectangle> allButtons)
        {
            for (int i = 0; i < style1.Length; i++)
            {
                //need to corner stop (go c2 then b3 then b3. game.)
                if (cornerStop)
                {
                    cornerStop = false;
                    if (buttonGroup[5] == false && buttonGroup[7] == false)
                    {
                        selectButtonIndex = 8;
                        return;
                    }
                    else if (buttonGroup[3] == false && buttonGroup[7] == false)
                    {
                        selectButtonIndex = 6;
                        return;
                    }
                    else if(buttonGroup[1] == false && buttonGroup[5] == false)
                    {
                        selectButtonIndex = 2;
                        return;
                    }
                }
  
                if (buttonGroup[allButtons.IndexOf(style1[i])])
                {
                    selectButtonIndex = allButtons.IndexOf(style1[i]);
                    return;
                }
            }

            for (int i = 0; i < style2.Length; i++)
            {
                if (buttonGroup[allButtons.IndexOf(style2[i])])
                {
                    selectButtonIndex = allButtons.IndexOf(style2[i]);
                    return;
                }
            }
        }



        public void UpdateBoardValues(int selectButtonIndex, ref int[] checkX, ref int[] checkO)
        {
            int[] playerCheck;

            if (xTurn)//this is inversed as it was inversed in the set up of the playerMove
            {
                playerCheck = checkO;
            }
            else
            {
                playerCheck = checkX;
            }

            if (selectButtonIndex == 0 || selectButtonIndex == 3 || selectButtonIndex == 6)
            {
                playerCheck[0]++;//row 1
            }
            else if (selectButtonIndex == 1 || selectButtonIndex == 4 || selectButtonIndex == 7)
            {
                playerCheck[1]++;//row 2
            }
            else if (selectButtonIndex == 2 || selectButtonIndex == 5 || selectButtonIndex == 8)
            {
                playerCheck[2]++;//row 3
            }
            if (selectButtonIndex == 0 || selectButtonIndex == 1 || selectButtonIndex == 2)
            {
                playerCheck[3]++; //col 1
            }
            else if (selectButtonIndex == 3 || selectButtonIndex == 4 || selectButtonIndex == 5)
            {
                playerCheck[4]++;// col 2
            }
            else if (selectButtonIndex == 6 || selectButtonIndex == 7 || selectButtonIndex == 8)
            {
                playerCheck[5]++; //col 3
            }
            if (selectButtonIndex == 0 || selectButtonIndex == 4 || selectButtonIndex == 8)
            {
                playerCheck[6]++; //diagnal 1 (a1 to c3)
            }
            if (selectButtonIndex == 2 || selectButtonIndex == 4 || selectButtonIndex == 6)
            {
                playerCheck[7]++;//diagonal 2 (a3 to c1)
            }

            if (xTurn)
            {
                checkO = playerCheck;
            }
            else
            {
                checkX = playerCheck;
            }
        }

        public void GameOverCheck(int turnCount, int[] checkX3, int[] checkO3)
        {
            gameOver = false;
            int[] checkPlayer = checkX3;
            string player = "X";

            for (int i = 0; i < 2; i++)
            {
                if (i == 1)
                {
                    checkPlayer = checkO3;
                    player = "O";
                }
                if (checkPlayer.Contains(3))
                {
                    gameOver = true;
                    MessageBox.Show("Game Over. The player using : " + player + " won the game.");
                    return;
                }
                if (turnCount % 9 == 0)
                {
                    gameOver = true;
                    MessageBox.Show("Game Over. It was a draw.");
                    return;
                }
            }
        }
    }
}
