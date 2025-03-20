using UnityEngine;

public class Units : MonoBehaviour
{
    public struct UnitStatData
    {
        public string name;
        public int maxHp;
        public int atk;
        public int def;
        public int speed;
        public int huntGold;

        public UnitStatData(string name, int maxHp, int atk, int def, int speed, int huntGold)
        {
            this.name = name;
            this.maxHp = maxHp;
            this.atk = atk;
            this.def = def;
            this.speed = speed;
            this.huntGold = huntGold;
        }
    }

    private UnitStatData _unitStatData;
    public UnitStatData StatData => _unitStatData;

    public UnitRenderer unitRenderer;

    private bool _isAlive = true;
    public bool IsAlive => _isAlive;

    public void SetAlive(bool isAlive)
    {
        _isAlive = isAlive;
    }

    public void SetInitData(UnitStatData unitStatData)
    {
        _unitStatData = unitStatData;
    }
}
