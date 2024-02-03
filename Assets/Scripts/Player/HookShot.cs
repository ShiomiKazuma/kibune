using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour
{
    [SerializeField, Header("カメラ")]
    Transform cam;
    [SerializeField]
    Transform gunTip;
    [SerializeField, Header("Grapple Origin")]
    Transform grappleOrigin;
    [SerializeField, Header("グラップリングが当たるレイヤー")]
    LayerMask grapableLayer;
    [SerializeField]
    LineRenderer lineRend;

    [SerializeField, Header("グラッブできる最大距離")]
    float maxGrappleDistance;
    [SerializeField, Header("硬直時間")]
    float grappleDelayTime;
    [SerializeField, Header("最高到達点(相対)")]
    float overshootYAxis;

    [SerializeField, Header("グラップリングのクールタイム")]
    float _grapplingCT;

    float _grapplingCTThreshold;

    [SerializeField, Header("グラップリングするインプットキー")]
    KeyCode grappleKey = KeyCode.Mouse0;
    bool Isgrappling;

    [SerializeField, Header("カザキリパーティクル")]
    ParticleSystem _particle;

    [SerializeField, Header("グラップリングフック")]
    GameObject _hook;

    [SerializeField, Header("Aim Input")]
    KeyCode input_aim = KeyCode.Mouse1;

    PlayerCrossHair _crossHair; // Players Cross Hair
    PlayerMovementGrappling _playerMove;
    PauseManager _pMan;

    Vector3 _grapplePoint;
    bool _pausedOverride;

    void AimGrapple()
    {
        if (_grapplingCTThreshold > 0) return;

        Isgrappling = true;
        _playerMove._freeze = true;

        RaycastHit hit;
        var stat = Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, grapableLayer);
        if (stat)
        {
            _grapplePoint = hit.point;
            lineRend.SetPosition(0, grappleOrigin.position);
            lineRend.SetPosition(1, _grapplePoint);
            //フックを出現させる
            lineRend.enabled = true;
            _hook.SetActive(true);
            _hook.transform.position = _grapplePoint;
            if (Input.GetKey(grappleKey))
            {
                ExecuteGrapple();
            }
            //Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            lineRend.SetPosition(0, grappleOrigin.position);
            lineRend.SetPosition(1, grappleOrigin.position);
            //フックを出現させる
            lineRend.enabled = false;
            _hook.SetActive(false);
            //_grapplePoint = cam.position + cam.forward * maxGrappleDistance;
            //Invoke(nameof(StopGrapple), grappleDelayTime);
        }
    }

    void ExecuteGrapple()
    {
        _playerMove._freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = _grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;
        if (grapplePointRelativeYPos < 0)
            highestPointOnArc = overshootYAxis;

        _playerMove.JumpToPosition(_grapplePoint, highestPointOnArc);
        Invoke(nameof(StopGrapple), 1f);

        _hook.SetActive(true);
        _hook.transform.position = _grapplePoint;
        _hook.transform.LookAt((_grapplePoint - this.transform.position).normalized);
        _hook.transform.position = _hook.transform.position - ((_grapplePoint - this.transform.position).normalized * 1);
    }

    public void StopGrapple()
    {
        _playerMove._freeze = false;
        Isgrappling = false;
        _grapplingCTThreshold = _grapplingCT;
        lineRend.enabled = false;

        _hook.SetActive(false);
        _hook.transform.position = this.transform.position;
    }

    public Vector3 GetGrapplePoint()
    {
        return _grapplePoint;
    }

    void OnPaused()
    {
        _pausedOverride = true;
    }

    void OnEndPaused()
    {
        _pausedOverride = false;
    }

    private void Awake()
    {
        _pMan = GameObject.FindFirstObjectByType<PauseManager>();
    }

    private void OnEnable()
    {
        _pMan.BeginPause += OnPaused;
        _pMan.EndPause += OnEndPaused;
    }

    private void OnDisable()
    {
        _pMan.BeginPause -= OnPaused;
        _pMan.EndPause -= OnEndPaused;
    }

    void Start()
    {
        _playerMove = GetComponent<PlayerMovementGrappling>();
        _crossHair = GameObject.FindAnyObjectByType<PlayerCrossHair>();
        _hook.SetActive(false);
    }

    void Update()
    {
        if (_pausedOverride) return;

        if (Input.GetKeyDown(input_aim))
            AimGrapple();
        //クールダウンタイマーを減らす処理
        if (_grapplingCTThreshold > 0)
            _grapplingCTThreshold -= Time.deltaTime;

        // グラップル可能かの判定
        RaycastHit hit;
        var stat = Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, grapableLayer);
        if (_crossHair == null) _crossHair = GameObject.FindAnyObjectByType<PlayerCrossHair>();
        _crossHair.SetGrappling(stat);

        if (Isgrappling)
        {
            _crossHair.SetCrossHairStatus(PlayerCrossHair.CrossHairStatus.Close);
            _particle.Play();
        }
        else
        {
            _crossHair.SetCrossHairStatus(PlayerCrossHair.CrossHairStatus.Deploy);
            _particle.Stop();
        }
    }

    private void LateUpdate()
    {
        if (Isgrappling)
        {
            Vector3 temp = cam.position;
            temp.y = cam.position.y - 2;
            lineRend.SetPosition(0, temp);
        }
    }
}
