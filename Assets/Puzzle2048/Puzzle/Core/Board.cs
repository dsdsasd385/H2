using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Puzzle2048
{
    public class Board
    {
        private struct TileData
        {
            public bool IsMerged;
            public int Value;
        }
        
        private static readonly Dictionary<InputDirection, (int x, int y)> DirVectors = new()
        {
            { InputDirection.UP,    (0, 1) },
            { InputDirection.DOWN,  (0, -1) },
            { InputDirection.LEFT,  (-1, 0) },
            { InputDirection.RIGHT, (1, 0) },
        };
        
        private readonly Dictionary<Vector2Int, TileData> _board = new();

        private IPuzzleRule _rule;
        private int         _boardSize;
        
        public void Initialize(int size, IPuzzleRule rule)
        {
            _rule = rule;
            _boardSize = size;
            _board.Clear();

            for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
            {
                var pos = new Vector2Int(x, y);
                _board[pos] = new TileData();
            }
        }
        
        public List<Vector2Int> GetEmptyTiles()
        {
            return _board
                .Where(pair => pair.Value.Value == 0)
                .Select(pair => pair.Key)
                .ToList();
        }
        
        public bool IsStuck()
        {
            foreach (var pair in _board)
            {
                var pos = pair.Key;
                var val = pair.Value.Value;

                if (val == 0)
                    return false;

                foreach (var dir in DirVectors.Values)
                {
                    var neighbor = new Vector2Int(pos.x + dir.x, pos.y + dir.y);
                    if (_board.TryGetValue(neighbor, out var neighborData) == false)
                        continue;
                    if (neighborData.Value == val)
                        return false;
                }
            }

            return true;
        }
        
        public void AddNewTile(Vector2Int position, int value)
        {
            if (!_board.ContainsKey(position) || _board[position].Value != 0)
                throw new InvalidOperationException("[PuzzleBoard.AddNewTile] Invalid Position");

            var tile = _board[position];
            tile.Value = value;
            _board[position] = tile;
        }
        
        public Dictionary<Vector2Int, int> GetBoardSnapshot()
        {
            return _board
                .Where(pair => pair.Value.Value != 0)
                .ToDictionary(pair => pair.Key, pair => pair.Value.Value);
        }
        
        public List<MergeResult> Merge(InputDirection dir)
        {
            foreach (var key in _board.Keys.ToList())
            {
                var tile = _board[key];
                tile.IsMerged = false;
                _board[key] = tile;
            }

            var lines = SliceLines(dir);

            List<MergeResult> results = new();

            foreach (var line in lines)
            {
                for (int i = 0; i < line.Count - 1; i++)
                {
                    var current = line[i];
                    var next = line[i + 1];

                    if (_board[current].Value == _board[next].Value &&
                        !_board[current].IsMerged &&
                        !_board[next].IsMerged)
                    {
                        var result = new MergeResult { To = current, From = next };
                        
                        var value = _board[current].Value * 2;

                        result.MergedValue = value;

                        var currentTile = _board[current];
                        currentTile.IsMerged = true;

                        if (_rule != null && _rule.ShouldRemoveTile(current, value))
                        {
                            value = 0;
                            result.IsRemoved = true;
                        }
                            
                        currentTile.Value = value;
                        
                        _board[current] = currentTile;

                        var nextTile = _board[next];
                        nextTile.Value = 0;
                        _board[next] = nextTile;
                        
                        results.Add(result);

                        i++;
                    }
                }
            }

            return results;
        }
        
        public List<MoveResult> Move(InputDirection dir)
        {
            List<MoveResult> results = new();
            
            var lines = SliceLines(dir);

            foreach (var line in lines)
            {
                foreach (var pos in line)
                {
                    var farthest = GetFarthestPosition(pos, dir);
                    if (pos.Equals(farthest))
                        continue;

                    var value = _board[pos].Value;
                    var emptyTile = _board[farthest];
                    emptyTile.Value = value;
                    _board[farthest] = emptyTile;

                    var originalTile = _board[pos];
                    originalTile.Value = 0;
                    _board[pos] = originalTile;

                    results.Add(new MoveResult{From = pos, To = farthest});
                }
            }

            return results;
        }
        
        private List<List<Vector2Int>> SliceLines(InputDirection dir)
        {
            List<List<Vector2Int>> lines = new List<List<Vector2Int>>();

            bool horizontal = dir == InputDirection.LEFT || dir == InputDirection.RIGHT;
            bool reverse = dir == InputDirection.UP || dir == InputDirection.RIGHT;

            for (int i = 0; i < _boardSize; i++)
            {
                List<Vector2Int> line = new List<Vector2Int>();

                for (int j = 0; j < _boardSize; j++)
                {
                    int x = horizontal ? (reverse ? _boardSize - 1 - j : j) : i;
                    int y = horizontal ? i : (reverse ? _boardSize - 1 - j : j);
                    var pos = new Vector2Int(x, y);

                    if (_board[pos].Value != 0)
                        line.Add(pos);
                }

                if (line.Count > 0)
                    lines.Add(line);
            }

            return lines;
        }
        
        private Vector2Int GetFarthestPosition(Vector2Int origin, InputDirection dir)
        {
            var direction = DirVectors[dir];
            var current = new Vector2Int(origin.x + direction.x, origin.y + direction.y);
            var lastEmpty = origin;

            while (IsInBounds(current) && _board[current].Value == 0)
            {
                lastEmpty = current;
                current = new Vector2Int(current.x + direction.x, current.y + direction.y);
            }

            return lastEmpty;
        }
        
        private bool IsInBounds(Vector2Int pos) =>
            pos.x >= 0 && pos.x < _boardSize && pos.y >= 0 && pos.y < _boardSize;
    }
}