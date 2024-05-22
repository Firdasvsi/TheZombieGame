using System;
using System.Windows.Forms;


namespace TheZombieGame.Views
{
    public partial class GameMenu : Form
    {


        public GameMenu()
        {
            InitializeComponent();
            
        }

        private void btn_start_MouseHover(object sender, EventArgs e)
        {
            btn_start.Image = Properties.Resources.start_hover;
            System.Media.SoundPlayer sound = new System.Media.SoundPlayer(@"C:\Users\user\Desktop\TheZombieGame\TheZombieGame\Resources\background sound.wav");
            sound.Play();
        }

        private void btn_option_MouseHover(object sender, EventArgs e)
        {
            btn_option.Image = Properties.Resources.option_hover;
            System.Media.SoundPlayer sound = new System.Media.SoundPlayer(@"C:\Users\user\Desktop\TheZombieGame\TheZombieGame\Resources\background sound.wav");
            sound.Play();
        }

        private void btn_exit_MouseHover(object sender, EventArgs e)
        {
            btn_exit.Image = Properties.Resources.exit_hover;
            System.Media.SoundPlayer sound = new System.Media.SoundPlayer(@"C:\Users\user\Desktop\TheZombieGame\TheZombieGame\Resources\background sound.wav");
            sound.Play();
        }

        private void btn_start_MouseLeave(object sender, EventArgs e)
        {
            btn_start.Image = Properties.Resources.start_normal;
        }

        private void btn_option_MouseLeave(object sender, EventArgs e)
        {
            btn_option.Image = Properties.Resources.option_normal;
        }

        private void btn_exit_MouseLeave(object sender, EventArgs e)
        {
            btn_exit.Image = Properties.Resources.exit_normal;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            GameScreen gameScreen = new GameScreen();
            gameScreen.ShowDialog(this);
        }

        private void btn_option_Click(object sender, EventArgs e)
        {
            option_page option_Page = new option_page();
            option_Page.ShowDialog();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GameViews_Load(object sender, EventArgs e)
        {
            // Initial load logic if needed
        }
    }
}
