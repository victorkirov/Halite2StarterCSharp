namespace Halite2.hlt
{
    public class Ship : Entity
    {
        public enum DockingStatus { Undocked = 0, Docking = 1, Docked = 2, Undocking = 3 }

        private DockingStatus dockingStatus;
        private int dockedPlanet;
        private int dockingProgress;
        private int weaponCooldown;

        public Ship(int owner, int id, double xPos, double yPos,
                    int health, DockingStatus dockingStatus, int dockedPlanet,
                    int dockingProgress, int weaponCooldown)
            : base(owner, id, xPos, yPos, health, Constants.SHIP_RADIUS)
        {
            this.dockingStatus = dockingStatus;
            this.dockedPlanet = dockedPlanet;
            this.dockingProgress = dockingProgress;
            this.weaponCooldown = weaponCooldown;
        }

        public int getWeaponCooldown()
        {
            return weaponCooldown;
        }

        public DockingStatus getDockingStatus()
        {
            return dockingStatus;
        }

        public int getDockingProgress()
        {
            return dockingProgress;
        }

        public int getDockedPlanet()
        {
            return dockedPlanet;
        }

        public bool canDock(Planet planet)
        {
            return getDistanceTo(planet) <= Constants.DOCK_RADIUS + planet.getRadius();
        }

        public override string toString()
        {
            return "Ship[" +
                    base.toString() +
                    ", dockingStatus=" + dockingStatus +
                    ", dockedPlanet=" + dockedPlanet +
                    ", dockingProgress=" + dockingProgress +
                    ", weaponCooldown=" + weaponCooldown +
                    "]";
        }
    }
}
