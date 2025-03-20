using UnityEngine;

public class SandPistolSpell : Spell
{
    private GameObject _sandPistolPrefab;

    public SandPistolSpell(GameObject sandPistolPrefab)
    {
        _sandPistolPrefab = sandPistolPrefab;
    }

    public override void CastSpell(Vector3 position, Transform target, int damage)
    {
        if (_sandPistolPrefab != null)
        {
            GameObject sandPistol = GameObject.Instantiate(_sandPistolPrefab, position, Quaternion.identity);
            sandPistol.GetComponent<SandPistol>().Initialize(Vector3.down, damage);
        }
    }
}
