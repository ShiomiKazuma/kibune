using UnityEngine;

public class Grappling : MonoBehaviour
{
    PlayerMovementGrappling _pm;
    [Header("�J����")]
    public Transform _cam;
    public Transform _gunTip;
    [Header("�O���b�v�����O�������郌�C���[")]
    public LayerMask _grappleable;
    public LineRenderer _lr;

    [Header("�O���b�u�ł���ő勗��")]
    public float _maxGrappleDistance;
    [Header("�t�b�N���L�тĂ���A�j���[�V�����̎���")]
    public float _grappleDelayTime;
    [Header("�ǂꂾ���ō����B�_���������邩")]
    public float _overshootYAxis;
    Vector3 _grapplePoint;

    [Header("�O���b�v�����O�̃N�[���^�C��")]
    public float _grapplingCT;
    float _grapplingCTTimer;

    [Header("�O���b�v�����O����C���v�b�g�L�[")]
    public KeyCode grappleKey = KeyCode.Mouse1;
    bool Isgrappling;

    [SerializeField, Header("�J�U�L���p�[�e�B�N��")]
    ParticleSystem _particle;

    [SerializeField, Header("�O���b�v�����O�t�b�N")]
    GameObject _hook;

    PlayerCrossHair _crossHair; // Players Cross Hair
    PauseManager _pMan;

    bool _pausedOverride;

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
            //�t�b�N���o��������
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

        _hook.SetActive(true);
        _hook.transform.position = _grapplePoint;
        _hook.transform.LookAt((_grapplePoint - this.transform.position).normalized);
        _hook.transform.position = _hook.transform.position - ((_grapplePoint - this.transform.position).normalized * 1);
    }

    public void StopGrapple()
    {
        _pm._freeze = false;
        Isgrappling = false;
        _grapplingCTTimer = _grapplingCT;
        _lr.enabled = false;

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
        _pm = GetComponent<PlayerMovementGrappling>();
        _crossHair = GameObject.FindAnyObjectByType<PlayerCrossHair>();
        _hook.SetActive(false);
    }

    void Update()
    {
        if (_pausedOverride) return;

        if (Input.GetKeyDown(grappleKey))
            StartGrapple();
        //�N�[���_�E���^�C�}�[�����炷����
        if (_grapplingCTTimer > 0)
            _grapplingCTTimer -= Time.deltaTime;

        // �O���b�v���\���̔���
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
}
