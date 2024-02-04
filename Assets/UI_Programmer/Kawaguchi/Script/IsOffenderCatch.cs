using SLib.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

public class IsOffenderCatch : MonoBehaviour
{
    [Header("�Ԃ̏�ɏ�������̏���")]
    [SerializeField] UnityEvent _event;

    [SerializeField, Header("�A�j���[�V�������I������Ƃ��ɔ��΂���C�x���g")]
    UnityEvent _animEnd;

    SplineAnimate _sanim;

    bool _isInvokedOnAnimEnd;

    private void Awake()
    {
        _sanim = gameObject.transform.parent.GetComponent<SplineAnimate>();
    }

    private void Update()
    {
        if (_sanim.ElapsedTime >= _sanim.Duration && !_isInvokedOnAnimEnd)
        {
            _animEnd.Invoke();
            _isInvokedOnAnimEnd = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var scene = GameObject.FindFirstObjectByType<SceneLoader>();
            var hud = GameObject.FindAnyObjectByType<HUDManager>();
            hud.ToFront(0);
            GameObject.Destroy(GameObject.FindGameObjectWithTag("Player"));
            GameObject.Destroy(GameObject.FindAnyObjectByType<MoveCamera>());
            GameObject.Destroy(GameObject.FindAnyObjectByType<PlayerCam>());
            _event.Invoke();
            scene.LoadSceneByName("Epilougue");
        }
    }
}
