using System.Collections;
using Puzzle2048;
using UnityEngine;

public class PuzzleScene : AdditiveScene, IPuzzleRule
{
    [SerializeField] private int boardSize = 3;
    [SerializeField] private int moveCount = 10;
    
    private bool _gameOver;
    
    protected override IEnumerator OnSceneLoaded()
    {
        _gameOver = false;
        
        var puzzle = Puzzle.CreateGame(this);

        puzzle.GameOverEvent += (_, point) => OnGameOver(point);
        
        puzzle.StartGame();

        yield return new WaitUntil(() => _gameOver);
    }

    private void OnGameOver(int point)
    {
        Player.CurrentPlayer.AddCoin(point);

        StartCoroutine(GameOverCoroutine(point));
    }

    private IEnumerator GameOverCoroutine(int point)
    {
        string msg = point > 0 ? $"<color=#2989FF>SUCCESS!</color>\n{point}골드 획득!" : $"<color=#FF3419>FAILED!</color>";
        
        yield return UI.Open<PuzzleGameOverUI>().SetMessage(msg);
        
        yield return UnloadScene();
    }

    protected override IEnumerator OnUnloadScene()
    {
        _gameOver = true;
        
        yield break;
    }
    
    public int  GetBoardSize()         => boardSize;
    public int GetTotalMoveCount()     => moveCount;
    public int GetPointPerRemoveTile() => 1000;

    public bool ShouldRemoveTile(Vector2Int position, int value) => value >= 16;
}