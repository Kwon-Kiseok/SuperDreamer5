using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class EnemyDatabase
{
    private const string ENEMY_DATA_DB = "EnemyDataList_Table";

    private Dictionary<int, Units.UnitStatData> _enemyStatDataDictionary = new Dictionary<int, Units.UnitStatData>();
    private List<int> _enemyKeyList = new List<int>();

    public void Load()
    {
        if (_enemyStatDataDictionary.Count != 0) return;

        List<Dictionary<string, object>> dataList = CSVReader.Read($"Table/{ENEMY_DATA_DB}");

        for (int i = 0; i < dataList.Count; i++)
        {
            int dataID = (int)dataList[i]["ID"];
            string dName = dataList[i]["Name"].ToString();
            int dMaxHp = (int)dataList[i]["MAXHP"];
            int dAtk = (int)dataList[i]["ATK"];
            int dDef = (int)dataList[i]["DEF"];
            int dSpeed = (int)dataList[i]["SPEED"];

            Units.UnitStatData enemyData = new Units.UnitStatData(dName, dMaxHp, dAtk, dDef, dSpeed);
            _enemyStatDataDictionary.Add(dataID, enemyData);
            _enemyKeyList.Add(dataID);
        }
    }

    public Units.UnitStatData GetStatData(int dataID)
    {
        return _enemyStatDataDictionary[dataID];
    }
}
