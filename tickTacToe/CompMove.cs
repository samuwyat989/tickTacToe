using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tickTacToe
{
    class CompMove
    {
        //public CompMove(int turnCount, )
        //{

        //}
        //public void CompMove(ref bool turn, Button selectButton)
        //{
        //    if (turnCount == 1)
        //    {
        //        if (a1Button.Enabled == false || c1Button.Enabled == false || a3Button.Enabled == false || c3Button.Enabled == false)
        //        {
        //            fourCorner = true;//player chose one of the corners, important for counter stategy
        //        }
        //        if (b2Button.Enabled == true)//Opening move
        //        {
        //            AddVal(b2Button, turn, checkX, checkO);
        //            ChangeButton(b2Button, ref turn);
        //        }
        //        else
        //        {
        //            AddVal(a1Button, turn, checkX, checkO);
        //            ChangeButton(a1Button, ref turn);
        //        }
        //    }
        //    if (turnCount == 2)//corner stop
        //    {
        //        if (c2Button.Enabled == false && b3Button.Enabled == false || a2Button.Enabled == false && b3Button.Enabled == false || b1Button.Enabled == false && c2Button.Enabled == false)
        //        {
        //            cornerStop = true;
        //        }
        //    }
        //    else
        //    {
        //        bool noThreat = false, compWin = false;
        //        string last3Char;

        //        for (int i = 0; i < checkX.Length; i++)//checks for each row/column/diagonal 
        //        {
        //            if (checkO[i] == 2 && checkX[i] == 0)//checks all row/column for O 
        //            {
        //                last3Char = checkOName[i].Substring(checkOName[i].Length - 3);//takes part of the row/col/diagonal, starting from the third last letter and ehding on the last letter
        //                FindButton(last3Char, ref turn);
        //                compWin = true;
        //                break;
        //            }
        //        }
        //        if (compWin == false)
        //        {
        //            for (int i = 0; i < checkX.Length; i++)//checks for each row/column/diagonal 
        //            {
        //                if (checkX[i] == 2 && checkO[i] == 0)//checks all row/column for 2 X  
        //                {
        //                    last3Char = checkXName[i].Substring(checkXName[i].Length - 3);//takes part of the row/col/diagonal, starting from the third last letter and ehding on the last letter
        //                    FindButton(last3Char, ref turn);
        //                    noThreat = false;
        //                    break;
        //                }
        //                noThreat = true;
        //            }
        //            if (noThreat)
        //            {
        //                if (fourCorner)
        //                {
        //                    CompPlayStyle(nonCorners, corners, ref turn);
        //                }
        //                else
        //                {
        //                    CompPlayStyle(corners, nonCorners, ref turn);
        //                }
        //            }
        //        }
        //    }
        //    turnCount++;
        //    EndCheck(checkX, checkO);
        //}

        //public void FindButton(string endString, ref bool turn)
        //{
        //    Char c;
        //    string endVal = endString.Substring(endString.Length - 1);

        //    for (int i = 1; i <= 3; i++)
        //    {
        //        if (endString.Contains("w"))//it is a row
        //        {
        //            c = (Char)((isCaps ? 65 : 97) + (i - 1)); //converts the current i value to a letter, ex. 1 = a, 2 = b ...
        //            if (buttonGroup[c.ToString() + endVal].Enabled == true) //uses the value above and the specified row number(endVal) to search the dictionary for the corresponding button. If enabled the button is the only left in the row that can be chosen.
        //            {
        //                AddVal(buttonGroup[c.ToString() + endVal], turn, checkX, checkO);
        //                ChangeButton(buttonGroup[c.ToString() + endVal], ref turn);
        //                break;
        //            }
        //        }
        //        else if (endString.Contains("al"))//it is a diagonal
        //        {
        //            c = (Char)((isCaps ? 65 : 97) + (i - 1));

        //            if (endString.Last().ToString() == "1")//diagonal 1
        //            {
        //                endVal = i.ToString();
        //                if (buttonGroup[c.ToString() + endVal].Enabled == true) // This goes from a1 to c3
        //                {
        //                    AddVal(buttonGroup[c.ToString() + endVal], turn, checkX, checkO);
        //                    ChangeButton(buttonGroup[c.ToString() + endVal], ref turn);
        //                    break;
        //                }
        //            }
        //            else//diagonal 2
        //            {
        //                endVal = (4 - i).ToString();
        //                if (buttonGroup[c.ToString() + endVal].Enabled == true) // This goes from a3 to c1
        //                {
        //                    AddVal(buttonGroup[c.ToString() + endVal], turn, checkX, checkO);
        //                    ChangeButton(buttonGroup[c.ToString() + endVal], ref turn);
        //                    break;
        //                }
        //            }
        //        }
        //        else//it is a column
        //        {
        //            if (buttonGroup[endVal.ToLower() + i.ToString()].Enabled == true)
        //            {
        //                AddVal(buttonGroup[endVal.ToLower() + i.ToString()], turn, checkX, checkO);
        //                ChangeButton(buttonGroup[endVal.ToLower() + i.ToString()], ref turn);
        //            }
        //        }
        //    }
        //}


        //public void CompPlayStyle(Button[] style1, Button[] style2, ref bool turn)
        //{
        //    bool skip = false;
        //    for (int i = 0; i < style1.Length; i++)
        //    {
        //        //need to corner stop (go c2 then b3 then b3. game.)
        //        if (cornerStop)
        //        {

        //        }
        //        else if (style1[i].Enabled == true)
        //        {
        //            AddVal(style1[i], turn, checkX, checkO);
        //            ChangeButton(style1[i], ref turn);
        //            skip = true;
        //            break;
        //        }
        //    }
        //    if (skip == false)
        //    {
        //        for (int i = 0; i < style2.Length; i++)
        //        {
        //            if (style2[i].Enabled == true)
        //            {
        //                AddVal(style2[i], turn, checkX, checkO);
        //                ChangeButton(style2[i], ref turn);
        //                break;
        //            }
        //        }
        //    }
        //}
    }
}
