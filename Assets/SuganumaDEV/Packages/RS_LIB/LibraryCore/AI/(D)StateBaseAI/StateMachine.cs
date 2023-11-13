using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace RSEngine
{
    namespace AI
    {
        namespace StateMachine
        {
            #region README�h�L�������g
            /* ----- README -----
             * ����StateMachine�N���X�̗��p���� IStateMachineUser �� �p�����邱�ƁB
             * ����StateMachine�N���X�͈ȉ��̏������Ƃɏ��������Ă���
             * |---------------------|
             * |StatePairedTransition|
             * |---------------------|
             * -----------------------------------------------|
             * ����̓g�����W�V�����ŁA�Q�̃X�e�[�g���������Ă���    |
             * [State A] ---> [State B] �� ���Ɠ���           |
             * �J�ڌ�(From) : [State A]|�J�ڐ�(To) : [State B]  |
             * �Ƃ���                                          |
             * �l�X�ȏ���ێ����Ă���                           |
             * |----------------------------------------------|
             * |------|
             * |IState|
             * |------|
             * -----------------------------------------------------------------------|
             * [State A] �̂悤�ȃX�e�[�g�Ƃ��Ĉ����^                                     |
             * �����C�ӂ̃N���X(�C���^�[�t�F�C�X���p��)�Ōp������                           |
             * �X�e�[�g�}�V�����тɃX�e�[�g�y�A�g�����W�V�����ň������[�U�[��`�N���X�͂��� IState|
             * ���p�����Ȃ���΂Ȃ�Ȃ��B                                                 |
             * -----------------------------------------------------------------------|
             * �X�e�[�g�}�V�����p���͈ȉ��̎菇�ŃX�e�[�g�}�V�����������ƁB
             * Awake���\�b�h��-----------------|
             * (0)�X�e�[�g�}�V���̃C���X�^���X��   |
             * (1)�eIState�p���N���X�̓o�^       |
             * (2)�e�X�e�[�g�g�����W�V�����̓o�^   |
             * (3)�X�e�[�g�}�V���N��             |
             * -------------------------------|
             * FixedUpdate��--------------------------|
             * (0)�e�g�����W�V�����̃R���f�B�V�����̍X�V     |
             * (1)�X�e�[�g�}�V����Update���\�b�h�̌Ăяo��  |
             * ----------------------------------------|
               ----- README -----*/
            #endregion
            /// <summary> �X�e�[�g�}�V���̋@�\��񋟂��� </summary>
            public class StateMachine
            {
                /// <summary> �X�e�[�g�y�A�̑J�ڃ��X�g�i�d��X�j </summary>
                /// �d�������Ȃ�
                HashSet<StatePairedTransition> _htransition = new();

                /// <summary> �X�e�[�g�y�A�̑J�ڃ��X�g </summary>
                List<StatePairedTransition> _transition = new();

                /// <summary> �O�t���[���Ŏ��s�����J�ڂ̗��� </summary>
                StatePairedTransition _ptransition;

                /// <summary> �X�e�[�g�̃��X�g�i�d��X�j </summary>
                /// �d�������Ȃ�
                HashSet<IState> _hstates = new();

                /// <summary> �X�e�[�g�̃��X�g </summary>
                List<IState> _states = new();

                /// <summary> ���݂̃X�e�[�g </summary>
                IState _currentState;

                /// <summary> ���ݎ��s���̃X�e�[�g </summary>
                int _currentTransitionIndex = -1;

                /// <summary> �X�e�[�g���甲�����Ƃ��ɃX�e�[�g�}�V�����ŌĂяo�����C�x���g </summary>
                /// <param name="info"></param>
                public delegate void OnStateExit(StateTransitionInfo info);

                /// <summary> �R�[���o�b�N���X�i�[�̓o�^��̃f���Q�[�g </summary>
                public event OnStateExit onStateExit;

                #region Machine
                /// <summary> �X�e�[�g�}�V���N�����ɌĂяo���B�X�e�[�g�}�V�����N������ </summary>
                public void Initialize()
                {
                    _transition = _htransition.ToList();
                    _states = _hstates.ToList();
                    _currentTransitionIndex = 0;
                }

                /// <summary> ���t���[���Ăяo�����\�b�h </summary>
                public void Update()
                {
                    // �X�e�[�g�̎��s
                    var currentTransition = _transition[_currentTransitionIndex];
                    _currentState = currentTransition.Current;
                    _currentState.Do();
                    // �C�x���g����
                    onStateExit.Invoke
                        (new StateTransitionInfo(currentTransition.GetState(0)
                        , currentTransition.GetState(1)
                        , currentTransition.GetTransitionId()));
                }
                #endregion

                #region Transition
                /// <summary> �J��id���w�肵�Ă���ɑΉ��������������� </summary>
                /// <param name="transitionID"></param>
                /// <param name="condition"></param>
                public void UpdateTransitionCondition(int transitionID, bool condition)
                {
                    var transition = _transition[transitionID];
                    // ���O�ɍs�����J�ڂŐ���������ꂽ��J�ڂ�����
                    if (_ptransition == null) // �������Ȃ��Ȃ�o�^
                    {
                        var tTransition = _transition[transitionID];
                        _ptransition = new(tTransition.GetState(0), tTransition.GetState(1), transitionID);
                    }
                    else
                    {
                        // ��j������ STATE-A => STATE-B �����݂��ASTATE-A => STATE-B => STATE-C �̑J�ڂ����悤�Ƃ��Ă�Ƃ��āA
                        // ���ۂɂ́ASTATE-A => STATE-B �� STATE-B => STATE-C �̃g�����W�V�����͈Ⴄ�ϐ��Ȃ̂ŁA
                        // TRANSITION-(B => C) �� STATE-B �� �g�����W�V�����̑J�ڐ�Ƃ��ė����Ɋ��蓖�Ă��Ă���Ȃ�A
                        // STATE-A => STATE-B �� STATE-B �� ���������ƂɂȂ��Ă���A���̂܂� STATE-B => STATE-C �̃g�����W�V���������Ă����Ȃ�
                        if (_ptransition.Current == transition.GetState(0))
                        {
                            if (_ptransition.GetState(1) == _ptransition.Current) // ���O�̍X�V�O�̗����̃X�e�[�g�̏�����
                            {
                                _ptransition.ResetTransition();
                            }
                            _currentTransitionIndex = transitionID;
                            _ptransition = _transition[transitionID];
                        }
                    }
                    transition.UpdateStateCondition(condition, _currentState);
                }

                /// <summary> �J�ڌ��ƑJ�ڐ�̏���ێ�����X�e�[�g�y�A��o�^����B </summary>
                /// <param name="from"></param>
                /// <param name="to"></param>
                public void AddTransition(IState from, IState to)
                {
                    _htransition.Add(new StatePairedTransition(from, to, _htransition.Count /* id => 0 ~ */));
                }

                /// <summary> ���ׂẴX�e�[�g�y�A�̑J�ڂ��N���A���� </summary>
                public void ClearTransition()
                {
                    _htransition.Clear();
                }
                #endregion

                #region State
                /// <summary> �X�e�[�g�̓o�^������ </summary>
                /// <param name="state"></param>
                public void AddState(IState state)
                {
                    _hstates.Add(state);
                }

                /// <summary> �X�e�[�g�̓o�^���������� </summary>
                /// <param name="state"></param>
                public void RemoveState(IState state)
                {
                    _hstates.Remove(state);
                }
                #endregion

                #region States
                /// <summary> ���X�g�`���œn���ꂽ�X�e�[�g��o�^���� </summary>
                /// <param name="states"></param>
                public void AddStates(List<IState> states)
                {
                    foreach (IState state in states)
                    {
                        _hstates.Add(state);
                    }
                }

                public void ClearAllStates()
                {
                    _hstates.Clear();
                }
                #endregion
            }

            // �J�ڌ��ƑJ�ڐ�
            /// <summary> �J�ڌ��ƑJ�ڐ�̏���ێ�����N���X </summary>
            /// <typeparam name="Tcurrent"> �J�ڌ� </typeparam>
            /// <typeparam name="Tnext"> �J�ڐ� </typeparam>
            public class StatePairedTransition
            {
                public StatePairedTransition(IState from, IState to, int transitionID)
                {
                    _from = from;
                    _to = to;
                    _current = _from;
                    this.transitionID = transitionID;
                }
                IState _from; // id = 0
                public IState From => _from;
                IState _to; // id = 1
                public IState To => _to;
                IState _current; // id => current State
                public IState Current => _current;
                int transitionID; // transition id non duplication
                /// <summary> �����������������ꂽ��J�ڐ�X�e�[�g�ֈڂ��ĂƂǂ܂�B </summary>
                /// <param name="condition"></param>
                public void UpdateStateCondition(bool condition, IState currentState)
                {
                    // ���������^�ł��܂�����̃X�e�[�g���J�ڌ��̎��ɂ̂ݎ��s�B
                    // ��x�����J�ڐ�Ɉڂ�B
                    if (condition && _current == _from)
                    {
                        if (currentState != null) currentState.Out();
                        _current = _to;
                        _current.In();
                    }
                }
                /// <summary> ���݂���X�e�[�g��Ԃ� </summary>
                /// <returns>0 : (�X�e�[�g�y�A�̑J�ڌ�) 1 : (�X�e�[�g�y�A�J�ڐ�)</returns>
                public int GetCurrentState()
                {
                    return (_current == _from) ? 0 : 1; // from => 0 : to => 1
                }
                /// <summary> �J��id��Ԃ� </summary>
                /// <returns></returns>
                public int GetTransitionId()
                {
                    return transitionID;
                }
                /// <summary> �w�肳�ꂽ�X�e�[�gid�̃X�e�[�g��Ԃ� </summary>
                /// <param name="stateId"></param>
                /// <returns></returns>
                public IState GetState(int stateId)
                {
                    return (stateId == 0) ? _from : _to;
                }
                /// <summary> �J�ڂ̏����� </summary>
                public void ResetTransition()
                {
                    _current = _from;
                }
            }

            /// <summary> ����G���g���[���Ă���X�e�[�g�y�A�̑J�ڂ̃X�e�[�g�̏���ێ�����\���� </summary>
            public struct StateTransitionInfo
            {
                public IState _from;
                public IState _to;
                public int _id;
                public StateTransitionInfo(IState from, IState to, int id)
                {
                    _from = from;
                    _to = to;
                    _id = id;
                }

                public override string ToString()
                {
                    return $"{_from}, {_to}, {_id}";
                }
            }

            /// <summary> �X�e�[�g�}�V���������J�ڃ��X�g�ɓo�^����N���X���p�����ׂ��C���^�[�t�F�C�X </summary>
            public interface IState
            {
                /// <summary> �X�e�[�g�˓����ɌĂяo����� </summary>
                public void In();
                /// <summary> �X�e�[�g�ʉߎ��ɌĂяo����� </summary>
                public void Do();
                /// <summary> �X�e�[�g�E�o���ɌĂяo����� </summary>
                public void Out();
            }

            /// <summary> �X�e�[�g�}�V�����p���N���X���p������ </summary>
            public interface IStateMachineUser
            {
                /// <summary> �X�e�[�g�� In(),Tick(),Out() �����ׂČĂяo��������ɔ��΂���C�x���g�̃��X�i�[ </summary>
                /// <param name="info"></param>
                public void OnStateWasExitted(StateTransitionInfo info);
            }
        }
    }
}