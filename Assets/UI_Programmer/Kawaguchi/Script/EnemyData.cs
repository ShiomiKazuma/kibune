using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CsvInporter/Create EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHp;
    public int atk;
    public int def;
    public int exp;
    public int gold;
}
