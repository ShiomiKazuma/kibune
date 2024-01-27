using SLib.Singleton;
using SLib.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : SingletonBaseClass<PauseManager>   // 院ゲームのみのインスタンスシングルトンである必要なし
{
    [SerializeField] GameObject _inventoryUI;
    /// <summary> ポーズ画面に入ったときに呼ばれるメソッド </summary>
    public static Action BeginPause;
    /// <summary> ポーズ画面が終わった時に呼ばれるメソッド </summary>
    public static Action EndPause;

    HUDManager _hudMan;
    GameInfo _gInfo;

    /// <summary> ポーズ状態であるかのフラグ </summary>
    bool _isPaused;
    public bool IsPaused => _isPaused;


    protected override void ToDoAtAwakeSingleton()
    {
        BeginPause += () => { };
        EndPause += () => { };
        _hudMan = GameObject.FindObjectOfType<HUDManager>();
        _gInfo = GameObject.FindObjectOfType<GameInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        // Only InGame Scene This Will Be Processed
        if (_gInfo.SceneStatus == GameInfo.SceneTransitStatus.To_TitleScene
            || _gInfo.SceneStatus == GameInfo.SceneTransitStatus.To_UniqueScene)
            return;

        //ポーズボタンを押したら、処理が走る
        if (Input.GetButtonDown("Inventory"))
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
                _hudMan.ToFront(3);
            }
        }

        // （菅沼） 設定画面用の入力が入った時の処理をこれ以降にはさむ予定
        if (Input.GetButtonDown("Settings"))
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
