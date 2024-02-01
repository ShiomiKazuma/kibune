using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLib.Systems;
// ì¬ ›À
public class GameInitializer : MonoBehaviour
{
    PlayerSaveDataSerializer _dataSerializer;

    void Awake()
    {
    }

    private void Start()
    {
        InitializePlayer();
    }

    public void InitializePlayer()      // ƒQ[ƒ€ƒV[ƒ““Ç‚İ‚İŒã‚É‚±‚ê‚ğ“Ç‚İ‚Ş
    {
       _dataSerializer = GameObject.FindObjectOfType<PlayerSaveDataSerializer>();
        var saveDataTemplate = _dataSerializer.ReadSaveData();
        var go = GameObject.FindGameObjectWithTag("Player");
        go.SetActive(false);
        go.transform.position = saveDataTemplate._lastStandingPosition;
        go.transform.rotation = saveDataTemplate._lastStandingRotation;
        go.SetActive(true);

        Debug.Log("PLAYER POS");
    }
}
