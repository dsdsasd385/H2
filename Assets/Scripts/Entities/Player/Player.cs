using System;
using System.Collections;
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
        public event Action<int, int> ExpChanged;
        public event Action<int>      LevelChanged; 
        
        public Player(int hp, int attackPoint, int defence, int criticalPercentage, int speed) : base(hp, attackPoint, defence, criticalPercentage, speed)
        {
            
        }

        private int _gold;
        private int _exp;
        private int _needExp = 10;
        private int _level = 1;
        private int _skillPoint;

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

        public int Exp
        {
            get => _exp;
            set
            {
                int before = _exp;
                _exp = Mathf.Max(0, value);
                while (_needExp <= _exp)
                {
                    var remain = _exp - _needExp;
                    _exp = remain;
                    Level++;
                    _needExp += 5;
                }
                ExpChanged?.Invoke(before, _exp);
            }
        }

        public int Level
        {
            get => _level;
            private set
            {
                if (_level >= value) return;

                _level = value;
                _skillPoint++;
                LevelChanged?.Invoke(_level);
            }
        }
        
        public void SetGoldByPercent(int percent)=> Gold +=(Gold * percent / 100);
        
        public void AddGold(int value) => Gold += value;

        public void OnRoulette(RouletteResult result)
        {
            switch (result.Type)
            {
                case StageRouletteType.EXERCISE:
                    SetMaxHpByPercent(result.ChangeValue);
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
                case StageRouletteType.READ_BOOK:
                    Exp += result.ChangeValue;
                    break;
                case StageRouletteType.BUG_BITE:
                    SetMaxHpByPercent(result.ChangeValue);
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

        public IEnumerator AddSkill()
        {
            if (_skillPoint == 0)
                yield break;
            
            // todo Load Skill Add UI
            
            for (int i = 0; i < _skillPoint; i++)
            {
                Debug.Log("스킬 선택!");
            }

            _skillPoint = 0;
        }
    }
}