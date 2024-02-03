using SLib.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// auth suganuma 
public class OnMovieEnd : MonoBehaviour // ラストシーンのムービーが終わったときの処理ここ
{
    public void SetupForStartLEvent()
    {
        var sceneL = GameObject.FindObjectOfType<SceneLoader>();
        var hud = GameObject.FindObjectOfType<HUDManager>();

        sceneL.LoadSceneByName("LastScene");
        hud.ToFront(2);
    }
}
