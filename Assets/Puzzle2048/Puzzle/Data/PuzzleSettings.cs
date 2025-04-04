using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Puzzle2048
{
    [CreateAssetMenu(fileName = "PuzzleSettings", menuName = "Puzzle/Settings", order = 0)]
    public class PuzzleSettings : ScriptableObject
    {
        #region Singleton
        private static PuzzleSettings _instance;
        private static PuzzleSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Resources.Load<PuzzleSettings>("PuzzleSettings");
                return _instance;
            }
        }
        #endregion

        public static float TileFloatAmp      => Instance.tileFloatingAmp;
        public static float TileFloatDuration => Instance.tileFloatingDuration;
        public static float FadeInDuration    => Instance.fadeInDuration;
        public static Ease  FadeInEase        => Instance.fadeInEase;
        public static float MoveDuration      => Instance.moveDuration;
        public static Ease  MoveEase          => Instance.moveEase;
        public static float DelayEachTile     => Instance.delayEachTile;
        public static float RemoveDuration    => Instance.removeTileDuration;
        public static Ease  RemoveEase        => Instance.removeEase;
        public static float JumpOnMerge       => Instance.jumpOnMerge;

        public static PuzzleView     GetViewPrefab()     => Instance.viewPrefab;
        public static PuzzleTile     GetTilePrefab()     => Instance.tilePrefab;
        public static PuzzleParticle GetParticlePrefab() => Instance.particlePrefab;
        public static Sprite         GetParticleSprite(int index) => Instance.particleSpriteList[index];
        
        public static float ParticleMinDuration => Instance.particleMinDuration;
        public static float ParticleMaxDuration => Instance.particleMaxDuration;
        public static float ParticleMinSize     => Instance.particleMinSize;
        public static float ParticleMaxSize     => Instance.particleMaxSize;
        public static float ParticleMinSpeed => Instance.particleMinSpeed;
        public static float ParticleMaxSpeed => Instance.particleMaxSpeed;

        /******************************************************************************************************************/
        /******************************************************************************************************************/

        [Header("PREFABS")]
        [SerializeField] private PuzzleTile     tilePrefab;
        [SerializeField] private PuzzleView     viewPrefab;
        [SerializeField] private PuzzleParticle particlePrefab;

        [Header("ANIMS")] 
        [SerializeField] private float tileFloatingAmp      = 25f;
        [SerializeField] private float tileFloatingDuration = 1f;
        [SerializeField] private float delayEachTile        = 0.08f;
        [SerializeField] private float fadeInDuration       = 0.15f;
        [SerializeField] private Ease  fadeInEase           = Ease.OutFlash;
        [SerializeField] private float moveDuration         = 0.25f;
        [SerializeField] private Ease  moveEase             = Ease.OutFlash;
        [SerializeField] private float removeTileDuration   = 0.3f;
        [SerializeField] private Ease  removeEase           = Ease.OutFlash;
        [SerializeField] private float jumpOnMerge          = 30f;

        [Header("PARTICLE SETTINGS")] 
        [SerializeField] private List<Sprite> particleSpriteList;
        [SerializeField] private float particleMinDuration = 0.3f;
        [SerializeField] private float particleMaxDuration = 0.6f;
        [SerializeField] private float particleMinSize     = 0.5f;
        [SerializeField] private float particleMaxSize     = 1.2f;
        [SerializeField] private float particleMinSpeed    = 60f;
        [SerializeField] private float particleMaxSpeed    = 120f;
    }
}