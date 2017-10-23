using Halite2.hlt;
using System.Collections.Generic;

namespace Halite2
{
    public class MyBot
    {

        public static void Main(string[] args)
        {
            string name = args.Length > 0 ? args[0] : "Tamagocchi";

            Networking networking = new Networking();
            GameMap gameMap = networking.initialize(name);

            List<Move> moveList = new List<Move>();
            for (; ; )
            {
                moveList.Clear();
                gameMap.updateMap(Networking.readLineIntoMetadata());

                foreach (Ship ship in gameMap.getMyPlayer().getShips().Values)
                {
                    if (ship.getDockingStatus() != Ship.DockingStatus.Undocked)
                    {
                        continue;
                    }

                    foreach (Planet planet in gameMap.getAllPlanets().Values)
                    {
                        if (planet.isOwned())
                        {
                            continue;
                        }

                        if (ship.canDock(planet))
                        {
                            moveList.Add(new DockMove(ship, planet));
                            break;
                        }

                        ThrustMove newThrustMove = Navigation.navigateShipToDock(gameMap, ship, planet, Constants.MAX_SPEED / 2);
                        if (newThrustMove != null)
                        {
                            moveList.Add(newThrustMove);
                        }

                        break;
                    }
                }
                Networking.sendMoves(moveList);
            }
        }
    }
}
