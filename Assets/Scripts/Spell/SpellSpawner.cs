using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class SpellSpawner : MonoBehaviour
{
    private SpellDatabase _spellDatabase;
    private SpellCaster _playerSpellCaster;
    public List<SpellPrefabMapping> _spellPrefabMappings = new List<SpellPrefabMapping>();

    private Dictionary<string, GameObject> _spellPrefabs = new Dictionary<string, GameObject>();

    [Inject]
    public void Inject(SpellDatabase spellDatabase)
    {
        _spellDatabase = spellDatabase;

        foreach(var mapping in _spellPrefabMappings)
        {
            _spellPrefabs[mapping.spellName] = mapping.prefab;
        }
    }

    void Awake()
    {
        if(_spellDatabase != null)
        {
            _spellDatabase.Load();
        }
    }

    public void RegisterSpellCaster(SpellCaster SpellCaster)
    {
        _playerSpellCaster = SpellCaster;
    }


    public void SpawnSpell()
    {
        var spellStat = _spellDatabase.GetRandomSpell();
        Debug.Log($"{spellStat.name} is spawned");

        if(!_spellPrefabs.TryGetValue(spellStat.name, out GameObject prefab))
        {
            return;
        }

        Spell newSpell = SpellFactory.CreateSpell(spellStat.name, prefab, _playerSpellCaster.transform);
        newSpell.SetStat(spellStat);
        _playerSpellCaster.AddSpell(newSpell);
    }

    public void CombineSpell(Spell spell, int spellCount)
    {
        if(spellCount >= 3)
        {

        }
    }
}
