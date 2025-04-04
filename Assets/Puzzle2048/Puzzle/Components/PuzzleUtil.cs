using System.Collections.Generic;
using UnityEngine;

namespace Puzzle2048
{
    public static class PuzzleUtil
    {
        public static Vector2 GetCellSize(Vector2 boardRect, int boardSize, Vector2 padding)
        {
            Vector2 totalPadding = new Vector2(padding.x * (boardSize - 1), padding.y * (boardSize - 1));
            float width = (boardRect.x - totalPadding.x) / boardSize;
            float height = (boardRect.y - totalPadding.y) / boardSize;

            return new Vector2(width, height);
        }

        public static Vector2 GetCellPosition(Vector2 boardRect, int boardSize, Vector2 padding, Vector2Int index)
        {
            var cellSize = GetCellSize(boardRect, boardSize, padding);
            var start = new Vector2(
                -boardRect.x / 2f + cellSize.x / 2f,
                -boardRect.y / 2f + cellSize.y / 2f
            );

            float posX = start.x + index.x * (cellSize.x + padding.x);
            float posY = start.y + index.y * (cellSize.y + padding.y);

            return new Vector2(posX, posY);
        }

        public static Dictionary<Vector2Int, Vector2> GetAllCellPositions(Vector2 boardRect, int boardSize, Vector2 padding)
        {
            var map = new Dictionary<Vector2Int, Vector2>();
            for (int x = 0; x < boardSize; x++)
            for (int y = 0; y < boardSize; y++)
            {
                var pos = new Vector2Int(x, y);
                map[pos] = GetCellPosition(boardRect, boardSize, padding, pos);
            }
            return map;
        }
    }
}