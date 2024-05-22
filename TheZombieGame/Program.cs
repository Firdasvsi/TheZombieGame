using System;
using System.Windows.Forms;
using TheZombieGame.Views;

namespace TheZombieGame
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameMenu());
        }
    }
}
