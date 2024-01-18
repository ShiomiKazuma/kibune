using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CsvImporter/Create EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int enemyMaxHp;
    public int enemyAtk;
    public int enemyDef;
    public int enemyExp;
    public int enemyGold;
}
