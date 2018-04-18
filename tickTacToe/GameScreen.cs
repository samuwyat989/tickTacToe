using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace tickTacToe
{
    public partial class GameScreen : UserControl
    {
        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        #region Global Variables
        List<PlayerMove> playerMoveDB = new List<PlayerMove>();
        
        
        //Sets up the drawing of the buttons
        const int BUTTON_SIZE = 50;
        const int BUFFER_SIZE = 20;
        const int START_X = 100;
        const int START_Y = 100;

        Rectangle menuButton = new Rectangle();
        SolidBrush menuBrush = new SolidBrush(Color.Black);

        Rectangle a1Button = new Rectangle();
        Rectangle a2Button = new Rectangle();
        Rectangle a3Button = new Rectangle();
        Rectangle b1Button = new Rectangle();
        Rectangle b2Button = new Rectangle();
        Rectangle b3Button = new Rectangle();
        Rectangle c1Button = new Rectangle();
        Rectangle c2Button = new Rectangle();
        Rectangle c3Button = new Rectangle();

        List<Rectangle> allButtons = new List<Rectangle>();
        Rectangle[] corners, nonCorners;
        int selectButtonIndex = 0;

        bool xTurn = true, fourCorner, cornerStop, isCaps = false;
        int turnCount = 0, Xrow1, Xrow2, Xrow3, XcolA, XcolB, XcolC, Xdiagonal1, Xdiagonal2,
                           Orow1, Orow2, Orow3, OcolA, OcolB, OcolC, Odiagonal1, Odiagonal2;
        int[] checkX, checkO;
        string[] checkXName, checkOName;
       // Button[] corners, nonCorners;
        Dictionary<int, bool> buttonGroup = new Dictionary<int, bool>();
        Dictionary<string, int> buttonCheck = new Dictionary<string, int>();
        #endregion

        public void OnStart()
        {
            allButtons.AddRange(new[] { a1Button, a2Button, a3Button, b1Button, b2Button, b3Button, c1Button, c2Button, c3Button, menuButton });
            Char c;
            int rowReset = 0;
            int yOffSet = 0;

            for (int i = 0; i < allButtons.Count - 1; i++)
            {
                if (i % 3 == 0 && i != 0)
                {
                    rowReset = 0;
                    yOffSet++;
                }
                allButtons[i] = new Rectangle(START_X + rowReset * (BUTTON_SIZE + BUFFER_SIZE), START_Y + yOffSet * (BUTTON_SIZE + BUFFER_SIZE), BUTTON_SIZE, BUTTON_SIZE);
                rowReset++;

                c = (Char)((isCaps ? 65 : 97) + yOffSet);
                buttonCheck.Add(c.ToString() + rowReset.ToString(), i);
                buttonGroup.Add(i, true);
            }

            allButtons[9] = new Rectangle(allButtons[0].X, allButtons[8].Y + BUTTON_SIZE+ 2* BUFFER_SIZE, 3*BUTTON_SIZE+2*BUFFER_SIZE, BUTTON_SIZE);
            Reset();
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.DodgerBlue, allButtons[selectButtonIndex].X - 5, allButtons[selectButtonIndex].Y - 5, allButtons[selectButtonIndex].Width + 10, allButtons[selectButtonIndex].Height + 10);

            foreach (Rectangle r in allButtons)
            {
                e.Graphics.FillRectangle(Brushes.Gainsboro, r);
            }

            e.Graphics.DrawString("Menu", new Font("Calibri", 20), menuBrush, allButtons[9].X+allButtons[9].Width/4+10, allButtons[9].Y+10);

            int xOffest = 0;

            foreach(PlayerMove pm in playerMoveDB)
            {
                if(pm.xoVal == "X") { xOffest = 2; }
                else { xOffest = 0; }
                e.Graphics.DrawString(pm.xoVal, new Font("Calibri", 20), Brushes.Black, allButtons[pm.selectButtonIndex].X + BUTTON_SIZE/4 + xOffest, allButtons[pm.selectButtonIndex].Y+10);
            }
            this.Focus();
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    ChangeSelect("right");
                    break;
                case Keys.Left:
                    ChangeSelect("left");
                    break;
                case Keys.Space:
                    if(selectButtonIndex == 9)
                    {
                        ReturnToMenu();
                    }
                    else
                    {
                        if (buttonGroup[selectButtonIndex])
                        {
                            NewMove();
                            Form1.selectPlayer.Play();
                        }
                    }
                    break;
                case Keys.Escape:
                    {
                        Application.Exit();
                    }
                    break;
            }
            Refresh();
        }

        public void ChangeSelect(string direction)
        {
            if(direction == "right")
            {
                if (selectButtonIndex == 9)
                {
                    selectButtonIndex = 0;
                }
                else
                {
                    selectButtonIndex++;
                }
            }
            else
            {
                if (selectButtonIndex == 0)
                {
                    selectButtonIndex = 9;
                }
                else
                {
                    selectButtonIndex--;
                }
            }


            if(selectButtonIndex == 9)
            {
                menuBrush = new SolidBrush(Color.DodgerBlue);
            }
            else
            {
                menuBrush = new SolidBrush(Color.Black);
            }

            Form1.player.Play();
        }

        public void NewMove()
        {
            PlayerMove pm = new PlayerMove(ref xTurn, selectButtonIndex);
            playerMoveDB.Add(pm);
            pm.UpdateBoardValues(selectButtonIndex, ref checkX, ref checkO);
            turnCount++;
            buttonGroup[selectButtonIndex] = false;

            pm.GameOverCheck(turnCount, checkX, checkO);
            if (pm.gameOver)
            {
                ReturnToMenu();
                return;
            }

            if (Form1.playerVcomp)
            {
                PlayerMove compMove = new PlayerMove(xTurn, turnCount, checkX, checkO, buttonGroup, buttonCheck, ref fourCorner, ref cornerStop,
                    checkXName, checkOName, isCaps, allButtons, corners, nonCorners);


                PlayerMove translateCompMove = new PlayerMove(ref compMove.xTurn, compMove.selectButtonIndex);
                xTurn = compMove.xTurn;
                playerMoveDB.Add(translateCompMove);
                translateCompMove.UpdateBoardValues(compMove.selectButtonIndex, ref checkX, ref checkO);
                turnCount++;
                buttonGroup[translateCompMove.selectButtonIndex] = false;
                pm.GameOverCheck(turnCount, checkX, checkO);
                if (pm.gameOver)
                {
                    ReturnToMenu();
                }
            }
        }
        public void ReturnToMenu()
        {
            Form f = this.FindForm();
            f.Controls.Remove(this);
            MenuScreen ms = new MenuScreen();
            f.Controls.Add(ms);
            ms.Location = new Point((Form1.formWidth - ms.Width) / 2, (Form1.formHeight - ms.Height) / 2);
        }

        public void Reset()
        {
            turnCount = Xrow1 = Xrow2 = Xrow3 = XcolA = XcolB = XcolC = Xdiagonal1 = Xdiagonal2 = Orow1 = Orow2 = Orow3 = OcolA = OcolB = OcolC = Odiagonal1 = Odiagonal2 = 0;
            xTurn = true;
            fourCorner = false;
            playerMoveDB.Clear();
            corners = new[] { allButtons[0], allButtons[2], allButtons[6], allButtons[8] };
            nonCorners = new[] { allButtons[3], allButtons[7], allButtons[5], allButtons[1] };
            checkXName = new string[] { "Xrow1", "Xrow2", "Xrow3", "XcolA", "XcolB", "XcolC", "Xdiagonal1", "Xdiagonal2" };
            checkOName = new string[] { "Orow1", "Orow2", "Orow3", "OcolA", "OcolB", "OcolC", "Odiagonal1", "Odiagonal2" };
            checkX = new int[] { Xrow1, Xrow2, Xrow3, XcolA, XcolB, XcolC, Xdiagonal1, Xdiagonal2 };
            checkO = new int[] { Orow1, Orow2, Orow3, OcolA, OcolB, OcolC, Odiagonal1, Odiagonal2 };
        }
    }
}
