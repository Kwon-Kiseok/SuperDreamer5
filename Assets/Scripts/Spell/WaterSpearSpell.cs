using UnityEngine;

public class WaterSpearSpell : Spell
{
    private GameObject _waterSpearPrefab;

    public WaterSpearSpell(GameObject waterSpearPrefab)
    {
        _waterSpearPrefab = waterSpearPrefab;
    }

    public override void CastSpell(Vector3 position, Transform target, int damage)
    {
        if (_waterSpearPrefab != null)
        {
            GameObject waterSpear = GameObject.Instantiate(_waterSpearPrefab, position, Quaternion.identity);
            waterSpear.GetComponent<WaterSpear>().Initialize(Vector3.down, damage);
        }
    }
}
