using System;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle2048
{
    public class Puzzle : MonoBehaviour
    {
        public static Puzzle CreateGame(IPuzzleRule rule)
        {
            var instance = new GameObject() { name = "Puzzle2048" }.AddComponent<Puzzle>();
            instance.Initialize(rule);
            return instance;
        }
    
        /******************************************************************************************************************/
        /******************************************************************************************************************/
    
        public event Action<int>                         MergeEvent;
        public event Action                              TileRemovedEvent;
        public event Action<int>                         TotalMoveChangedEvent;
        public event Action<int>                         TotalUndoChangedEvent;
        public event Action<bool>                        UndoAbleChangeEvent;
        public event Action<Dictionary<Vector2Int, int>> GameWinEvent;
        public event Action<Dictionary<Vector2Int, int>> GameOverEvent;

        private IPuzzleRule   _rule;
        private PuzzleView    _view;
        private PuzzleGameApp _app;

        private void Initialize(IPuzzleRule rule)
        {
            _rule = rule;
            _view = Instantiate(PuzzleSettings.GetViewPrefab());
            _app = gameObject.AddComponent<PuzzleGameApp>();
            _app.Initialize(_rule, _view, rule.GetBoardSize());
        }

        public void StartGame()
        {
            _app.MergeEvent            += MergeEvent;
            _app.TileRemovedEvent      += TileRemovedEvent;
            _app.TotalMoveChangedEvent += TotalMoveChangedEvent;
            _app.TotalUndoChangedEvent += TotalUndoChangedEvent;
            _app.UndoAbleChangeEvent   += UndoAbleChangeEvent;
            _app.GameWinEvent          += GameWinEvent;
            _app.GameOverEvent         += GameOverEvent;
        
            _app.StartGame();
        }
    
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                _app.Move(InputDirection.UP);
        
            if (Input.GetKeyDown(KeyCode.DownArrow))
                _app.Move(InputDirection.DOWN);
        
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                _app.Move(InputDirection.LEFT);
        
            if (Input.GetKeyDown(KeyCode.RightArrow))
                _app.Move(InputDirection.RIGHT);
        }

        private void OnDestroy()
        {
            if (_view != null)
                Destroy(_view.gameObject);

            if (_app != null)
                Destroy(_app.gameObject);
        }
    }
}
