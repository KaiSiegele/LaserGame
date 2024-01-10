using System;

namespace LaserGame
{
    public class Game
    {
        public void Play(Board board)
        {
            int step = 1;
            Positions actualPositions = new Positions();
            actualPositions.CalcutlateStartPosition(board);
            actualPositions.WritePositions(step);
            do
            {
                Positions nextPositions = new Positions();
                actualPositions.CalcuateNextPositions(board, nextPositions);
                actualPositions = nextPositions;
                step++;
                actualPositions.WritePositions(step);
            }
            while (!actualPositions.AtEnd(board.EndField));
        }
    }
}
