namespace Halite2.hlt
{

    public class ThrustMove : Move
    {

        private int angleDeg;
        private int thrust;

        public ThrustMove(Ship ship, int angleDeg, int thrust)
            : base(MoveType.Thrust, ship)
        {
            this.thrust = thrust;
            this.angleDeg = angleDeg;
        }

        public int getAngle()
        {
            return angleDeg;
        }

        public int getThrust()
        {
            return thrust;
        }
    }
}
