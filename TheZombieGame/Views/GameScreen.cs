using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TheZombieGame.Models;
using TheZombieGame.Controllers;

namespace TheZombieGame.Views
{
    public partial class GameScreen : Form
    {
        private bool goLeft, goRight, goUp, goDown, gameOver;
        private int score;
        private List<PictureBox> zombiesList = new List<PictureBox>();
        private List<ZombieController> zombiesControllers = new List<ZombieController>();

        private PlayerModel playerModel;
        private PlayerController playerController;
        private HealthPackController healthPackController;
        private Random randNum = new Random();
        private int maxZombies = 3;

        private bool healthPackExists;
        private Timer zombieSpawnTimer;
        private const int maxZombieSpawnDelay = 1800; // Максимальная задержка в миллисекундах

        public GameScreen()
        {
            InitializeComponent();
            playerModel = new PlayerModel();
            playerController = new PlayerController(playerModel);
            healthPackController = new HealthPackController();

            zombieSpawnTimer = new Timer();
            zombieSpawnTimer.Tick += ZombieSpawnTimer_Tick;

            RestartGame();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (playerModel.Health > 1)
            {
                healthBar.Value = playerModel.Health;
            }
            else if (!gameOver)
            {
                gameOver = true;
                player.Image = Properties.Resources.dead1;
                GameTimer.Stop();
                zombieSpawnTimer.Stop();
                ShowGameOverMessage();
            }

            txtAmmo.Text = "Ammo: " + playerModel.Ammo;
            txtScore.Text = "Kills: " + score;

            playerController.UpdatePlayerPosition(player);

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "zombie")
                {
                    foreach (var zombieController in zombiesControllers)
                    {
                        zombieController.MoveZombies(player, x as PictureBox);
                    }

                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerModel.Health -= 1;
                    }
                }
            }

            healthPackController.CheckHealth(playerModel.Health, healthBar, this.Controls, healthPackExists, (exists) => healthPackExists = exists);

            // Проверка столкновений пуль с зомби
            List<Control> bulletsToRemove = new List<Control>();
            List<Control> zombiesToRemove = new List<Control>();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "bullet")
                {
                    foreach (PictureBox zombie in zombiesList)
                    {
                        if (x.Bounds.IntersectsWith(zombie.Bounds))
                        {
                            bulletsToRemove.Add(x);
                            zombiesToRemove.Add(zombie);
                            score++;
                            break;
                        }
                    }
                }
            }

            foreach (var bullet in bulletsToRemove)
            {
                this.Controls.Remove(bullet);
                bullet.Dispose();
            }

            foreach (var zombie in zombiesToRemove)
            {
                this.Controls.Remove(zombie);
                zombie.Dispose();
                zombiesList.Remove(zombie as PictureBox);
                zombiesControllers.RemoveAll(z => z.Equals(zombie));
                StartZombieSpawnTimer(); // Запускаем таймер для создания нового зомби
            }

            // Проверка столкновений игрока с патронами
            List<Control> ammoToRemove = new List<Control>();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "ammo")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerController.PickupAmmo(5);
                        ammoToRemove.Add(x);
                    }
                }
            }

            foreach (var ammo in ammoToRemove)
            {
                this.Controls.Remove(ammo);
                ammo.Dispose();
            }

            // Проверка столкновений игрока с аптечками
            List<Control> healthPacksToRemove = new List<Control>();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "health_Pack")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerModel.Health += 20;
                        if (playerModel.Health > 100) playerModel.Health = 100;
                        healthPacksToRemove.Add(x);
                    }
                }
            }

            foreach (var healthPack in healthPacksToRemove)
            {
                this.Controls.Remove(healthPack);
                healthPack.Dispose();
                healthPackExists = false;
            }
        }

        private void ShowGameOverMessage()
        {
            MessageBox.Show("Game Over! Press Enter to restart.", "The Zombie Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ZombieSpawnTimer_Tick(object sender, EventArgs e)
        {
            SpawnZombie();
            if (zombiesList.Count >= maxZombies)
            {
                zombieSpawnTimer.Stop();
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (gameOver)
            {
                return;
            }

            playerController.MovePlayer(e.KeyCode, true);
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (gameOver)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    RestartGame();
                }
                return;
            }

            playerController.MovePlayer(e.KeyCode, false);

            if (e.KeyCode == Keys.Space && playerModel.Ammo > 0)
            {
                playerModel.Ammo--;
                playerController.ShootBullet(this);

                if (playerModel.Ammo < 1)
                {
                    DropAmmo();
                }
            }
        }

        private void ShootBullet(string direction)
        {
            Bullet shootBullet = new Bullet
            {
                Direction = direction,
                BulletLeft = player.Left + (player.Width / 2),
                BulletTop = player.Top + (player.Height / 2)
            };
            shootBullet.MakeBullet(this);
        }

        private void MakeZombies(int count)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnZombie();
            }
        }

        private void SpawnZombie()
        {
            ZombieModel zombieModel = new ZombieModel();
            ZombieController zombieController = new ZombieController(zombieModel);
            zombiesControllers.Add(zombieController);

            PictureBox zombie = new PictureBox
            {
                Tag = "zombie",
                Image = Properties.Resources.zdown,
                Left = randNum.Next(0, this.ClientSize.Width - 50),
                Top = randNum.Next(0, this.ClientSize.Height - 50),
                SizeMode = PictureBoxSizeMode.AutoSize
            };

            zombiesList.Add(zombie);
            this.Controls.Add(zombie);
            player.BringToFront();
        }

        private void StartZombieSpawnTimer()
        {
            int delay = randNum.Next(maxZombieSpawnDelay);
            zombieSpawnTimer.Interval = delay;
            zombieSpawnTimer.Start();
        }

        private void RestartGame()
        {
            player.Image = Properties.Resources.up1;

            foreach (var zombie in zombiesList)
            {
                this.Controls.Remove(zombie);
            }

            zombiesControllers.Clear();
            zombiesList.Clear();

            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;
            gameOver = false;
            healthPackExists = false;

            playerModel.Health = 100;
            score = 0;
            playerModel.Ammo = 10;

            MakeZombies(maxZombies);
            GameTimer.Start();
        }

        private void DropAmmo()
        {
            PictureBox ammo = new PictureBox
            {
                Image = Properties.Resources.ammo_Image1,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Left = randNum.Next(10, this.ClientSize.Width - 50),
                Top = randNum.Next(60, this.ClientSize.Height - 50),
                Tag = "ammo"
            };

            this.Controls.Add(ammo);
            ammo.BringToFront();
            player.BringToFront();
        }
    }
}
