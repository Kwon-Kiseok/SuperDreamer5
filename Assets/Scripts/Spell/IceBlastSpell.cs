using UnityEngine;

public class IceBlastSpell : Spell
{
    private GameObject _iceBlastPrefab;

    public IceBlastSpell(GameObject iceBlastPrefab)
    {
        _iceBlastPrefab = iceBlastPrefab;
    }

    public override void CastSpell(Vector3 position, Transform target, int damage)
    {
        if (_iceBlastPrefab != null)
        {
            GameObject iceBlast = GameObject.Instantiate(_iceBlastPrefab, position, Quaternion.identity);
            iceBlast.GetComponent<IceBlast>().Initialize(Vector3.down, damage);
        }
    }
}
