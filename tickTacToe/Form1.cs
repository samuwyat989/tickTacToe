using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace tickTacToe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static int instructionNumber, formWidth, formHeight;
        public static bool playerVplayer, playerVcomp;
        public static SoundPlayer player = new SoundPlayer(Properties.Resources.moveSound);
        public static SoundPlayer selectPlayer = new SoundPlayer(Properties.Resources.selectSound);


        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            formWidth = this.Width;
            formHeight = this.Height;

            MenuScreen ms = new MenuScreen();
            this.Controls.Add(ms);
            ms.Location = new Point((formWidth - ms.Width) / 2, (formHeight - ms.Height) / 2);
        }
    }
}
