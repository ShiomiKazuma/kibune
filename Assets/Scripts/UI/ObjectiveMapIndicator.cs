using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
#region 3DTransform To 2DScreenPosition
/// <summary> �^����ꂽ�R�����̕�������Q�����̕����̐��K�������x�N�g����Ԃ� </summary>
/// <param name="direction"></param>
/// <returns></returns>
//Vector2 Calculate3DPosTo2DViewPos(Vector3 target, Vector3 origin, Vector3 cameraForward, Vector3 cameraRight)
//{
/// �l����
/// 3d �ł̖ڕW�ƃv���C���[�̃x�N�g����D
/// 3d �ł̃v���C���[�̐��ʃx�N�g����F
/// 2d �ł̐��ʃx�N�g��F1��(0,1)
/// F ���牽�x��]����������D�����邩�m�肽���̂� F �E D �����߂�
/// �����2d ��ōČ�����΂悢����
#region Cos_
//// the vector player -> target
//Vector3 t = (target - origin).normalized, f = cameraForward.normalized;
//// calculate dotFT-product
//var dotP = Vector3.Dot(f, t);
//dotP = Mathf.Clamp(dotP, -1.0f, 1.0f);
//// calculate euler angle
//var angle = Mathf.Acos(dotP) * Mathf.Rad2Deg;
//// calculate vector rotated
//var vec = cameraForward;
//var result = Quaternion.Euler(0, angle, 0) * vec;
//var rvec = new Vector2(result.z, result.x);
//// get resolutions 
//var res = _canvasS.referenceResolution;
//rvec.Normalize();
//var posX = rvec.x * (res.x / 2.0f) + (res.x / 2.0f);
//var posY = rvec.y * (res.y / 2.0f) + (res.y / 2.0f);
//return new Vector2(posX, posY);
//Debug.Log($"vector res : {result.ToString()}");
#endregion

#region Atan2
// XZ���� �� forward-right����
// calculate angle
//Vector3 forward = target - cameraForward;
//Vector3 right = target - cameraRight;
//forward.Normalize();
//right.Normalize();
//float deg = Mathf.Atan2(forward.magnitude, right.magnitude);
//Vector3 dir = target - cameraForward;
//dir.Normalize();

//float deg = Mathf.Atan2(dir.z, dir.x);
//// calculate vector rotated
//var vec = cameraForward;
//var result = Quaternion.Euler(0, (deg < 0) ? deg + 360.0f : deg, 0) * vec;
//var rvec = new Vector2(result.x, result.z);
//// get resolutions 
//var res = _canvasS.referenceResolution;
//rvec.Normalize();
//var posX = rvec.x * (res.x / 2.0f) + (res.x / 2.0f);
//var posY = rvec.y * (res.y / 2.0f) + (res.y / 2.0f);
//return new Vector2(posX, posY);
//Debug.Log($"vector res : {result.ToString()}");
#endregion

#region DotPAcos-Atan2
//Vector3 f = cameraForward;
//Vector3 r = cameraRight;
//Vector3 t = target - origin;

//// calculate Dot
//float dotFT = Vector3.Dot(f, t);
//dotFT = Mathf.Clamp(dotFT, -1.0f, 1.0f);
//// calculate euler angle Acos
//var angleAcos = Mathf.Acos(dotFT) * Mathf.Rad2Deg;
//Vector3 resVec1 = Quaternion.Euler(0, angleAcos, 0) * cameraForward;

//// calculate angle Atan2
//// �ˉe�x�N�g�������߂� Atan��x
//Vector3 pjT = r.normalized * (t.magnitude * Mathf.Cos((90.0f - angleAcos)));
//// �����̃w�N�g�� Atan��y
//Vector3 h = t.normalized * Mathf.Sin((90.0f - angleAcos));
//float atan2 = Mathf.Atan2(h.magnitude, pjT.magnitude);
//Vector3 resVec2 = Quaternion.Euler(0, atan2, 0) * cameraForward;

//var result = resVec1 + resVec2;

//var rvec = new Vector2(result.x, result.z);
//// get resolutions 
//var res = _canvasS.referenceResolution;
//rvec.Normalize();
//// set screen position
//var posX = rvec.x * (res.x / 2.0f) + (res.x / 2.0f);
//var posY = rvec.y * (res.y / 2.0f) + (res.y / 2.0f);
//// return result

//Debug.Log($"Acos = {angleAcos}, Atan2 = {atan2}");

//return new Vector2(posX, posY);
#endregion
//}
#endregion
/// <summary> �}�b�v���ɖڕW�������\�� </summary>
public class ObjectiveMapIndicator : MonoBehaviour
{
    [SerializeField, Header("Indicator Sprite Image")]
    Image ImageIcon;
    [SerializeField, Header("Image Icon Size [px]")]
    Vector2 Size2D;
    [SerializeField, Header("Player Tag")]
    string PlayerTag;
    [SerializeField, Header("Camera Tag")]
    string CameraTag;
    [SerializeField, Header("Objective Tag")]
    string ObjTag;

    CanvasGroup _canvasG;
    CanvasScaler _canvasS;
    Transform _targetTf, _player;
    Camera _mainCam;
    RectTransform _rect;
    Vector3 _camForward;
    public Transform Target => _targetTf;

    public void SetTarget(Transform target)
    {
        _targetTf = target;
    }

    private void Start()
    {
        _canvasG = GetComponent<CanvasGroup>();
        _canvasS = GetComponent<CanvasScaler>();
        _player = GameObject.FindGameObjectWithTag(PlayerTag).transform;
        if (GameObject.FindGameObjectWithTag(CameraTag).GetComponent<Camera>() != null)
        {
            _mainCam = GameObject.FindGameObjectWithTag(CameraTag).GetComponent<Camera>();
        }
        else
        {
            _mainCam = GameObject.FindGameObjectWithTag(CameraTag).GetComponentInChildren<Camera>();
        }
        _rect = GetComponent<RectTransform>();
        if (_targetTf == null) _targetTf = GameObject.FindGameObjectWithTag(ObjTag).transform;
    }

    private void LateUpdate()
    {
        if (GameObject.FindAnyObjectByType<GameInfo>().SceneStatus != GameInfo.SceneTransitStatus.To_InGameScene)
        {
            return;
        }
        float canvasScale = transform.root.localScale.z;
        var center = 0.5f * new Vector3(Screen.width, Screen.height);

        var pos = _mainCam.WorldToScreenPoint(_targetTf.position) - center;
        if (pos.z < 0f)
        {
            pos.x = -pos.x;
            pos.y = -pos.y;

            if (Mathf.Approximately(pos.y, 0f))
            {
                pos.y = -center.y;
            }
        }

        var halfSize = 0.5f * canvasScale * _rect.sizeDelta;
        float d = Mathf.Max(
            Mathf.Abs(pos.x / (center.x - halfSize.x)),
            Mathf.Abs(pos.y / (center.y - halfSize.y))
        );


        bool isOffscreen = (pos.z < 0f || d > 1f);
        if (isOffscreen)
        {
            pos.x /= d;
            pos.y /= d;
        }
        _rect.anchoredPosition = pos / canvasScale;
    }
}
