using UnityEngine;

namespace Puzzle2048
{
    public interface IPuzzleRule
    {
        int GetBoardSize();
        int GetTotalMoveCount();
        bool ShouldRemoveTile(Vector2Int position, int value);
    }
}