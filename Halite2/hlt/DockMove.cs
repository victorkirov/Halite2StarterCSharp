namespace Halite2.hlt
{

    public class DockMove : Move
    {

        private long destinationId;

        public DockMove(Ship ship, Planet planet)
            : base(MoveType.Dock, ship)
        {
            destinationId = planet.getId();
        }

        public long getDestinationId()
        {
            return destinationId;
        }
    }
}