using UnityEngine;

namespace Puzzle2048
{
    public interface IPuzzleRule
    {
        int GetBoardSize();
        
        bool IsGameWin(int totalMovedCount);
        
        bool ShouldRemoveTile(Vector2Int position, int value);
    }
}