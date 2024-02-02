using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLib.Systems;
using UnityEditor;
using UnityEngine.Splines;
using System.Linq;
// 作成 菅沼
public class GameInitializer : MonoBehaviour
{
    PlayerSaveDataSerializer _dataSerializer;
    enum SceneType
    {
        InGame,
        LastScene,
    }
    [SerializeField, Header("シーンタイプ")]
    SceneType sceneType;

    public void InitializePlayer()      // ゲームシーン読み込み後にこれを読み込む
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

    void InitializeInventory()
    {
        var dataMan = GameObject.FindObjectOfType<FramedEventsInGameGeneralManager>();
        var idata = dataMan.ReadSaveData();
        var flagMan = GameObject.FindObjectOfType<FlagManager>();
        flagMan.OverwriteProgress(idata.Finished);
    }
    private static void InitializeVeicles()
    {
        var sanims = GameObject.FindObjectsOfType<SplineAnimate>().ToList();
        foreach (var item in sanims)
        {
            item.Play();
        }
    }

    void Awake()
    {
    }

    private void Start()
    {
        if (sceneType == SceneType.InGame)
        {
            InitializePlayer();
            InitializeInventory();
            InitializeVeicles();
        }
        else
        {
            InitializeInventory();
            var player = GameObject.FindGameObjectWithTag("Player");
            var pos = GameObject.FindGameObjectWithTag("LastEventStart_Pos").transform.position;
        }
    }

}
