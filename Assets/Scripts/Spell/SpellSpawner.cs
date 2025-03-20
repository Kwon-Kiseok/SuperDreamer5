using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Collections.Generic;

public class SpellSpawner : MonoBehaviour
{
    private SpellDatabase _spellDatabase;
    private SpellCaster _playerSpellCaster;
    public List<SpellPrefabMapping> _spellPrefabMappings = new List<SpellPrefabMapping>();

    private Dictionary<string, GameObject> _spellPrefabs = new Dictionary<string, GameObject>();
    private Dictionary<string, int> _spellCounts = new Dictionary<string, int>();

    public List<SpellButton> _spellButtons = new List<SpellButton>();

    private int _spellSpawnCostGold = 20;
    public int SpellSpawnCostGold => _spellSpawnCostGold;

    private int _goldOffset = 8;

    [Inject]
    public void Inject(SpellDatabase spellDatabase)
    {
        _spellDatabase = spellDatabase;

        foreach(var mapping in _spellPrefabMappings)
        {
            _spellPrefabs[mapping.spellName] = mapping.prefab;
            _spellCounts[mapping.spellName] = 0;
        }
    }

    void Awake()
    {
        if(_spellDatabase != null)
        {
            _spellDatabase.Load();
        }

        foreach(var spellButton in _spellButtons)
        {
            spellButton.Button.onClick.AddListener(() => {
                CombineSpell(spellButton.SpellName);
            });
        }
    }

    public void RegisterSpellCaster(SpellCaster SpellCaster)
    {
        _playerSpellCaster = SpellCaster;
    }


    public void SpawnSpell()
    {
        if(CostCheck() == false) return;

        var spellStat = _spellDatabase.GetRandomSpell();
        Debug.Log($"{spellStat.name} is spawned");

        if(!_spellPrefabs.TryGetValue(spellStat.name, out GameObject prefab))
        {
            return;
        }

        Spell newSpell = SpellFactory.CreateSpell(spellStat.name, prefab, _playerSpellCaster.transform);
        newSpell.SetStat(spellStat);
        _playerSpellCaster.AddSpell(newSpell);

        if(_spellCounts.ContainsKey(spellStat.name))
        {
            _spellCounts[spellStat.name]++;

            if(_spellCounts[spellStat.name] > 0)
            {
                var spellButton = FindSpellButton(spellStat.name);
                spellButton.SetSpellButtonActive(true);
                spellButton.SetSpellCount(_spellCounts[spellStat.name]);
            }
        }

        CurrenyManager.Instance.UseGold(SpellSpawnCostGold);
        _spellSpawnCostGold += _goldOffset;
    }

    private SpellButton FindSpellButton(string name)
    {
        foreach(var button in _spellButtons)
        {
            if(button.SpellName == name)
            {
                return button;
            }
        }
        return null;
    }

    public void CombineSpell(string spellName)
    {
        if(_spellCounts[spellName] >= 3)
        {
            _spellCounts[spellName] -= 3;

            var spellButton = FindSpellButton(spellName);

            if (_spellCounts[spellName] > 0)
            {
                spellButton.SetSpellButtonActive(true);
            }
            else
            {
                spellButton.SetSpellButtonActive(false);
            }
            spellButton.SetSpellCount(_spellCounts[spellName]);

            // 현재는 노말 레어뿐이라 일단 노말만 넣음
            var newSpellStat = _spellDatabase.GetCombineSpell(Spell.SpellGrade.Normal);

            if (!_spellPrefabs.TryGetValue(newSpellStat.name, out GameObject prefab))
            {
                return;
            }

            Spell newSpell = SpellFactory.CreateSpell(newSpellStat.name, prefab, _playerSpellCaster.transform);
            newSpell.SetStat(newSpellStat);
            _playerSpellCaster.AddSpell(newSpell);

            if (_spellCounts.ContainsKey(newSpellStat.name))
            {
                _spellCounts[newSpellStat.name]++;

                if (_spellCounts[newSpellStat.name] > 0)
                {
                    var newSpellButton = FindSpellButton(newSpellStat.name);
                    newSpellButton.SetSpellButtonActive(true);
                    newSpellButton.SetSpellCount(_spellCounts[newSpellStat.name]);
                }
            }
        }
    }

    private bool CostCheck()
    {
        if(CurrenyManager.Instance != null)
        {
            if(CurrenyManager.Instance.CurrenntGold.Value >= _spellSpawnCostGold)
            {
                return true;
            }
        }
        
        return false;
    }

    public int GetSpellCount(string SpellName)
    {
        return _spellCounts.ContainsKey(SpellName) ? _spellCounts[SpellName] : 0;
    }
}
