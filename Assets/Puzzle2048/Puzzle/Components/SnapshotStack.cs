using System;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle2048
{
    public struct PuzzleSnapshot
    {
        public int BoardSize;
        public Dictionary<Vector2Int, int> Tiles;
    }
    
    public class SnapShotStack
    {
        public event Action OnSnapshotChanged;
        
        public bool CanPop() => _snapshots.Count > 0;
        
        private LinkedList<PuzzleSnapshot> _snapshots;
        private int max_stack;

        public SnapShotStack(int maxStack)
        {
            max_stack = maxStack;
            _snapshots = new LinkedList<PuzzleSnapshot>();
        }

        public void Push(PuzzleSnapshot snapshot)
        {
            if (_snapshots.Count >= max_stack)
                _snapshots.RemoveFirst();

            _snapshots.AddLast(snapshot);
            OnSnapshotChanged?.Invoke();
        }

        public bool TryPop(out PuzzleSnapshot snapshot)
        {
            if (CanPop() == false)
            {
                snapshot = default;
                return false;
            }

            snapshot = _snapshots.Last.Value;
            _snapshots.RemoveLast();
            OnSnapshotChanged?.Invoke();
            return true;
        }
    }
}