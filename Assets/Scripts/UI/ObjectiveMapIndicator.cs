using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

/// <summary> マップ内に目標を強調表示 </summary>
public class ObjectiveMapIndicator : MonoBehaviour
{
    [SerializeField, Header("Indicator Sprite Image")]
    Image ImageIcon;
    [SerializeField, Header("Image Icon Size [px]")]
    Vector2 Size2D;
    [SerializeField, Header("Player Tag")]
    string PlayerTag;

    [SerializeField] Vector2 dir;
    [SerializeField] Transform target;

    CanvasGroup _canvasG;
    CanvasScaler _canvasS;
    Transform _targetTf, _player;
    public Transform Target => _targetTf;

    public void SetTarget(Transform target)
    {
        _targetTf = target;
    }

    /// <summary> 与えられた３次元の方向から２次元の方向の正規化したベクトルを返す </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    Vector2 Calculate3DPosTo2DViewPos(Vector3 direction)
    {
        // forward - backward AND left - right
        direction /= 2.0f;
        Vector2 dir2d = new(direction.x, direction.z);
        dir2d.Normalize();
        var resolution = _canvasS.referenceResolution;
        var posY = -(dir2d.y * (resolution.y / 2.0f)) + (resolution.y / 2.0f);
        var posX = (dir2d.x * (resolution.x / 2.0f)) + (resolution.x / 2.0f);
        return new Vector2 (posX, posY);
    }

    void SetIconScreenPos(Vector2 pos, Vector2 iconSize)
    {
        ImageIcon.rectTransform.position = pos;
    }

    private void Start()
    {
        _canvasG = GetComponent<CanvasGroup>();
        _canvasS = GetComponent<CanvasScaler>();
        //_canvasG.alpha = 0.0f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        var playerCAM = GameObject.FindObjectOfType<PlayerCam>();
        var lookAt = playerCAM.Forward;
        lookAt.y = 0;
        var pos = Calculate3DPosTo2DViewPos((_targetTf.position - lookAt));
        //var pos = Calculate3DPosTo2DViewPos(new Vector3(dir.x, 0f, dir.y));
        Debug.Log($"OBJECTIVE : {((_targetTf.position - _player.transform.position)).normalized.ToString()}");
        SetIconScreenPos(pos, Size2D);
    }
}
