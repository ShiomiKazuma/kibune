using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    [SerializeField, Header("アイテムを表示するフラグ")]
    List<bool> IsItemActive;
    [SerializeField, Header("アイテムボックスのゲームオブジェクト")]
    List<GameObject> _items;

    public List<bool> Progress => IsItemActive;

    /// <summary> 保持されている進捗を上書き </summary>
    /// <param name="progress"></param>
    public void OverwriteProgress(List<bool> progress)
    {
        IsItemActive.Clear();

        foreach (var item in progress)
        {
            IsItemActive.Add(item);
        }

        UpdateActiveItem();
    }

    /// <summary>
    /// インベントリへ有効なアイテムを追加（インデックス指定）
    /// </summary>
    /// <param name="Index"> アイテムID </param>
    public void AddActiveItem(int Index)
    {
        IsItemActive[Index] = true;
        _items[Index].SetActive(true);

        UpdateActiveItem();
    }

    /// <summary>
    /// アイテムのオンオフを更新する
    /// </summary>
    void UpdateActiveItem()
    {
        //アイテムフラグの更新
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].SetActive(IsItemActive[i]);
        }
    }

    private void Start()
    {
        //アイテムフラグの初期化
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].SetActive(IsItemActive[i]);
        }
    }

}
