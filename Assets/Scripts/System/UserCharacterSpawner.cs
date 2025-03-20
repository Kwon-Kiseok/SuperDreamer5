using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class UserCharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _userCharacterPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _spawnTransform;

    private SpellSpawner _spellSpawner; // SpellSpawner 참조

    private Lane _lane;
    private bool _isPlayerControlled;

    [Inject] private DiContainer _diContainer;

    void Start()
    {
        _spellSpawner = FindObjectOfType<SpellSpawner>(); // SpellSpawner 찾기

        _lane = GetComponentInParent<Lane>();
        if (_lane != null)
        {
            _isPlayerControlled = _lane.IsPlayerControlled;
        }

        // 캐릭터 생성
        GameObject userCharacter = Instantiate(_userCharacterPrefab, _spawnPoint.position, Quaternion.identity);
        userCharacter.transform.SetParent(_spawnTransform);

        // SpellCaster가 없으면 자동 추가
        SpellCaster spellCaster = userCharacter.GetComponent<SpellCaster>() ?? userCharacter.AddComponent<SpellCaster>();

        spellCaster.SetCharacterTransform(userCharacter.transform);

        // SpellSpawner에 등록 (주문을 받을 수 있도록)
        if (_spellSpawner != null && _isPlayerControlled)
        {
            _spellSpawner.RegisterSpellCaster(spellCaster);
        }

        _diContainer.InjectGameObject(userCharacter);
    }
}