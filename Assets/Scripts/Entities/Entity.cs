using System;
using UnityEngine;

namespace Entities
{
    public abstract class Entity
    {
        public event Action<int, int> HpChanged;
        public event Action<int, int> AttackPointChanged;
        public event Action<int, int> DefenceChanged;
        public event Action<int, int> CriticalChanged;
        public event Action<int, int> SpeedChanged;
        
        public int Hp
        {
            get => _status.Hp;
            set
            {
                int before = _status.Hp;
                _status.Hp = Mathf.Max(0, value);
                HpChanged?.Invoke(before, _status.Hp);
            }
        }

        public int AttackPoint
        {
            get => _status.AttackPoint;
            set
            {
                int before = _status.AttackPoint;
                _status.AttackPoint = Mathf.Max(0, value);
                AttackPointChanged?.Invoke(before, _status.AttackPoint);
            }
        }
        
        public int Defence
        {
            get => _status.Defence;
            set
            {
                int before = _status.Defence;
                _status.Defence = Mathf.Max(0, value);
                DefenceChanged?.Invoke(before, _status.Defence);
            }
        }
        
        public int Critical
        {
            get => _status.CriticalPercentage;
            set
            {
                int before = _status.CriticalPercentage;
                _status.CriticalPercentage = Mathf.Max(0, value);
                CriticalChanged?.Invoke(before, _status.CriticalPercentage);
            }
        }
        
        public int Speed
        {
            get => _status.Speed;
            set
            {
                int before = _status.Speed;
                _status.Speed = Mathf.Max(0, value);
                SpeedChanged?.Invoke(before, _status.Speed);
            }
        }
        
        private EntityStatus _status;

        public void SetHpByPercent(int percent)          => Hp          +=(Hp * percent / 100);
        public void SetAttackPointByPercent(int percent) => AttackPoint +=(AttackPoint * percent / 100);
        public void SetDefenceByPercent(int percent)     => Defence     +=(Defence * percent / 100);
        public void SetCriticalByPercent(int percent)    => Critical    +=(Critical * percent / 100);
        public void SetSpeedByPercent(int percent)       => Speed       +=(Speed * percent / 100);

        public void AddHp(int value)          => Hp          += value;
        public void AddAttackPoint(int value) => AttackPoint += value;
        public void AddDefence(int value)     => Defence     += value;
        public void AddCritical(int value)    => Critical    += value;
        public void AddSpeed(int value)       => Speed       += value;
        
        
        protected Entity(int hp, int attackPoint, int defence, int criticalPercentage, int speed)
        {
            _status = new EntityStatus(hp, attackPoint, defence, criticalPercentage, speed);
        }
    }
}