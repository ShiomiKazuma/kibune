using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLib.Systems;
public class InGameToEpiTest : MonoBehaviour
{
    SceneLoader _sLoader;

    private void Awake()
    {
        _sLoader = GameObject.FindFirstObjectByType<SceneLoader>();

        _sLoader.LoadSceneByName("Epilougue");
    }
}
