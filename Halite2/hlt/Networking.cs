using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Halite2.hlt
{
    public class Networking
    {

        private static char UNDOCK_KEY = 'u';
        private static char DOCK_KEY = 'd';
        private static char THRUST_KEY = 't';

        public static void sendMoves(IEnumerable<Move> moves)
        {
            StringBuilder moveString = new StringBuilder();

            foreach (Move move in moves)
            {
                switch (move.getType())
                {
                    case Move.MoveType.Noop:
                        continue;
                    case Move.MoveType.Undock:
                        moveString.Append(UNDOCK_KEY)
                                .Append(" ")
                                .Append(move.getShip().getId())
                                .Append(" ");
                        break;
                    case Move.MoveType.Dock:
                        moveString.Append(DOCK_KEY)
                                .Append(" ")
                                .Append(move.getShip().getId())
                                .Append(" ")
                                .Append(((DockMove)move).getDestinationId())
                                .Append(" ");
                        break;
                    case Move.MoveType.Thrust:
                        moveString.Append(THRUST_KEY)
                                .Append(" ")
                                .Append(move.getShip().getId())
                                .Append(" ")
                                .Append(((ThrustMove)move).getThrust())
                                .Append(" ")
                                .Append(((ThrustMove)move).getAngle())
                                .Append(" ");
                        break;
                }
            }
            Console.WriteLine(moveString);
        }

        private static String readLine()
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                int buffer;

                for (; (buffer = Console.Read()) >= 0;) {
                    if (buffer == '\n')
                    {
                        break;
                    }
                    if (buffer == '\r')
                    {
                        // Ignore carriage return if on windows for manual testing.
                        continue;
                    }
                    builder = builder.Append((char)buffer);
                }
                return builder.ToString();
            }
            catch (Exception e)
            {
                Environment.Exit(0);
                return null;
            }
        }

        public static Metadata readLineIntoMetadata()
        {
            return new Metadata(readLine().Trim().Split(' '));
        }

        public GameMap initialize(String botName)
        {
            int myId = int.Parse(readLine());
            DebugLog.initialize(new StreamWriter(String.Format("{0}_{1}.log", myId, botName)));

            Metadata inputStringMapSize = readLineIntoMetadata();
            int width = int.Parse(inputStringMapSize.pop());
            int height = int.Parse(inputStringMapSize.pop());
            GameMap gameMap = new GameMap(width, height, myId);

            // Associate bot name
            Console.WriteLine(botName);

            Metadata inputStringMetadata = readLineIntoMetadata();
            gameMap.updateMap(inputStringMetadata);

            return gameMap;
        }
    }

}
