using System;
using System.Collections.Generic;

namespace Halite2.hlt {
    public class MetadataParser {
        public static void populateShipList(List<Ship> shipsOutput, int owner, Metadata shipsMetadata) {
            long numberOfShips = long.Parse(shipsMetadata.pop());

            for (int i = 0; i < numberOfShips; ++i) {
                shipsOutput.Add(newShipFromMetadata(owner, shipsMetadata));
            }
        }

        private static Ship newShipFromMetadata(int owner, Metadata metadata) {
            int id = int.Parse(metadata.pop());
            double xPos = double.Parse(metadata.pop());
            double yPos = double.Parse(metadata.pop());
            int health = int.Parse(metadata.pop());

            // Ignoring velocity(x,y) which is always (0,0) in current version.
            metadata.pop();
            metadata.pop();

            Ship.DockingStatus dockingStatus = (Ship.DockingStatus)int.Parse(metadata.pop());
            int dockedPlanet = int.Parse(metadata.pop());
            int dockingProgress = int.Parse(metadata.pop());
            int weaponCooldown = int.Parse(metadata.pop());

            return new Ship(owner, id, xPos, yPos, health, dockingStatus, dockedPlanet, dockingProgress, weaponCooldown);
        }

        public static Planet newPlanetFromMetadata(List<int> dockedShips, Metadata metadata) {
            int id = int.Parse(metadata.pop());
            double xPos = double.Parse(metadata.pop());
            double yPos = double.Parse(metadata.pop());
            int health = int.Parse(metadata.pop());

            double radius = double.Parse(metadata.pop());
            int dockingSpots = int.Parse(metadata.pop());
            int currentProduction = int.Parse(metadata.pop());
            int remainingProduction = int.Parse(metadata.pop());

            int hasOwner = int.Parse(metadata.pop());
            int ownerCandidate = int.Parse(metadata.pop());
            int owner;
            if (hasOwner == 1) {
                owner = ownerCandidate;
            } else {
                owner = -1; // ignore ownerCandidate
            }

            int dockedShipCount = int.Parse(metadata.pop());
            for (int i = 0; i < dockedShipCount; ++i) {
                dockedShips.Add(int.Parse(metadata.pop()));
            }

            return new Planet(owner, id, xPos, yPos, health, radius, dockingSpots,
                              currentProduction, remainingProduction, dockedShips);
        }

        public static int parsePlayerNum(Metadata metadata) {
            return int.Parse(metadata.pop());
        }

        public static int parsePlayerId(Metadata metadata) {
            return int.Parse(metadata.pop());
        }
    }
}
