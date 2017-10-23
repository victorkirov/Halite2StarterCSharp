namespace Halite2.hlt
{

    public class Entity : Position
    {

        private int owner;
        private int id;
        private int health;
        private double radius;

        public Entity(int owner, int id, double xPos, double yPos, int health, double radius)
            : base(xPos, yPos)
        {
            this.owner = owner;
            this.id = id;
            this.health = health;
            this.radius = radius;
        }

        public int getOwner()
        {
            return owner;
        }

        public int getId()
        {
            return id;
        }

        public int getHealth()
        {
            return health;
        }

        public override double getRadius()
        {
            return radius;
        }

        public override string toString()
        {
            return "Entity[" +
                    base.toString() +
                    ", owner=" + owner +
                    ", id=" + id +
                    ", health=" + health +
                    ", radius=" + radius +
                    "]";
        }
    }
}
