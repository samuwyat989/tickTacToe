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
    public partial class MenuScreen : UserControl
    {
        public MenuScreen()
        {
            InitializeComponent();
        }

        SolidBrush buttonBrush = new SolidBrush(Color.Gainsboro);
        SolidBrush selectBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush labelBrush = new SolidBrush(Color.Black);
        SolidBrush titleBrush = new SolidBrush(Color.White);

        Rectangle playerVplayerRect = new Rectangle();
        Rectangle playerVcompRect = new Rectangle();

        Rectangle[] selectButton = new Rectangle[2];
        Font selectFont = new Font("Calibri", 20);
        string selectLabel = "Player vs Player";

        int selectButtonIndex, selectX = 10;


        private void MenuScreen_Load(object sender, EventArgs e)
        {
            selectButtonIndex = 0;
            playerVplayerRect = new Rectangle((this.Width-200) / 2, 100, 200, 50);
            playerVcompRect = new Rectangle((this.Width - 200) / 2, 175, 200, 50);
            selectButton = new[] { playerVplayerRect, playerVcompRect };
            this.Focus();
        }

        private void MenuScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("Tick Tack Toe", new Font("Courier New", 35), titleBrush, 0, 0);

            e.Graphics.FillRectangle(selectBrush, selectButton[selectButtonIndex].X - 5, selectButton[selectButtonIndex].Y - 5, selectButton[selectButtonIndex].Width + 10, selectButton[selectButtonIndex].Height + 10);

            e.Graphics.FillRectangle(buttonBrush, playerVplayerRect);
            e.Graphics.FillRectangle(buttonBrush, playerVcompRect);

            e.Graphics.DrawString("Player vs Player", new Font("Calibri", 20), labelBrush, playerVplayerRect.X + 10, playerVplayerRect.Y+ 10);
            e.Graphics.DrawString("Player vs Computer", new Font("Calibri", 18), labelBrush, playerVcompRect.X + 2, playerVcompRect.Y + 10);

            e.Graphics.DrawString(selectLabel, selectFont, selectBrush, selectButton[selectButtonIndex].X + selectX, selectButton[selectButtonIndex].Y + 10);
        }

        private void MenuScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Down:
                    ChangeSelect(selectButtonIndex);
                    break;
                case Keys.Up:
                    ChangeSelect(selectButtonIndex);
                    break;
                case Keys.S:
                    ChangeSelect(selectButtonIndex);
                    break;
                case Keys.W:
                    ChangeSelect(selectButtonIndex);
                    break;
                case Keys.Escape:
                    {
                        Application.Exit();
                    }
                    break;
                case Keys.Space:
                    {
                        Form1.selectPlayer.PlaySync();

                        if (selectButtonIndex == 0)
                        {
                            Form1.playerVplayer = true;
                        }
                        else
                        {
                            Form1.playerVcomp = true;
                        }

                        Form f = this.FindForm();
                        f.Controls.Remove(this);
                        GameScreen gs = new GameScreen();
                        f.Controls.Add(gs);
                        gs.Location = new Point((Form1.formWidth - gs.Width) / 2, (Form1.formHeight - gs.Height) / 2);                     
                    }
                    break;
                case Keys.Z:
                    {
                        if (selectButtonIndex == 0)
                        {
                            Form1.playerVplayer = true;
                        }
                        else
                        {
                            Form1.playerVcomp = true;
                        }

                        Form f = this.FindForm();
                        f.Controls.Remove(this);
                        GameScreen gs = new GameScreen();
                        f.Controls.Add(gs);
                        gs.Location = new Point((Form1.formWidth - gs.Width) / 2, (Form1.formHeight - gs.Height) / 2);                        
                    }
                    break;
            }
            Refresh();
        }

        public void ChangeSelect(int selectIndex)
        {
            if (selectButtonIndex == 1)
            {
                selectButtonIndex = 0;
                selectFont = new Font("Calibri", 20);
                selectLabel = "Player vs Player";
                selectX = 10;
            }
            else if (selectButtonIndex == 0)
            {
                selectButtonIndex = 1;
                selectFont = new Font("Calibri", 18);
                selectLabel = "Player vs Computer";
                selectX = 2;
            }

            Form1.player.Play();
        }
    }
}
