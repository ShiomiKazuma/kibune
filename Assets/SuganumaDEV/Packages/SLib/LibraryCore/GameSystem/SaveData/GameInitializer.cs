using UnityEngine;
using SLib.Systems;
using UnityEngine.Splines;
using System.Linq;
// �쐬 ����
public class GameInitializer : MonoBehaviour
{
    enum SceneType
    {
        InGame,
        LastEvent,
    }

    [SerializeField, Header("�V�[���I��")]
    SceneType type;

    PlayerSaveDataSerializer _dataSerializer;

    public void InitializePlayer()      // �Q�[���V�[���ǂݍ��݌�ɂ����ǂݍ���
    {
       _dataSerializer = GameObject.FindObjectOfType<PlayerSaveDataSerializer>();
        var saveDataTemplate = _dataSerializer.ReadSaveData();
        var go = GameObject.FindGameObjectWithTag("Player");
        go.SetActive(false);
        go.transform.position = saveDataTemplate._lastStandingPosition;
        go.transform.rotation = saveDataTemplate._lastStandingRotation;
        go.SetActive(true);
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
        if (type == SceneType.LastEvent)
        {
            InitializeInventory();
            var player = GameObject.FindGameObjectWithTag("Player");
            var tr = GameObject.FindGameObjectWithTag("LastEventStart_Pos").transform;
            var rot = tr.rotation;
            var pos = tr.position;
            player.transform.position = pos;
            player.transform.rotation = rot;

            var feeder = GameObject.FindObjectOfType<DialogueFeeder>();
            var canvas = feeder.gameObject.GetComponent<CanvasGroup>();
            canvas.alpha = 0;
        }
        else
        {
            InitializePlayer();
            InitializeInventory();
            InitializeVehicles();
        }
    }

}
