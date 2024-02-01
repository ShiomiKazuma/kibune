using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOLObject : MonoBehaviour
{
    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
    }
}
