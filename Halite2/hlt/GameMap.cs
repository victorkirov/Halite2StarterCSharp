using System;
using System.Collections.Generic;
using System.Linq;

namespace Halite2.hlt {

    public class GameMap {
        private int width, height;
        private int playerId;
        private List<Player> players;
        private IList<Player> playersUnmodifiable;
        private Dictionary<int, Planet> planets;
        private List<Ship> allShips;
        private IList<Ship> allShipsUnmodifiable;

        // used only during parsing to reduce memory allocations
        private List<Ship> currentShips = new List<Ship>();

        public GameMap(int width, int height, int playerId) {
            this.width = width;
            this.height = height;
            this.playerId = playerId;
            players = new List<Player>(Constants.MAX_PLAYERS);
            playersUnmodifiable = players.AsReadOnly();
            planets = new Dictionary<int, Planet>();
            allShips = new List<Ship>();
            allShipsUnmodifiable = allShips.AsReadOnly();
        }

        public int getHeight() {
            return height;
        }

        public int getWidth() {
            return width;
        }

        public int getMyPlayerId() {
            return playerId;
        }

        public IList<Player> getAllPlayers() {
            return playersUnmodifiable;
        }

        public Player getMyPlayer() {
            return playersUnmodifiable[getMyPlayerId()];
        }

        public Ship getShip(int playerId, int entityId) {
            return players[playerId].getShip(entityId);
        }

        public Planet getPlanet(int entityId) {
            return planets[entityId];
        }

        public Dictionary<int, Planet> getAllPlanets() {
            return planets;
        }

        public IList<Ship> getAllShips() {
            return allShipsUnmodifiable;
        }

        public List<Entity> objectsBetween(Position start, Position target) {
            List<Entity> entitiesFound = new List<Entity>();

            addEntitiesBetween(entitiesFound, start, target, planets.Values.ToList<Entity>());
            addEntitiesBetween(entitiesFound, start, target, allShips.ToList<Entity>());

            return entitiesFound;
        }

        private static void addEntitiesBetween(List<Entity> entitiesFound,
                                               Position start, Position target,
                                               ICollection<Entity> entitiesToCheck) {

            foreach (Entity entity in entitiesToCheck) {
                if (entity.equals(start) || entity.equals(target)) {
                    continue;
                }
                if (Collision.segmentCircleIntersect(start, target, entity, Constants.FORECAST_FUDGE_FACTOR)) {
                    entitiesFound.Add(entity);
                }
            }
        }

        public Dictionary<double, Entity> nearbyEntitiesByDistance(Entity entity) {
            Dictionary<double, Entity> entityByDistance = new Dictionary<double, Entity>();

            foreach (Planet planet in planets.Values) {
                if (planet.equals(entity)) {
                    continue;
                }
                entityByDistance[entity.getDistanceTo(planet)] = planet;
            }

            foreach (Ship ship in allShips) {
                if (ship.equals(entity)) {
                    continue;
                }
                entityByDistance[entity.getDistanceTo(ship)] = ship;
            }

            return entityByDistance;
        }

        public GameMap updateMap(Metadata mapMetadata) {
            int numberOfPlayers = MetadataParser.parsePlayerNum(mapMetadata);

            players.Clear();
            planets.Clear();
            allShips.Clear();

            // update players info
            for (int i = 0; i < numberOfPlayers; ++i) {
                currentShips.Clear();
                Dictionary<int, Ship> currentPlayerShips = new Dictionary<int, Ship>();
                int playerId = MetadataParser.parsePlayerId(mapMetadata);

                Player currentPlayer = new Player(playerId, currentPlayerShips);
                MetadataParser.populateShipList(currentShips, playerId, mapMetadata);
                allShips.AddRange(currentShips);

                foreach (Ship ship in currentShips) {
                    currentPlayerShips[ship.getId()] = ship;
                }
                players.Add(currentPlayer);
            }

            int numberOfPlanets = int.Parse(mapMetadata.pop());

            for (int i = 0; i < numberOfPlanets; ++i) {
                List<int> dockedShips = new List<int>();
                Planet planet = MetadataParser.newPlanetFromMetadata(dockedShips, mapMetadata);
                planets[planet.getId()] = planet;
            }

            if (!mapMetadata.isEmpty()) {
                throw new InvalidOperationException("Failed to parse data from Halite game engine. Please contact maintainers.");
            }

            return this;
        }
    }

}
