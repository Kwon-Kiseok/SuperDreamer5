using UnityEngine;

public class FireballSpell : Spell
{
    private GameObject _fireballPrefab;

    public FireballSpell(GameObject fireballPrefab)
    {
        _fireballPrefab = fireballPrefab;
    }

    public override void CastSpell(Vector3 position, Transform target, int damage)
    {
        if(_fireballPrefab != null)
        {
            GameObject fireball = GameObject.Instantiate(_fireballPrefab, position, Quaternion.identity);
            fireball.GetComponent<Fireball>().Initialize(Vector3.down, damage);
        }
    }
}
