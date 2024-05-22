using TheZombieGame.Models;
using System.Windows.Forms;

namespace TheZombieGame.Controllers
{
    public class PlayerController
    {
        private PlayerModel playerModel;

        public PlayerController(PlayerModel model)
        {
            playerModel = model;
        }

        public void MovePlayer(Keys key, bool isKeyDown)
        {
            if (isKeyDown)
            {
                switch (key)
                {
                    case Keys.Left:
                        playerModel.MoveLeft();
                        break;
                    case Keys.Right:
                        playerModel.MoveRight();
                        break;
                    case Keys.Up:
                        playerModel.MoveUp();
                        break;
                    case Keys.Down:
                        playerModel.MoveDown();
                        break;
                }
            }
        }

        public void UpdatePlayerPosition(PictureBox player)
        {
            if (playerModel.IsDead)
            {
                player.Image = Properties.Resources.dead1;
                return;
            }

            switch (playerModel.Facing)
            {
                case "left":
                    if (player.Left > 0)
                    {
                        player.Left -= playerModel.Speed;
                    }
                    player.Image = Properties.Resources.left;
                    break;
                case "right":
                    if (player.Left + player.Width < player.Parent.ClientSize.Width)
                    {
                        player.Left += playerModel.Speed;
                    }
                    player.Image = Properties.Resources.right;
                    break;
                case "up":
                    if (player.Top > 45)
                    {
                        player.Top -= playerModel.Speed;
                    }
                    player.Image = Properties.Resources.up1;
                    break;
                case "down":
                    if (player.Top + player.Height < player.Parent.ClientSize.Height)
                    {
                        player.Top += playerModel.Speed;
                    }
                    player.Image = Properties.Resources.down;
                    break;
            }
        }

        public void ShootBullet(Form form)
        {
            Bullet bullet = new Bullet
            {
                Direction = playerModel.Facing,
                BulletLeft = form.Controls["player"].Left + (form.Controls["player"].Width / 2),
                BulletTop = form.Controls["player"].Top + (form.Controls["player"].Height / 2)
            };
            bullet.MakeBullet(form);
        }

        public void PickupAmmo(int ammoAmount)
        {
            playerModel.Ammo += ammoAmount;
        }

        public void TakeDamage(int damage)
        {
            playerModel.Health -= damage;
            if (playerModel.Health <= 0)
            {
                playerModel.IsDead = true;
            }
        }

        public void ResetPlayer()
        {
            playerModel.Health = 100;
            playerModel.Ammo = 10;
            playerModel.IsDead = false;
            playerModel.Facing = "up";
        }
    }
}
