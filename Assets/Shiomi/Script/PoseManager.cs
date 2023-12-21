using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    /// <summary> ポーズ画面に入ったときに呼ばれるメソッド </summary>
    public static Action BeginPause;
    /// <summary> ポーズ画面が終わった時に呼ばれるメソッド </summary>
    public static Action EndPause;
    /// <summary> ポーズ状態であるかのフラグ </summary>
    bool IsPause;

    private void OnEnable()
    {
        //デリゲート登録
        BeginPause += StartPause;
        EndPause += PauseEnd;
    }

    private void OnDisable()
    {
        //デリゲート解除
        BeginPause -= StartPause;
        EndPause -= PauseEnd;
    }
    // Start is called before the first frame update
    void Start()
    {
        IsPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズボタンを押したら、処理が走る
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(IsPause)
            {
                EndPause();
            }
            else
            {
                BeginPause();
            }
        }
    }

    void StartPause()
    {

    }

    void PauseEnd()
    {

    }
}
