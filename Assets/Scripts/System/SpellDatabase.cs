using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;

public class SpellDatabase
{
    private const string SPELL_DATA_DB = "SpellDataList_Table";

    private Dictionary<int, Spell.SpellStatData> _spellStatDataDictionary = new Dictionary<int, Spell.SpellStatData>();
    private Dictionary<Spell.SpellGrade, List<Spell.SpellStatData>> _spellsByGrade = new Dictionary<Spell.SpellGrade, List<Spell.SpellStatData>>();
    private Dictionary<Spell.SpellGrade, float> _gradeRateDictionary = new Dictionary<Spell.SpellGrade, float>
    {
        { Spell.SpellGrade.Normal, 0.80f },
        { Spell.SpellGrade.Rare, 0.125f },
    };

    private List<int> _spellKeyList = new List<int>();

    public void Load()
    {
        if (_spellStatDataDictionary.Count != 0) return;

        List<Dictionary<string, object>> dataList = CSVReader.Read($"Table/{SPELL_DATA_DB}");

        for (int i = 0; i < dataList.Count; i++)
        {
            int dataID = (int)dataList[i]["ID"];
            string dName = dataList[i]["Name"].ToString();
            Spell.SpellGrade dGrade = (Spell.SpellGrade)Enum.Parse((typeof(Spell.SpellGrade)), dataList[i]["Grade"].ToString());
            Spell.SpellType dType = (Spell.SpellType)Enum.Parse((typeof(Spell.SpellType)), dataList[i]["Type"].ToString());
            Spell.SpellElement dElement = (Spell.SpellElement)Enum.Parse((typeof(Spell.SpellElement)), dataList[i]["Element"].ToString());
            int dDamage = (int)dataList[i]["Damage"];
            float dCooldown = float.Parse(dataList[i]["Cooldown"].ToString(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);

            Spell.SpellStatData spellData = new Spell.SpellStatData(dName, dGrade, dType, dElement, dCooldown, dDamage);
            _spellStatDataDictionary.Add(dataID, spellData);
            _spellKeyList.Add(dataID);

            if (!_spellsByGrade.ContainsKey(spellData.grade))
                _spellsByGrade[spellData.grade] = new List<Spell.SpellStatData>();

            _spellsByGrade[spellData.grade].Add(spellData);
        }
    }

    public Spell.SpellStatData GetStatData(int dataID)
    {
        return _spellStatDataDictionary[dataID];
    }

    public Spell.SpellStatData GetRandomSpell()
    {
        Spell.SpellGrade randomGrade = GetRandomGrade();

        if(_spellsByGrade.TryGetValue(randomGrade, out List<Spell.SpellStatData> spellList) && spellList.Count > 0)
        {
            return spellList[UnityEngine.Random.Range(0, spellList.Count)];
        }

        return spellList[0];
    }

    public Spell.SpellStatData GetCombineSpell(Spell.SpellGrade spellGrade)
    {
        Spell.SpellGrade upgrade = spellGrade + 1;

        if (_spellsByGrade.TryGetValue(upgrade, out List<Spell.SpellStatData> spellList) && spellList.Count > 0)
        {
            return spellList[UnityEngine.Random.Range(0, spellList.Count)];
        }

        return spellList[0];
    }

    private Spell.SpellGrade GetRandomGrade()
    {
        float totalWeight = _gradeRateDictionary.Values.Sum();
        float randomPoint = UnityEngine.Random.Range(0, totalWeight);

        float currentWeight = 0f;
        foreach(var grade in _gradeRateDictionary)
        {
            currentWeight += grade.Value;
            if(randomPoint <= currentWeight)
            {
                return grade.Key;
            }
        }
        return Spell.SpellGrade.Normal;
    }
}
