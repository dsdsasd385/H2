using System.Collections.Generic;
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

        puzzle.MergeEvent            += OnMerged;
        puzzle.TileRemovedEvent      += OnTileRemoved;
        puzzle.GameWinEvent          += OnFinished;
        puzzle.TotalMoveChangedEvent += OnMoved;
        
        puzzle.StartGame();
    }

    protected override void OnReleaseScene()
    {
        
    }

    private void OnMerged(int value)
    {
        print($"병합 포인트 : {value}");
    }

    private void OnTileRemoved()
    {
        _point += 500;
        
        print($"총 포인트 : {_point}");
    }

    private void OnMoved(int totalMoved)
    {
        print($"남은 이동 : {moveCount - totalMoved}");
    }

    private void OnFinished(Dictionary<Vector2Int, int> _)
    {
        print("게임 종료!");
    }
    
    public int  GetBoardSize() => boardSize;
    public bool IsGameWin(int totalMovedCount) => totalMovedCount >= moveCount;
    public bool ShouldRemoveTile(Vector2Int position, int value) => value >= 16;
    public int GetScoreForMerge(int mergedValue) => mergedValue == 0 ? 1 : 0;
}