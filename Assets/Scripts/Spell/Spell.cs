using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public enum SpellGrade
    {
        Normal,
        Rare
    }

    public enum SpellType
    {
        Melee,
        Ranged
    }

    public enum SpellElement
    {
        Stone,
        Fire,
        Water
    }

    public struct SpellStatData
    {
        public string name;
        public SpellGrade grade;
        public SpellType type;
        public SpellElement element;
        public float cooldown;
        public int damage;

        public SpellStatData(string name, SpellGrade grade, SpellType type, SpellElement element, float cooldown, int damage)
        {
            this.name = name;
            this.grade = grade;
            this.type = type;
            this.element = element;
            this.cooldown = cooldown;
            this.damage = damage;
        }  
    }

    private SpellStatData _StatData;
    public SpellStatData StatData => _StatData;

    public void SetStat(SpellStatData statData)
    {
        _StatData = statData;
    }

    public abstract void CastSpell(Vector3 position, Transform target, int damage);
}
