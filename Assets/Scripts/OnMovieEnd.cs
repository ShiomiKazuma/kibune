using SLib.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// auth suganuma 
public class OnMovieEnd : MonoBehaviour // ���X�g�V�[���̃��[�r�[���I������Ƃ��̏�������
{
    public void SetupForStartLEvent()
    {
        var sceneL = GameObject.FindObjectOfType<SceneLoader>();
        var hud = GameObject.FindObjectOfType<HUDManager>();

        sceneL.LoadSceneByName("LastScene");
        hud.ToFront(2);
    }
}
