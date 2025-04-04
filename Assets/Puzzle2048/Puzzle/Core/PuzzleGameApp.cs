using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Puzzle2048
{
    public class PuzzleGameApp : MonoBehaviour
    {
        public int  BoardSize { get; private set; }
        public bool CanUndo   => _stack.CanPop();

        public event Action<int>                         MergeEvent;
        public event Action                              TileRemovedEvent;
        public event Action<int>                         TotalMoveChangedEvent;
        public event Action<int>                         TotalUndoChangedEvent;
        public event Action<bool>                        UndoAbleChangeEvent;
        public event Action<Dictionary<Vector2Int, int>> GameOverEvent;

        private IPuzzleRule    _rule;
        private Board          _board;
        private IPuzzleView    _view;
        private IEnumerator    _gameCoroutine;
        private InputDirection _input;
        private int            _maxSave;
        private SnapShotStack  _stack;
        
        private int _totalMove;
        private int _totalUndo;
        
        public void Initialize(IPuzzleRule rule, IPuzzleView view, int boardSize, int maxSave = 5)
        {
            _rule     = rule;
            _view     = view;
            BoardSize = boardSize;
            _maxSave  = maxSave;
        }

        public void StartGame()
        {
            SetState(GameState.INITIALIZE);
        }

        public void FinishGame()
        {
            SetState(GameState.GAME_OVER);
        }

        public void Move(InputDirection dir)
        {
            _input = dir;
        }

        public void Undo()
        {
            SetState(GameState.HANDLE_UNDO);
        }

        private PuzzleSnapshot GetSnapshot()
        {
            return new PuzzleSnapshot
            {
                BoardSize = BoardSize,
                Tiles = _board.GetBoardSnapshot(),
            };
        }

        private void SetState(GameState state)
        {
            if (_gameCoroutine != null)
                StopCoroutine(_gameCoroutine);
            
            switch (state)
            {
                case GameState.INITIALIZE:
                    _gameCoroutine = InitCoroutine();
                    break;
                case GameState.SPAWN_TILE:
                    _gameCoroutine = SpawnTileCoroutine();
                    break;
                case GameState.WAIT_FOR_INPUT:
                    _gameCoroutine = WaitForInputCoroutine();
                    break;
                case GameState.HANDLE_MOVE:
                    _gameCoroutine = HandleMoveCoroutine();
                    break;
                case GameState.HANDLE_UNDO:
                    _gameCoroutine = UndoCoroutine();
                    break;
                case GameState.GAME_OVER:
                    _gameCoroutine = GameOverCoroutine();
                    break;
            }

            StartCoroutine(_gameCoroutine);
        }

        private IEnumerator InitCoroutine()
        {
            _stack = new(_maxSave);
            _stack.OnSnapshotChanged += () => UndoAbleChangeEvent?.Invoke(CanUndo);
            
            var rect = _view.GetBoardRect();
            var padding = _view.GetTilePadding();
            var tileSize = PuzzleUtil.GetCellSize(rect, BoardSize, padding);
            var positions = PuzzleUtil.GetAllCellPositions(rect, BoardSize, padding);
            
            yield return _view.Initialize(BoardSize, tileSize, positions);
            
            _board = new();
            _board.Initialize(BoardSize, _rule);
            
            SetState(GameState.SPAWN_TILE);
        }

        private IEnumerator SpawnTileCoroutine()
        {
            var emptyTiles = _board.GetEmptyTiles();

            if (emptyTiles.Count == 0)
            {
                SetState(GameState.GAME_OVER);
                yield break;
            }

            var pos = emptyTiles[Random.Range(0, emptyTiles.Count)];
            var val = Random.value > 0.9f ? 4 : 2;
            _board.AddNewTile(pos, val);
            yield return _view.SpawnTile(pos, val);

            if (_board.IsStuck())
            {
                SetState(GameState.GAME_OVER);
                yield break;
            }
            
            SetState(GameState.WAIT_FOR_INPUT);
        }

        private IEnumerator WaitForInputCoroutine()
        {
            _input = InputDirection.NONE;

            yield return new WaitUntil(() => _input != InputDirection.NONE);
            
            SetState(GameState.HANDLE_MOVE);
        }

        private IEnumerator HandleMoveCoroutine()
        {
            _stack.Push(GetSnapshot());

            var mergeResult = _board.Merge(_input);
            var moveResult   = _board.Move(_input);

            if (mergeResult.Count == 0 && moveResult.Count == 0)
            {
                SetState(GameState.WAIT_FOR_INPUT);
                yield break;
            }
            
            foreach (var merge in mergeResult)
            {
                MergeEvent?.Invoke(merge.MergedValue);

                if (merge.IsRemoved)
                    TileRemovedEvent?.Invoke();
            }

            _totalMove++;
            TotalMoveChangedEvent?.Invoke(_totalMove);
            
            yield return _view.MoveTile(mergeResult, moveResult);
            
            SetState(GameState.SPAWN_TILE);
        }

        private IEnumerator UndoCoroutine()
        {
            if (_stack.TryPop(out var snapshot) == false)
            {
                SetState(GameState.WAIT_FOR_INPUT);
                yield break;
            }

            var current = _board.GetBoardSnapshot();
            var restore  = snapshot.Tiles;

            _totalUndo++;
            TotalUndoChangedEvent?.Invoke(_totalUndo);

            yield return _view.Restore(current, restore);
            
            SetState(GameState.WAIT_FOR_INPUT);
        }

        private IEnumerator GameOverCoroutine()
        {
            GameOverEvent?.Invoke(_board.GetBoardSnapshot());
            
            yield break;
        }
    }
}