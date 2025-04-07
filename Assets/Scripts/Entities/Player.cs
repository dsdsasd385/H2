using System;
using UnityEngine;

namespace Entities
{
    public class Player : Entity
    {
        public static Player Self { get; private set; }
        
        public static Player CreateNew(int hp, int attackPoint, int defence, int criticalPercentage, int speed,
            int gold)
        {
            var player = new Player(hp, attackPoint, defence, criticalPercentage, speed)
            {
                _gold = gold
            };

            Self = player;

            return player;
        }
        
        /**************************************************************************************************************/
        /**************************************************************************************************************/
        
        
        public event Action<int, int> GoldChanged; 
        
        public Player(int hp, int attackPoint, int defence, int criticalPercentage, int speed) : base(hp, attackPoint, defence, criticalPercentage, speed)
        {
            
        }

        private int _gold;

        public int Gold
        {
            get => _gold;
            set
            {
                int before = _gold;
                _gold = Mathf.Max(0, value);
                GoldChanged?.Invoke(before, _gold);
            }
        }
        
        public void SetGoldByPercent(int percent)=> Gold +=(Gold * percent / 100);
        public void AddGold(int value) => Gold += value;

        public void OnRoulette(RouletteResult result)
        {
            switch (result.Type)
            {
                case StageRouletteType.EXERCISE:
                    SetHpByPercent(result.ChangeValue);
                    break;
                case StageRouletteType.RESHARPENING_WEAPON:
                    SetAttackPointByPercent(result.ChangeValue);
                    break;
                case StageRouletteType.CLEANING_ARMOR:
                    SetDefenceByPercent(result.ChangeValue);
                    break;
                case StageRouletteType.PICK_COIN:
                    AddGold(result.ChangeValue);
                    break;
                case StageRouletteType.BUG_BITE:
                    SetHpByPercent(result.ChangeValue);
                    break;
                case StageRouletteType.BROKEN_WEAPON:
                    SetAttackPointByPercent(result.ChangeValue);
                    break;
                case StageRouletteType.LOOSEN_ARMOR:
                    SetDefenceByPercent(result.ChangeValue);
                    break;
                case StageRouletteType.LOST_COIN:
                    AddGold(result.ChangeValue);
                    break;
            }
        }
    }
}