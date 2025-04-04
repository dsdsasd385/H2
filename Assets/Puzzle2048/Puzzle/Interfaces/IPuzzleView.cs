using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle2048
{
    public interface IPuzzleView
    {
        Vector2 GetBoardRect();
        Vector2 GetTilePadding();
        
        IEnumerator Initialize(int boardSize, Vector2 tileSize, Dictionary<Vector2Int, Vector2> tilePositions);
        IEnumerator SpawnTile (Vector2Int pos, int value);
        IEnumerator MoveTile  (List<MergeResult> mergeResult, List<MoveResult> moveResult);
        IEnumerator Restore(Dictionary<Vector2Int, int> current, Dictionary<Vector2Int, int> restore);
    }
}