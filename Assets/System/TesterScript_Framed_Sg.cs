using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLib.Systems;
public class TesterScript_Framed_Sg : MonoBehaviour
{
    FramedInputBuffer _iBuff;

    private void OnEnable()
    {
        PauseManager.BeginPause += () => { Debug.Log("Start Pause"); };
        PauseManager.EndPause += () => { Debug.Log("End Pause"); };
    }

    private void Awake()
    {
        _iBuff = GameObject.FindAnyObjectByType<FramedInputBuffer>();
    }

    private void Update()
    {
        var raw = _iBuff.EnQueueInputContext();
        var outStr = $"Move_{raw.move.ToString()}, Look_{raw.look.ToString()}, Grapple1_{raw.grapple_t1.ToString()}" +
            $", Grapple2_{raw.grapple_t2.ToString()}, Inventry_{raw.inventry.ToString()}, Pause_{raw.pause.ToString()}";
        Debug.Log(outStr);
    }
}
