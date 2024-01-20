using SLib.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour   // 院ゲームのみのインスタンスシングルトンである必要なし
{
    /// <summary> ポーズ画面に入ったときに呼ばれるメソッド </summary>
    public static Action BeginPause;
    /// <summary> ポーズ画面が終わった時に呼ばれるメソッド </summary>
    public static Action EndPause;
    /// <summary> ポーズ状態であるかのフラグ </summary>
    bool _isPaused;
    public bool IsPaused => _isPaused;

    [SerializeField] GameObject _inventoryUI;
    HUDManager _hudMan;

    private void Awake() => _hudMan = GameObject.FindObjectOfType<HUDManager>();

    // Update is called once per frame
    void Update()
    {
        //ポーズボタンを押したら、処理が走る
        if (Input.GetButtonDown("Inventory"))
        {
            _isPaused = !_isPaused;

            if (IsPaused)
            {
                EndPause();
                _inventoryUI.SetActive(!_isPaused);
                _inventoryUI.transform.SetAsFirstSibling();
            }
            else
            {
                BeginPause();
                _inventoryUI.SetActive(_isPaused);
                _inventoryUI.transform.SetAsLastSibling();
            }
        }

        // （菅沼） 設定画面用の入力が入った時の処理をこれ以降にはさむ予定
        if (Input.GetKeyDown("Settings"))
        {
            _isPaused = !_isPaused;

            if (IsPaused)
            {
                EndPause();
                _hudMan.ToFront(2);
            }
            else
            {
                BeginPause();
                _hudMan.ToFront(1);
            }
        }
    }
}
