using System;

namespace Halite2.hlt {

    public class Position {

        private double xPos;
        private double yPos;

        public Position(double xPos, double yPos) {
            this.xPos = xPos;
            this.yPos = yPos;
        }

        public double getXPos() {
            return xPos;
        }

        public double getYPos() {
            return yPos;
        }

        public double getDistanceTo(Position target) {
            double dx = xPos - target.getXPos();
            double dy = yPos - target.getYPos();
            return Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
        }

        // TODO: remove
        public virtual double getRadius() {
            return 0;
        }

        public int orientTowardsInDeg(Position target) {
            return Util.angleRadToDegClipped(orientTowardsInRad(target));
        }

        public double orientTowardsInRad(Position target) {
            double dx = target.getXPos() - xPos;
            double dy = target.getYPos() - yPos;

            return Math.Atan2(dy, dx) + 2 * Math.PI;
        }

        public Position getClosestPoint(Position target) {
            double radius = target.getRadius() + Constants.MIN_DISTANCE;
            double angleRad = target.orientTowardsInRad(this);

            double dx = target.getXPos() + radius * Math.Cos(angleRad);
            double dy = target.getYPos() + radius * Math.Sin(angleRad);

            return new Position(dx, dy);
        }

        public bool equals(Object o) {
            if (this == o) 
                return true;            

            if (o == null || GetType() != o.GetType())
                return false;
            
            Position position = (Position)o;

            if (position == null)
                return false;

            return Equals(position.xPos, xPos) && Equals(position.yPos, yPos);
        }

        public int hashCode() {
            int result;
            long temp;
            temp = BitConverter.DoubleToInt64Bits(xPos);
            result = (int)(temp ^ (temp >> 32));
            temp = BitConverter.DoubleToInt64Bits(yPos);
            result = 31 * result + (int)(temp ^ (temp >> 32));

            return result;
        }

        public virtual string toString() {
            return "Position(" + xPos + ", " + yPos + ")";
        }
    }
}