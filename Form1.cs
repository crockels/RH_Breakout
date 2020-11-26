using System;

using System.Collections.Generic;

using System.ComponentModel;

using System.Data;

using System.Drawing;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.Windows.Forms;



namespace breakout_game
{

    public partial class Form1 : Form
    {
        bool go_left = false, go_right = false;
        int speed = 10;
        int ball_x = 5;
        int ball_y = 5;
        int score = 0;

        private Random rand = new Random();

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && player.Left > 0) { go_left = true; }
            if (e.KeyCode == Keys.Right && player.Left + player.Width < 920) { go_right = true; }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { go_left = false; }
            if (e.KeyCode == Keys.Right) { go_right = false; }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ball.Left += ball_x;
            ball.Top += ball_y;

            label1.Text = "Score: " + score;

            if (go_left && player.Left > 0) { player.Left -= speed; }

            if (go_right && player.Left > 700) { player.Left += speed; }

            if (player.Left < 0) { go_left = false; }

            else if (player.Left + player.Width > 920) { go_right = false; }

            if (ball.Left + ball.Width > ClientSize.Width || ball.Left < 0) { ball_x = -ball_x; }

            if (ball.Top < 0 || ball.Bounds.IntersectsWith(player.Bounds)) { ball_y = -ball_y; }

            if (ball.Top + ball.Height > ClientSize.Height) { game_over(); }

            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && x.Tag == "block")
                {

                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {

                        this.Controls.Remove(x);

                        ball_y = -ball_y; score++;

                    }

                }

            }

            if (score >= 35) { game_over(); MessageBox.Show("you win"); }

        }

        public Form1()
        {

            InitializeComponent();

            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && x.Tag == "block")
                {

                    Color randomColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));

                    x.BackColor = randomColor;

                }

            }

        }

        private void game_over()
        {

            timer1.Stop();

        }

    }

}