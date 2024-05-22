using System.Drawing;
using System.Windows.Forms;
using TheZombieGame.Models;

namespace TheZombieGame.Controllers
{
    public class AmmoController
    {
        private AmmoModel ammoModel;

        public AmmoController(AmmoModel model)
        {
            ammoModel = model;
        }

        public void SetAmmoPosition(int left, int top)
        {
            ammoModel.Left = left;
            ammoModel.Top = top;
        }

        public PictureBox CreateAmmoPictureBox()
        {
            PictureBox ammo = new PictureBox
            {
                Image = Properties.Resources.ammo_Image1, // Убедитесь, что у вас есть изображение для патронов
                SizeMode = PictureBoxSizeMode.AutoSize,
                Left = ammoModel.Left,
                Top = ammoModel.Top,
                Tag = "ammo"
            };
            return ammo;
        }
    }
}
