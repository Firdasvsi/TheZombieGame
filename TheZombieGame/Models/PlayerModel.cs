namespace TheZombieGame.Models
{
    public class PlayerModel
    {
        public int Health { get; set; } = 100;
        public int Speed { get; set; } = 10;
        public string Facing { get; set; } = "up";
        public int Ammo { get; set; } = 10;
        public bool IsDead { get; set; } = false;

        public void MoveLeft()
        {
            Facing = "left";
        }

        public void MoveRight()
        {
            Facing = "right";
        }

        public void MoveUp()
        {
            Facing = "up";
        }

        public void MoveDown()
        {
            Facing = "down";
        }

        public string Direction
        {
            get { return Facing; }
        }

        public void ResetPlayer()
        {
            Health = 100;
            Ammo = 10;
            IsDead = false;
            Facing = "up";
        }
    }
}
