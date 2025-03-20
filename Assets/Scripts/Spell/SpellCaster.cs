using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    private List<Spell> _ownedSpells = new List<Spell>();
    private Dictionary<Spell, float> _spellCooldowns = new Dictionary<Spell, float>();
    private CancellationTokenSource _cts;
    private readonly object _lockObject = new object();

    void Start()
    {
        _cts = new CancellationTokenSource();
        StartSpellLoop(_cts.Token).Forget();
    }

    public void SetCharacterTransform(Transform characterTransform)
    {
        _playerTransform = characterTransform;
    }

    private async UniTaskVoid StartSpellLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            List<Spell> spellSnapshot;
            lock (_lockObject)
            {
                spellSnapshot = new List<Spell>(_ownedSpells); // 리스트 복사
            }

            foreach (var spell in spellSnapshot)
            {
                if (_spellCooldowns.TryGetValue(spell, out float remainingTime) && remainingTime > 0)
                {
                    continue; // 쿨타임이 남아있으면 스킵
                }

                spell.CastSpell(_playerTransform.position + Vector3.down, null, spell.StatData.damage);
                lock (_lockObject)
                {
                    _spellCooldowns[spell] = spell.StatData.cooldown; // 쿨타임 설정
                }
            }

            await UniTask.Delay(100); // 0.1초마다 체크

            // 쿨타임 감소
            lock (_lockObject)
            {
                List<Spell> keys = new List<Spell>(_spellCooldowns.Keys);
                foreach (var spell in keys)
                {
                    _spellCooldowns[spell] = Mathf.Max(0, _spellCooldowns[spell] - 0.1f);
                }
            }
        }
    }

    public void AddSpell(Spell newSpell)
    {
        lock (_lockObject)
        {
            _ownedSpells.Add(newSpell);
            _spellCooldowns[newSpell] = 0f;
        }
    }

    public void RemoveSpell(Spell spell)
    {
        lock (_lockObject)
        {
            _ownedSpells.Remove(spell);
            _spellCooldowns.Remove(spell);
        }
    }

    void OnDestroy()
    {
        _cts?.Cancel();
    }
}
