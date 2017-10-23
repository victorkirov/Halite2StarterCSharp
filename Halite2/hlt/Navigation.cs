using System;
using System.Linq;

namespace Halite2.hlt
{
    public class Navigation
    {
        public static ThrustMove navigateShipToDock(
                GameMap gameMap,
                Ship ship,
                Entity dockTarget,
                int maxThrust)
        {
            int maxCorrections = Constants.MAX_NAVIGATION_CORRECTIONS;
            bool avoidObstacles = true;
            double angularStepRad = Math.PI / 180.0;
            Position targetPos = ship.getClosestPoint(dockTarget);

            return navigateShipTowardsTarget(gameMap, ship, targetPos, maxThrust, avoidObstacles, maxCorrections, angularStepRad);
        }

        public static ThrustMove navigateShipTowardsTarget(
                GameMap gameMap,
                Ship ship,
                Position targetPos,
                int maxThrust,
                bool avoidObstacles,
                int maxCorrections,
                double angularStepRad)
        {
            if (maxCorrections <= 0)
            {
                return null;
            }

            double distance = ship.getDistanceTo(targetPos);
            double angleRad = ship.orientTowardsInRad(targetPos);

            if (avoidObstacles && gameMap.objectsBetween(ship, targetPos).Any())
            {
                double newTargetDx = Math.Cos(angleRad + angularStepRad) * distance;
                double newTargetDy = Math.Sin(angleRad + angularStepRad) * distance;
                Position newTarget = new Position(ship.getXPos() + newTargetDx, ship.getYPos() + newTargetDy);

                return navigateShipTowardsTarget(gameMap, ship, newTarget, maxThrust, true, (maxCorrections - 1), angularStepRad);
            }

            int thrust;
            if (distance < maxThrust)
            {
                // Do not round up, since overshooting might cause collision.
                thrust = (int)distance;
            }
            else
            {
                thrust = maxThrust;
            }

            int angleDeg = Util.angleRadToDegClipped(angleRad);

            return new ThrustMove(ship, angleDeg, thrust);
        }
    }
}
