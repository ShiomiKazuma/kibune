using UnityEngine;

public class SwingGrap : MonoBehaviour
{
    [Header("インプットキー")] public KeyCode _swingKey = KeyCode.Mouse1;
    public LineRenderer _lr;
    public Transform _gunTip, _cam, _player;
    public LayerMask IsGrappleable;
    public PlayerMovementGrappling _pm;
    float _maxSwingDistance = 25f;
    Vector3 _swingPoint;
    SpringJoint _joint;

    //Prediction
    public RaycastHit _predictionHit;
    public float _predictionSphereCastRadius;
    public Transform _predictionPoint;

    PlayerCrossHair _crossHair; // Players Cross Hair

    private void Start()
    {
        _crossHair = GameObject.FindAnyObjectByType<PlayerCrossHair>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_swingKey)) StartSwing();
        if (Input.GetKeyUp(_swingKey)) StopSwing();
        CheckForSwingPoints();
        if (_joint != null) OdmGearMoveMent();

        _crossHair.SetGrappling(_pm._swinging);
        if (_pm._swinging)
        {
            _crossHair.SetCrossHairStatus(PlayerCrossHair.CrossHairStatus.Close);
        }
        else
        {
            _crossHair.SetCrossHairStatus(PlayerCrossHair.CrossHairStatus.Deploy);
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void CheckForSwingPoints()
    {
        if(_joint != null) return;
        RaycastHit sphereCastHit;
        Physics.SphereCast(_cam.position, _predictionSphereCastRadius, _cam.forward, out sphereCastHit, _maxSwingDistance, IsGrappleable);
        RaycastHit raycastHit;
        // グラップルできるかのフラグ
        Physics.Raycast(_cam.position, _cam.forward, out raycastHit, _maxSwingDistance, IsGrappleable);

        Vector3 realHitPoint;
        if(raycastHit.point != Vector3.zero)
            realHitPoint = raycastHit.point;
        else if(sphereCastHit.point != Vector3.zero)
            realHitPoint = sphereCastHit.point;
        else
            realHitPoint = Vector3.zero;

        if(realHitPoint != Vector3.zero)
        {
            _predictionPoint.gameObject.SetActive(true);
            _predictionPoint.position = realHitPoint;
        }
        else
        {
            _predictionPoint.gameObject.SetActive(false);
        }

        _predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
    }
    void StartSwing()
    {
        if (_predictionHit.point == Vector3.zero) return;
        if(GetComponent<Grappling>() != null)
            GetComponent<Grappling>().StopGrapple();
        _pm.ResetRestrictions();

        _pm._swinging = true;
        
        _swingPoint = _predictionHit.point;
        _joint = _player.gameObject.AddComponent<SpringJoint>();
        _joint.autoConfigureConnectedAnchor = false;
        _joint.connectedAnchor = _swingPoint;
        float distanceFromPoint = Vector3.Distance(_player.position, _swingPoint);

        _joint.maxDistance = distanceFromPoint * 0.8f;
        _joint.minDistance = distanceFromPoint * 0.25f;

        _joint.spring = 4.5f;
        _joint.damper = 7f;
        _joint.massScale = 4.5f;
        
        _lr.positionCount = 2;
        _currentGrapplePosition = _gunTip.position;
    }

    public void StopSwing()
    {
        _pm._swinging = false;
        _lr.positionCount = 0;
        Destroy(_joint);
    }

    Vector3 _currentGrapplePosition;
    void DrawRope()
    {
        if (!_joint) return;
        _currentGrapplePosition = Vector3.Lerp(_currentGrapplePosition, _swingPoint, Time.deltaTime * 8f);
        Vector3 temp = _gunTip.position;
        temp.y = _gunTip.position.y - 2;
        _lr.SetPosition(0, temp);
        //_lr.SetPosition(0, _gunTip.position);
        _lr.SetPosition(1, _swingPoint);
    }

    public Transform _orientation;
    public Rigidbody _rb;
    public float _horizontalThrustForce;
    public float _extendCableSpeed;

    void OdmGearMoveMent()
    {
        //right
        if(Input.GetKey(KeyCode.D)) _rb.AddForce(_orientation.right * _horizontalThrustForce * Time.deltaTime);
        //left
        if (Input.GetKey(KeyCode.A)) _rb.AddForce(-_orientation.right * _horizontalThrustForce * Time.deltaTime);
        //forward
        if (Input.GetKey(KeyCode.W)) _rb.AddForce(_orientation.forward * _horizontalThrustForce * Time.deltaTime);
        //短くする
        if(Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = _swingPoint - transform.position;
            _rb.AddForce(directionToPoint.normalized * _horizontalThrustForce * Time.deltaTime);
            float distanceFromPoint = Vector3.Distance(transform.position, _swingPoint);
            _joint.maxDistance = distanceFromPoint * 0.8f;
            _joint.minDistance = distanceFromPoint * 0.25f;
        }
        //長くする
        if(Input.GetKey(KeyCode.S))
        {
            float extendedDistanceromPoint = Vector3.Distance(transform.position, _swingPoint) + _extendCableSpeed;
            _joint.maxDistance = extendedDistanceromPoint * 0.8f;
            _joint.minDistance = extendedDistanceromPoint * 0.25f;
        }
    }
}
