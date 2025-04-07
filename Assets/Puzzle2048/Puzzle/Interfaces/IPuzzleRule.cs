using UnityEngine;

namespace Puzzle2048
{
    public interface IPuzzleRule
    {
        int GetBoardSize();
        int GetTotalMoveCount();
        int GetPointPerRemoveTile();
        bool ShouldRemoveTile(Vector2Int position, int value);
    }
}