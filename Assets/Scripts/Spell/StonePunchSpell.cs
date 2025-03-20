using UnityEngine;

public class StonePunchSpell : Spell
{
    private GameObject _stonePunchPrefab;

    public StonePunchSpell(GameObject stonePunchPrefab)
    {
        _stonePunchPrefab = stonePunchPrefab;
    }

    public override void CastSpell(Vector3 position, Transform target, int damage)
    {
        if (_stonePunchPrefab != null)
        {
            GameObject stonePunch = GameObject.Instantiate(_stonePunchPrefab, position, Quaternion.identity);
            stonePunch.GetComponent<StonePunch>().Initialize(Vector3.down, damage);
        }
    }
}
