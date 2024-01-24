using SLib.Inputs;
using SLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 菅沼 が つくった
#region FramedIputContext
public struct FramedDeviceInputContext
{
    // ゲームに応じてここのフィールドを改変する 追加や削減など
    public Vector2 _move;
    public Vector2 _look;
    public bool _grapple_t1;   // L - Click
    public bool _grapple_t2;   // R - Click
    public bool _inventry;     // I - Key
    public bool _pause;        // Tab - Key

    public FramedDeviceInputContext(Vector2 move, Vector2 look, bool grapple1, bool grapple2, bool inventry, bool pause)   // ここのコンストラクタも同様ゲームに応じて書き換える
    {
        _move = move;
        _look = look;
        _grapple_t1 = grapple1;   // L - Click
        _grapple_t2 = grapple2;   // R - Click
        _inventry = inventry;     // I - Key
        _pause = pause;        // Tab - Key
    }
}
#endregion

public class FramedInputBuffer : MonoBehaviour
{
    List<FramedDeviceInputContext> _inputHistory = new();

    PlayerInputBinder _inputBinder;
    FramedInputBuffer _inputBuffer;
    Vector2 _mInput, _lInput;
    bool _grapple_t1;   // L - Click
    bool _grapple_t2;   // R - Click
    bool _inventry;     // I - Key
    bool _pause;        // Tab - Key

    /// <summary> 入力のキューをする </summary>
    /// <param name="context"></param>
    public void QueueInput(FramedDeviceInputContext context)
    {
        _inputHistory.Add(context);
    }

    /// <summary> 入力を取り出す </summary>
    /// <returns></returns>
    public (Vector2 move, Vector2 look, bool grapple_t1, bool grapple_t2, bool inventry, bool pause)
        EnQueueInputContext()
    {
        Vector2 move = Vector2.zero;
        Vector2 look = Vector2.zero;
        bool grapple_t1 = false;    // Normal One
        bool grapple_t2 = false;    // Grapple Like Path Finder One
        bool inventry = false;      // inventry
        bool pause = false;         // pause and settings

        FramedDeviceInputContext context;

        if (_inputHistory.Count > 0)
        {
            context = _inputHistory[0];
            _inputHistory.RemoveAt(0);

            move = context._move;
            look = context._look;
            grapple_t1 = _grapple_t1;
            grapple_t2 = _grapple_t2;
            inventry = _inventry;
            pause = _pause;
        }

        return (move, look, grapple_t1, grapple_t2, inventry, pause);
    }

    private void OnEnable()
    {
        GameObject.DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _inputBinder = GetComponent<PlayerInputBinder>();
        _inputBuffer = GetComponent<FramedInputBuffer>();
    }

    private void Update()
    {
        _mInput = _inputBinder.GetActionValueAs<Vector2>("Player", "Move");
        _lInput = _inputBinder.GetActionValueAs<Vector2>("PLayer", "Look");
        _grapple_t1 = _inputBinder.GetActionValueAsButton("Player", "Grapple_1");
        _grapple_t2 = _inputBinder.GetActionValueAsButton("Player", "Grapple_2");
        _inventry = _inputBinder.GetActionValueAsButton("Player", "Inventry");
        _pause = _inputBinder.GetActionValueAsButton("Player", "Pause");

        FramedDeviceInputContext context = new FramedDeviceInputContext(_mInput, _lInput, _grapple_t1, _grapple_t2, _inventry, _pause);

        _inputBuffer.QueueInput(context);
    }
}
