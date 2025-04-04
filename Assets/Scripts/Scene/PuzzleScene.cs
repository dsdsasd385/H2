using Puzzle2048;
using UnityEngine;

public class PuzzleScene : GameScene, IPuzzleRule
{
    [SerializeField] private int boardSize = 3;
    [SerializeField] private int moveCount = 10;
    
    private int _point;
    
    protected override void OnSceneStarted()
    {
        var puzzle = Puzzle.CreateGame(this);
        
        puzzle.StartGame();
    }

    protected override void OnReleaseScene()
    {
        
    }
    
    public int  GetBoardSize() => boardSize;
    public int GetTotalMoveCount() => moveCount;
    public bool ShouldRemoveTile(Vector2Int position, int value) => value >= 16;
}