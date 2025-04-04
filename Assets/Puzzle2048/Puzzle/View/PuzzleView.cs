using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle2048
{
    public class PuzzleView : MonoBehaviour, IPuzzleView
    {
        [SerializeField] private RectTransform boardRect;
        [SerializeField] private RectTransform particleLayout;
        [SerializeField] private RectTransform flyTarget;

        public Vector2 GetBoardRect()   => boardRect.rect.size;
        public Vector2 GetTilePadding() => Vector2.one * 10f;
    
        private Dictionary<Vector2Int, PuzzleTile> _tiles = new();
        private Dictionary<Vector2Int, Vector2>    _tilePositions;
        private Vector2 _tileSize;

        private Vector2 GetAnchoredPos(Vector2Int tilePos) => _tilePositions[tilePos];

        public IEnumerator Initialize(int boardSize, Vector2 tileSize, Dictionary<Vector2Int, Vector2> tilePositions)
        {
            _tileSize      = tileSize;
            _tilePositions = tilePositions;
            yield break;
        }

        public IEnumerator SpawnTile(Vector2Int pos, int value)
        {
            var tile = PuzzleTile.Get(boardRect.transform, _tileSize, GetAnchoredPos(pos), value);
            _tiles[pos] = tile;
            tile.FadeIn();
            yield break;
        }

        public IEnumerator MoveTile(List<MergeResult> mergeResult, List<MoveResult> moveResult)
        {
            foreach (var merge in mergeResult)
            {
                var tile = _tiles[merge.From];
                var mergedTile = _tiles[merge.To];
            
                _tiles.Remove(merge.From);
                mergedTile.Value = merge.MergedValue;
                tile.ReleaseOnMerge(GetAnchoredPos(merge.To));
                PuzzleParticle.PlayMergeParticle(particleLayout.transform, GetAnchoredPos(merge.To), merge.MergedValue);
                yield return mergedTile.PlayMergeEffect(mergedTile.Value >= 8);

                if (merge.IsRemoved)
                {
                    PuzzleParticle.PlayCoinParticle(particleLayout.transform, GetAnchoredPos(merge.To), flyTarget.anchoredPosition);
                    _tiles.Remove(merge.To);
                    yield return mergedTile.OnRemoved();
                }
            
                yield return new WaitForSeconds(PuzzleSettings.DelayEachTile);
            }

            if (mergeResult.Count > 0)
                yield return new WaitForSeconds(PuzzleSettings.FadeInDuration);
        
            foreach (var move in moveResult)
            {
                var tile = _tiles[move.From];
                _tiles.Remove(move.From);
                _tiles[move.To] = tile;
                tile.Move(GetAnchoredPos(move.To));
                yield return new WaitForSeconds(PuzzleSettings.DelayEachTile);
            }
        
            if (moveResult.Count > 0)
                yield return new WaitForSeconds(PuzzleSettings.MoveDuration);
        }

        public IEnumerator Restore(Dictionary<Vector2Int, int> current, Dictionary<Vector2Int, int> restore)
        {
            throw new System.NotImplementedException();
        }

        public void OnRemainMoveChanged(int moveCount)
        {
            
        }
    }
}