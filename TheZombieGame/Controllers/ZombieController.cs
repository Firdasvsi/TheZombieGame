using TheZombieGame.Models;
using System.Windows.Forms;

namespace TheZombieGame.Controllers
{
    public class ZombieController
    {
        private ZombieModel zombieModel;

        public ZombieController(ZombieModel model)
        {
            zombieModel = model;
        }

        public void MoveZombies(Control player, Control zombie)
        {
            int zombieLeft = zombie.Left;
            int zombieTop = zombie.Top;

            // Используем скорость зомби из модели
            zombieModel.MoveTowardsPlayer(player.Left, player.Top, ref zombieLeft, ref zombieTop);
            zombie.Left = zombieLeft;
            zombie.Top = zombieTop;
        }
    }
}
