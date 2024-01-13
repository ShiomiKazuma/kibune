using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epilogue : MonoBehaviour
{
    [SerializeField] string _nextScene;
    private void OnEnable()
    {
        SceneChanger.Instance.LoadScene(_nextScene);
    }
}
