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
    
        public event Action                              TileRemovedEvent;
        public event Action<Dictionary<Vector2Int, int>> GameOverEvent;

        private IPuzzleRule   _rule;
        private PuzzleView    _view;
        private PuzzleGameApp _app;
        
        private int           _remainMove;

        private void Initialize(IPuzzleRule rule)
        {
            _rule = rule;
            _view = Instantiate(PuzzleSettings.GetViewPrefab());
            _app = gameObject.AddComponent<PuzzleGameApp>();
            _app.Initialize(_rule, _view, rule.GetBoardSize());
        }

        public void StartGame()
        {
            _app.TileRemovedEvent      += TileRemovedEvent;
            _app.GameOverEvent         += GameOverEvent;
            _app.TotalMoveChangedEvent += move =>
            {
                _remainMove = _rule.GetTotalMoveCount() - move;
                _view.OnRemainMoveChanged(_remainMove);

                if (_remainMove == 0)
                    _app.FinishGame();
            };
        
            _app.StartGame();
        }
    
        private void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            HandleKeyboardInput();
#elif UNITY_IOS || UNITY_ANDROID
            HandleTouchInput();
#endif
        }
        
        private void HandleKeyboardInput()
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
        
        private Vector2 _touchStart;

        private void HandleTouchInput()
        {
            if (Input.touchCount == 0) return;

            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _touchStart = touch.position;
                    break;

                case TouchPhase.Ended:
                    Vector2 delta = touch.position - _touchStart;

                    if (delta.magnitude < 50f) return;

                    if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                        _app.Move(delta.x > 0 ? InputDirection.RIGHT : InputDirection.LEFT);
                    else
                        _app.Move(delta.y > 0 ? InputDirection.UP : InputDirection.DOWN);
                    break;
            }
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
