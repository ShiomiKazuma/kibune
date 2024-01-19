using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    [SerializeField, Header("アイテムを表示するフラグ")] public bool[] IsItemFlag;
    [SerializeField, Header("アイテムボックスのゲームオブジェクト")] GameObject[] _items;

    private void Start()
    {
        //アイテムフラグの初期化
        for(int i = 0; i < _items.Length; i++)
        {
            _items[i].SetActive(IsItemFlag[i]);
        }
    }
    /// <summary>
    /// アイテムフラグのtrue
    /// </summary>
    /// <param name="Id"> アイテムID </param>
    void AddItem(int Id)
    {
        IsItemFlag[Id] = true;
        _items[Id].SetActive(true);  
    }

    /// <summary>
    /// アイテムのオンオフを更新する
    /// </summary>
    public void UpdateItem()
    {
        //アイテムフラグの更新
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].SetActive(IsItemFlag[i]);
        }
    }
}
