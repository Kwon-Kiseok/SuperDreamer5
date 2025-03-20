using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellFactory
{
    private static Dictionary<string, Func<GameObject, Transform, Spell>> _spellConstructors =
        new Dictionary<string, Func<GameObject, Transform, Spell>>();

    public static void RegisterSpell(string spellName, Func<GameObject, Transform, Spell> constructor)
    {
        if (!_spellConstructors.ContainsKey(spellName))
        {
            _spellConstructors[spellName] = constructor;
        }
    }

    public static Spell CreateSpell(string spellName, GameObject prefab, Transform playerTransform)
    {
        if (_spellConstructors.TryGetValue(spellName, out var constructor))
        {
            return constructor(prefab, playerTransform);
        }
        else
        {
            Debug.LogError($"{spellName} 주문이 존재하지 않습니다!");
            return null;
        }
    }
}