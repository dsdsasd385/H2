namespace Entities
{
    public struct EntityStatus
    {
        public int MaxHp;
        public int Hp;
        public int AttackPoint;
        public int Defence;
        public int CriticalPercentage;
        public int Speed;
        
        public EntityStatus(int hp, int attackPoint, int defence, int criticalPercentage, int speed)
        {
            MaxHp = Hp = hp;
            AttackPoint = attackPoint;
            Defence = defence;
            CriticalPercentage = criticalPercentage;
            Speed = speed;
        }
    }
}