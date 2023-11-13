using System.Collections.Generic;
using UnityEngine;
using RSEngine.AI.StateMachine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
/// <summary> AI�@�\��񋟂���R���|�[�l���g </summary>
public class WantedAITracker : MonoBehaviour, IStateMachineUser
{
    // �X�e�[�g�}�V��
    /// <summary> AI�̃X�e�[�g�x�[�X�ȏ������T�|�[�g���邽�߂̃X�e�[�g�}�V�� </summary>
    StateMachine _sMachine;

    // �X�e�[�g
    /// <summary> �f�t�H���g�X�e�[�g�F�A�C�h�� </summary>
    WantedAIStateDefault _sDef;
    /// <summary> �X�e�[�g�F���� </summary>
    WantedAIStateGaze _sGaze;
    /// <summary> �X�e�[�g�F�ǐ� </summary>
    WantedAIStateChase _sChase;
    /// <summary> �X�e�[�g�F�U�� </summary>
    WantedAIStateAttack _sAttack;
    /// <summary> �X�e�[�g�F���S </summary>
    WantedAIStateDeath _sDeath;

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
    // �ړ����x
    [SerializeField] float _movespeed;
    // �p�j�o�H
    [SerializeField] List<Transform> _patrollingPath;

    [SerializeField] float _health;

    // AI�g�����W�V�����t���O
    bool _isInsideSightRange = false; // �f�t�H���g���璍������܂ł̏���
    bool _isFoundTargetNow = false; // �������I���A�v���C���[�Ƃ��Ĕ��肵���ꍇ�@�ǐՂ��邩�̏���
    bool _isInsideAttackingRange = false; // �ǐՂ����Ă��čU���\�����Ƀv���C���[���������ꍇ�@�U�����邩�̏���
    bool _isNoHealthNow = false;�@// ���S�������ꍇ

    public void OnStateWasExitted(StateTransitionInfo info)
    {

    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        // �X�e�[�g�}�V��������
        _sMachine = new StateMachine();

        // �e�X�e�[�g������
        _sDef = new(_patrollingPath.ToArray(), _agent);
        _sGaze = new(_sightRange, 2, transform, _targetLayer, _agent
            , (tTransform) =>
            {
                var dir = (tTransform.position - transform.position).normalized;
                dir.y = 0;
                transform.forward = dir;
            } // On Gazing
            , (tTransform) =>
            {
                var dir = (tTransform.position - transform.position).normalized;
                dir.y = 0;
                transform.forward = dir;
                if (!_isFoundTargetNow) _isFoundTargetNow = true;
            } // On Target Found
            );
        _sChase = new(_sightRange, _targetLayer, transform, _agent);
        _sAttack = new(_attackRange, transform, _targetLayer, _agent, () => { Debug.Log("�U����...."); });
        _sDeath = new(transform, _agent);

        // �C�x���g���X�i�[�o�^
        _sMachine.onStateExit += OnStateWasExitted;

        // �e�X�e�[�g�̓o�^
        _sMachine.AddStates(new List<IState> {
        _sDef,
        _sGaze,
        _sChase,
        _sAttack,
        _sDeath});

        // �ʏ킩�璍��
        _sMachine.AddTransition(_sDef, _sGaze); // default to gaze id{0}
        _sMachine.AddTransition(_sGaze, _sDef); // gaze to default id{1}

        // ��������ǐ�
        _sMachine.AddTransition(_sGaze, _sChase); // gaze to chase id{2}
        _sMachine.AddTransition(_sChase, _sGaze); // chase to default id{3}

        //�@�ǐՂ���U��
        _sMachine.AddTransition(_sChase, _sAttack); // chase to attack id{4}
        _sMachine.AddTransition(_sAttack, _sChase); // attack to chase id{5}

        //// ���S�X�e�[�g
        _sMachine.AddTransition(_sDef, _sDeath); // id{6}
        _sMachine.AddTransition(_sGaze, _sDeath); // id{7}
        _sMachine.AddTransition(_sChase, _sDeath); // id{8}
        _sMachine.AddTransition(_sAttack, _sDeath); // id{9}

        // �X�e�[�g�}�V���N��
        _sMachine.Initialize();
    }

    private void OnDisable()
    {
        //�@���X�i�[�o�^����
        _sMachine.onStateExit -= OnStateWasExitted;
    }

    private void FixedUpdate()
    {
        // ��������̔���
        _isInsideSightRange = Physics.CheckSphere(transform.position, _sightRange, _targetLayer);
        _isInsideAttackingRange = Physics.CheckSphere(transform.position, _attackRange, _targetLayer);
        if (!_isInsideSightRange && _isFoundTargetNow) _isFoundTargetNow = false;
        _isNoHealthNow = _health <= 0;

        // �e�X�e�[�g�X�V
        _sDef.Update(transform);
        _sGaze.Update(transform);
        _sChase.Update(transform);
        _sAttack.Update(transform);
        _sDeath.Update(transform);

        // defalut to gaze
        _sMachine.UpdateTransitionCondition(0, _isInsideSightRange);

        // gaze to deafult
        _sMachine.UpdateTransitionCondition(1, !_isInsideSightRange);

        // gaze to chase
        _sMachine.UpdateTransitionCondition(2, _isFoundTargetNow);

        // chase to gaze
        _sMachine.UpdateTransitionCondition(3, !_isFoundTargetNow);

        // chase to attack
        _sMachine.UpdateTransitionCondition(4, _isInsideAttackingRange);

        // attack to chase
        _sMachine.UpdateTransitionCondition(5, !_isInsideAttackingRange);

        //// any state to death
        _sMachine.UpdateTransitionCondition(6, _isNoHealthNow);
        _sMachine.UpdateTransitionCondition(7, _isNoHealthNow);
        _sMachine.UpdateTransitionCondition(8, _isNoHealthNow);
        _sMachine.UpdateTransitionCondition(9, _isNoHealthNow);

        // update statemachine
        _sMachine.Update();
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