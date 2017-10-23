using System.Collections.Generic;

namespace Halite2.hlt {
    public class Player {

        private Dictionary<int, Ship> ships;
        private int id;

        public Player(int id, Dictionary<int, Ship> ships) {
            this.id = id;
            this.ships = ships;
        }

        public IDictionary<int, Ship> getShips() {
            return ships;
        }

        public Ship getShip(int entityId) {
            return ships[entityId];
        }

        public int getId() {
            return id;
        }
    }
}