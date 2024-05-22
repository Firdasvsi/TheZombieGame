using System;
using System.Drawing;
using System.Windows.Forms;
using TheZombieGame.Models;

namespace TheZombieGame.Controllers
{
    public class HealthPackController
    {
        private HealthPackModel healthPackModel;
        private Random randNum;

        public HealthPackController()
        {
            healthPackModel = new HealthPackModel();
            randNum = new Random();
        }

        public void CheckHealth(int playerHealth, ProgressBar healthBar, Control.ControlCollection controls, bool healthPackExists, Action<bool> setHealthPackExists)
        {
            if ((float)playerHealth / healthBar.Maximum <= 0.45f && !healthPackExists)
            {
                // Создаем экземпляр HealthPack
                PictureBox healthPack = new PictureBox
                {
                    Image = Properties.Resources.health_Pack,
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Tag = "health_Pack",
                    Left = randNum.Next(10, controls.Owner.ClientSize.Width - 50),
                    Top = randNum.Next(60, controls.Owner.ClientSize.Height - 50)
                };

                // Добавляем healthPack на форму и приводим его на передний план
                controls.Add(healthPack);
                healthPack.BringToFront();

                setHealthPackExists(true); // Устанавливаем флаг в true, чтобы не создавать новую аптечку
            }
        }
    }
}
