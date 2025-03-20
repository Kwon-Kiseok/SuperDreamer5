using UnityEngine;
using Zenject;

public class UserCharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _userCharacterPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _spawnTransform;

    void Start()
    {
        GameObject userCharacter = Instantiate(_userCharacterPrefab, _spawnPoint.position, Quaternion.identity);
        userCharacter.transform.SetParent(_spawnTransform);
    }
}
