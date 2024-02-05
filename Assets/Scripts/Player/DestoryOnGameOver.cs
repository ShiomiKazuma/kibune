using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOnGameOver : MonoBehaviour
{
    GameManager gm;

    void OnGameOver()
    {
        GameObject.Destroy(transform.parent.gameObject);
    }

    private void Awake()
    {
        gm = GameObject.FindFirstObjectByType<GameManager>();
    }

    private void OnEnable()
    {
        gm.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        gm.OnGameOver -= OnGameOver;
    }
}
