using UnityEngine;

namespace Puzzle2048
{
    public enum GameState
    {
        INITIALIZE,
        SPAWN_TILE,
        WAIT_FOR_INPUT,
        HANDLE_MOVE,
        HANDLE_UNDO,
        GAME_OVER,
    }
    
    public enum InputDirection
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT,
    }
    
    public struct MergeResult
    {
        public Vector2Int To;
        public Vector2Int From;
        public int        MergedValue;
        public bool       IsRemoved;
    }

    public struct MoveResult
    {
        public Vector2Int From;
        public Vector2Int To;
    }
}