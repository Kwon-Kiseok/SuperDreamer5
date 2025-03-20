using UnityEngine;

public class FireStrikeSpell : Spell
{
    private GameObject _fireStrikePrefab;

    public FireStrikeSpell(GameObject fireStrikePrefab)
    {
        _fireStrikePrefab = fireStrikePrefab;
    }

    public override void CastSpell(Vector3 position, Transform target, int damage)
    {
        if (_fireStrikePrefab != null)
        {
            GameObject fireStrike = GameObject.Instantiate(_fireStrikePrefab, position, Quaternion.identity);
            fireStrike.GetComponent<FireStrike>().Initialize(Vector3.down, damage);
        }
    }
}
