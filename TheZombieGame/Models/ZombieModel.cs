namespace TheZombieGame.Models
{
    public class ZombieModel
    {
        public int Speed { get; set; }

        public ZombieModel()
        {
            Speed = 1; // Начальная скорость зомби
        }

        public void MoveTowardsPlayer(int playerX, int playerY, ref int zombieX, ref int zombieY)
        {
            if (zombieX < playerX)
            {
                zombieX += Speed;
            }
            else if (zombieX > playerX)
            {
                zombieX -= Speed;
            }

            if (zombieY < playerY)
            {
                zombieY += Speed;
            }
            else if (zombieY > playerY)
            {
                zombieY -= Speed;
            }
        }
    }
}
