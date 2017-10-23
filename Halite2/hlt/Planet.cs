using System.Collections.Generic;

namespace Halite2.hlt {
    public class Planet: Entity {

        private int remainingProduction;
        private int currentProduction;
        private int dockingSpots;
        private IList<int> dockedShips;

        public Planet(int owner, int id, double xPos, double yPos, int health,
                      double radius, int dockingSpots, int currentProduction,
                      int remainingProduction, List<int> dockedShips)
        :base(owner, id, xPos, yPos, health, radius)
        {
            this.dockingSpots = dockingSpots;
            this.currentProduction = currentProduction;
            this.remainingProduction = remainingProduction;
            this.dockedShips = dockedShips.AsReadOnly();
        }

        public int getRemainingProduction() {
            return remainingProduction;
        }

        public int getCurrentProduction() {
            return currentProduction;
        }

        public int getDockingSpots() {
            return dockingSpots;
        }

        public IList<int> getDockedShips() {
            return dockedShips;
        }

        public bool isFull() {
            return dockedShips.Count == dockingSpots;
        }

        public bool isOwned() {
            return getOwner() != -1;
        }
        
        public override string toString() {
            return "Planet[" +
                    base.toString() +
                    ", remainingProduction=" + remainingProduction +
                    ", currentProduction=" + currentProduction +
                    ", dockingSpots=" + dockingSpots +
                    ", dockedShips=" + dockedShips +
                    "]";
        }
    }
}