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
