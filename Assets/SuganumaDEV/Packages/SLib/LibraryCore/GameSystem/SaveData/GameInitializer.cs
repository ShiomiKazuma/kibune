using UnityEngine;
using SLib.Systems;
using UnityEngine.Splines;
using System.Linq;
using UnityEngine.SceneManagement;
// çÏê¨ êõè¿
public class GameInitializer : MonoBehaviour
{
    PlayerSaveDataSerializer _dataSerializer;

    public void InitializePlayer()      // ÉQÅ[ÉÄÉVÅ[Éìì«Ç›çûÇ›å„Ç…Ç±ÇÍÇì«Ç›çûÇﬁ
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

    private static void InitializeVehicles()
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
        if (SceneManager.GetActiveScene().name != "LastScene")
        {
            InitializePlayer();
            InitializeInventory();
            InitializeVehicles();
        }
        else
        {
            InitializeInventory();
            var player = GameObject.FindGameObjectWithTag("Player");
            var pos = GameObject.FindGameObjectWithTag("LastEventStart_Pos").transform.position;
        }
    }

}
