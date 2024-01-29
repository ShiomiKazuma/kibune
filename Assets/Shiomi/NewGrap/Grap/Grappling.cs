using UnityEngine;

public class Grappling : MonoBehaviour
{
    PlayerMovementGrappling _pm;
    [Header("カメラ")]
    public Transform _cam;
    public Transform _gunTip;
    [Header("グラップリングが当たるレイヤー")]
    public LayerMask _grappleable;
    public LineRenderer _lr;

    [Header("グラッブできる最大距離")]
    public float _maxGrappleDistance;
    [Header("フックが伸びているアニメーションの時間")]
    public float _grappleDelayTime;
    [Header("どれだけ最高到達点を高くするか")]
    public float _overshootYAxis;
    Vector3 _grapplePoint;

    [Header("グラップリングのクールタイム")]
    public float _grapplingCT;
    float _grapplingCTTimer;

    [Header("グラップリングするインプットキー")]
    public KeyCode grappleKey = KeyCode.Mouse1;
    bool Isgrappling;

    [SerializeField, Header("カザキリパーティクル")]
    ParticleSystem _particle;

    PlayerCrossHair _crossHair; // Players Cross Hair

    void Start()
    {
        _pm = GetComponent<PlayerMovementGrappling>();
        _crossHair = GameObject.FindAnyObjectByType<PlayerCrossHair>();
    }

    void Update()
    {
        if (Input.GetKeyDown(grappleKey))
            StartGrapple();
        //クールダウンタイマーを減らす処理
        if (_grapplingCTTimer > 0)
            _grapplingCTTimer -= Time.deltaTime;

        // グラップル可能かの判定
        RaycastHit hit;
        var stat = Physics.Raycast(_cam.position, _cam.forward, out hit, _maxGrappleDistance, _grappleable);
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
            Vector3 temp = _cam.position;
            temp.y = _cam.position.y - 2;
            _lr.SetPosition(0, temp);
        }

    }
    void StartGrapple()
    {
        if (_grapplingCTTimer > 0) return;
        Isgrappling = true;
        _pm._freeze = true;
        RaycastHit hit;
        var stat = Physics.Raycast(_cam.position, _cam.forward, out hit, _maxGrappleDistance, _grappleable);
        if (stat)
        {
            _grapplePoint = hit.point;
            _lr.SetPosition(1, _grapplePoint);
            //フックを出現させる
            _lr.enabled = true;
            Invoke(nameof(ExecuteGrapple), _grappleDelayTime);
        }
        else
        {
            //_grapplePoint = _cam.position + _cam.forward * _maxGrappleDistance;
            Invoke(nameof(StopGrapple), _grappleDelayTime);
        }
    }

    void ExecuteGrapple()
    {
        _pm._freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = _grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + _overshootYAxis;
        if (grapplePointRelativeYPos < 0)
            highestPointOnArc = _overshootYAxis;

        _pm.JumpToPosition(_grapplePoint, highestPointOnArc);
        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        _pm._freeze = false;
        Isgrappling = false;
        _grapplingCTTimer = _grapplingCT;
        _lr.enabled = false;
    }

    public Vector3 GetGrapplePoint()
    {
        return _grapplePoint;
    }
}
