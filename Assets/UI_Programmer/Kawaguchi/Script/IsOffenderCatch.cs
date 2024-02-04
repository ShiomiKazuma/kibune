using SLib.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

public class IsOffenderCatch : MonoBehaviour
{
    [Header("é‘ÇÃè„Ç…èÊÇ¡ÇΩéûÇÃèàóù")]
    [SerializeField] UnityEvent _event;

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
