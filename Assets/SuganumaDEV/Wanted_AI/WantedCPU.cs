// �Ǘ��� ����
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static SLib.SLib;
using SLib.AI;
using SLib.StateSequencer;
using UnityEngine.UI;
public class WantedCPU : MonoBehaviour
{
    // �X�e�[�g�}�V��
    /// <summary> AI�̃X�e�[�g�x�[�X�ȏ������T�|�[�g���邽�߂̃X�e�[�g�}�V�� </summary>
    StateSequencer _stateMachine;

    // �X�e�[�g IState �� �h���N���X
    /// <summary> �f�t�H���g�X�e�[�g�F�p�g���[�� </summary>
    WantedAIStateDefault _sDef;
    /// <summary> �X�e�[�g�F���� </summary>
    WantedAIStateGaze _sGaze;
    /// <summary> �X�e�[�g�F�ǐ� </summary>
    WantedAIStateChase _sChase;
    /// <summary> �X�e�[�g�F�U�� </summary>
    WantedAIStateAttack _sAttack;
    /// <summary> �X�e�[�g�F���S </summary>
    WantedAIStateDeath _sDeath;

    /// <summary> �m�۔���f���Q�[�g </summary>
    public event Action OnPlayerCaptured;

    // �������̂ɕK�v
    NavMeshAgent _agent;

    // �e�����W
    [SerializeField, Range(0f, 50f)] float _sightRange;
    [SerializeField, Range(0f, 50f)] float _attackRange;
    // �e���C���}�X�N
    [SerializeField] LayerMask _targetLayer;
    [SerializeField] LayerMask _groundLayer;
    // �^�[�Q�b�g
    [SerializeField] Transform _target;
    [SerializeField] int _targetLayerNum;
    // �p�j�o�H
    [SerializeField] PathHolder _patrolingPath;
    // �̗�
    [SerializeField] float _health;
    // ��������܂ł̎���
    [SerializeField] float _gazingTime;
    // �C���W�P�[�^�摜
    [SerializeField] Sprite _sprtGazing;
    [SerializeField] Sprite _sprtFound;
    [SerializeField] Image _indicator;

    Transform _selfTransform;

    PauseManager _pman;

    // AI�g�����W�V�����t���O
    bool _isInsideSightRange = false; // �f�t�H���g���璍������܂ł̏���
    bool _isFoundTargetNow = false; // �������I���A�v���C���[�Ƃ��Ĕ��肵���ꍇ�@�ǐՂ��邩�̏���
    bool _isInsideAttackingRange = false; // �ǐՂ����Ă��čU���\�����Ƀv���C���[���������ꍇ�@�U�����邩�̏���
    bool _isNoHealthNow = false;�@// ���S�������ꍇ

    void OnPause()
    {
        _stateMachine.PushStateMachine();
    }

    void OnEndPause()
    {
        _stateMachine.PopStateMachine();
    }

    private void Awake()
    {
        _pman = GameObject.FindAnyObjectByType<PauseManager>();

        if (_target == null) _target = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _selfTransform = transform;

        // �X�e�[�g�}�V��������
        _stateMachine = new();

        // �e�X�e�[�g������
        _sDef = new(_agent, _selfTransform, _patrolingPath, _indicator
            , () =>
            {

            });
        _sGaze = new(_sightRange, _gazingTime, transform, _targetLayer, _agent
            , (tTransform) =>
            {
                var dir = (tTransform.position - transform.position).normalized;
                dir.y = 0;
                transform.forward = dir;
                _indicator.sprite = _sprtGazing;
                _indicator.color = Color.white;
                _indicator.fillAmount = (_sGaze.GazingElapsedTime / _gazingTime);
            } // On Gazing
            , (tTransform) =>
            {
                var dir = (tTransform.position - transform.position).normalized;
                dir.y = 0;
                transform.forward = dir;
                if (!_isFoundTargetNow) _isFoundTargetNow = true;
                _indicator.sprite = _sprtFound;
            } // On Target Found
            );

        _sChase = new(_sightRange, _targetLayer, transform, _agent
            , () =>
            {

            });
        _sAttack = new(_attackRange, transform, _targetLayer, _agent
            , () =>
            {
                Debug.Log("�U����....");
                Knock(OnPlayerCaptured == null, () => { Debug.LogWarning("�m�۔���̂��߂�Action�ւ̃C�x���g�o�^������Ă܂���"); });
                Knock(OnPlayerCaptured != null, OnPlayerCaptured);
            }
            );
        _sDeath = new(transform, _agent);

        // �e�X�e�[�g�̓o�^
        _stateMachine.ResistStates(new List<IState> {
        _sDef,
        _sGaze,
        _sChase,
        _sAttack,});

        _stateMachine.ResistStateFromAny(_sDeath);

        _stateMachine.MakeTransition(_sDef, _sGaze, "D2G"); // default to gaze id{0}
        _stateMachine.MakeTransition(_sGaze, _sDef, "G2D"); // gaze to default id{1}

        _stateMachine.MakeTransition(_sGaze, _sChase, "G2C"); // gaze to chase id{2}
        _stateMachine.MakeTransition(_sChase, _sGaze, "C2G"); // chase to default id{3}

        _stateMachine.MakeTransition(_sChase, _sAttack, "C2A"); // chase to attack id{4}
        _stateMachine.MakeTransition(_sAttack, _sChase, "A2C"); // attack to chase id{5}

        _stateMachine.MakeTransitionFromAny(_sDeath, "DummyTransition");

        _stateMachine.PopStateMachine();
    }

    private void OnEnable()
    {
        _pman.BeginPause += OnPause;
        _pman.EndPause += OnEndPause;
    }

    private void OnDisable()
    {
        _pman.BeginPause -= OnPause;
        _pman.EndPause -= OnEndPause;
    }

    private void FixedUpdate()
    {
        // ��������̔���
        _isInsideSightRange = Physics.CheckSphere(transform.position, _sightRange, _targetLayer);
        _isInsideAttackingRange = Physics.CheckSphere(transform.position, _attackRange, _targetLayer);
        if (!_isInsideSightRange && _isFoundTargetNow) _isFoundTargetNow = false;
        _isNoHealthNow = _health <= 0;

        // �e�X�e�[�g�X�V
        _sDef.UpdateSelf(transform);
        _sGaze.UpdateSelf(transform);
        _sChase.UpdateSelf(transform);
        _sAttack.UpdateSelf(transform);
        _sDeath.UpdateSelf(transform);

        // defalut to gaze
        _stateMachine.UpdateTransition("D2G", ref _isInsideSightRange);

        // gaze to deafult
        _stateMachine.UpdateTransition("G2D", ref _isInsideSightRange, !true);

        // gaze to chase
        _stateMachine.UpdateTransition("G2C", ref _isFoundTargetNow);

        // chase to gaze
        _stateMachine.UpdateTransition("C2G", ref _isFoundTargetNow, !true);

        // chase to attack
        _stateMachine.UpdateTransition("C2A", ref _isInsideAttackingRange);

        // attack to chase
        _stateMachine.UpdateTransition("A2C", ref _isInsideAttackingRange, !true);

        // any state to death
        _stateMachine.UpdateTransitionFromAnyState("DummyTransition", ref _isNoHealthNow, true, true);
    }

#if UNITY_EDITOR_64
    private void OnDrawGizmos()
    {
        // ����
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _sightRange);

        // �U���͈�
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
#endif
}