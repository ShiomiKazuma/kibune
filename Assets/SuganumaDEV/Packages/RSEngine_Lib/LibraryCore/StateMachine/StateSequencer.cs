// �Ǘ��� ����
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SLib
{
    namespace StateSequencer
    {
        /// <summary> �X�e�[�g�}�V���̋@�\��񋟂��� </summary>
        public class StateSequencer
        {
            // �ʏ�X�e�[�g
            HashSet<IState> _states = new HashSet<IState>();
            // Any�X�e�[�g����̃X�e�[�g
            HashSet<IState> _statesFromAnyState = new HashSet<IState>();
            // �g�����W�V����
            HashSet<StateMachineTransition> _transitions = new HashSet<StateMachineTransition>();
            // Any����̃g�����W�V����
            HashSet<StateMachineTransition> _transitionsFromAny = new HashSet<StateMachineTransition>();
            // ���ݓ˓����Ă���X�e�[�g
            IState _currentPlayingState;
            // ���ݓ˓����Ă���g�����W�V������
            string _currentTransitionName;
            // �X�e�[�g�}�V�����ꎞ��~�����̃t���O
            bool _bIsPausing = true;
            // �f���Q�[�g���J��
            public event Action<string> OnEntered;
            public event Action<string> OnUpdated;
            public event Action<string> OnExited;

            #region �o�^����
            /// <summary> �X�e�[�g�̓o�^ </summary>
            /// <param name="state"></param>
            public void ResistState(IState state)
            {
                _states.Add(state);
                if (_currentPlayingState == null) { _currentPlayingState = state; }
            }

            /// <summary> Any����̃X�e�[�g�̓o�^ </summary>
            /// <param name="state"></param>
            public void ResisteStateFromAny(IState state)
            {
                _statesFromAnyState.Add(state);
            }

            /// <summary> �����̃X�e�[�g�������ɓn���Ă��ׂĂ̓n���ꂽ�X�e�[�g��o�^ </summary>
            /// <param name="states"></param>
            public void ResistStates(List<IState> states)
            {
                foreach (IState state in states)
                {
                    _states.Add(state);
                    if (_currentPlayingState == null) { _currentPlayingState = state; }
                }
            }

            /// <summary> �����̃X�e�[�g�������ɓn���Ă��ׂĂ̓n���ꂽAny����̃X�e�[�g��o�^ </summary>
            /// <param name="states"></param>
            public void ResistStatesFromAny(List<IState> states)
            {
                foreach (IState state in _statesFromAnyState)
                {
                    _states.Add(state);
                }
            }

            /// <summary> �X�e�[�g�Ԃ̑J�ڂ̓o�^ </summary>
            /// <param name="from"></param>
            /// <param name="to"></param>
            /// <param name="name"></param>
            public void MakeTransition(IState from, IState to, string name)
            {
                var tmp = new StateMachineTransition(from, to, name);
                _transitions.Add(tmp);
            }

            /// <summary> Any�X�e�[�g����̑J�ڂ̓o�^ </summary>
            /// <param name="from"></param>
            /// <param name="to"></param>
            /// <param name="name"></param>
            public void MakeTransitionFromAny(IState to, string name)
            {
                var tmp = new StateMachineTransition(new DummyStateClass(), to, name);
                _transitionsFromAny.Add(tmp);
            }

            #endregion

            #region �X�V����
            /// <summary> �C�ӂ̃X�e�[�g�ԑJ�ڂ̑J�ڂ̏󋵂��X�V����B </summary>
            /// <param name="name"></param>
            /// <param name="condition2transist"></param>
            /// <param name="tType"></param>
            /// <param name="equalsTo"></param>
            public void UpdateTransition(string name, ref bool condition2transist, bool equalsTo = true, bool isTrigger = false)
            {
                if (_bIsPausing) return; // �����ꎞ��~���Ȃ�X�V�����͂��Ȃ��B
                foreach (var t in _transitions)
                {
                    // �J�ڂ���ꍇ // * �����𖞂����Ă���Ȃ�O�g�����W�V�����𖳎����Ă��܂��̂ł��̔��菈�����͂��ނ��� *
                    // �����J�ڏ����𖞂����Ă��đJ�ږ�����v����Ȃ�
                    if ((condition2transist == equalsTo) && t.Name == name)
                    {
                        if (t.SFrom == _currentPlayingState) // ���ݍ��X�e�[�g�Ȃ�
                        {
                            _currentPlayingState.Exit(); // �E�X�e�[�g�ւ̑J�ڏ����𖞂������̂Ŕ�����
                            OnExited(_currentTransitionName);
                            if (isTrigger) condition2transist = !equalsTo; // IsTrigger �� true�Ȃ�
                            _currentPlayingState = t.STo; // ���݂̃X�e�[�g���E�X�e�[�g�ɍX�V�A�J�ڂ͂��̂܂�
                            _currentPlayingState.Entry(); // ���݂̃X�e�[�g�̏���N���������Ă�
                            OnEntered(_currentTransitionName);
                            _currentTransitionName = name; // ���݂̑J�ڃl�[�����X�V
                        }
                    }
                    // �J�ڂ̏����𖞂����Ă͂��Ȃ����A�J�ڃl�[������v�i�X�V����Ă��Ȃ��Ȃ�j���݂̃X�e�[�g�̍X�V�������Ă�
                    else if (t.Name == name)
                    {
                        _currentPlayingState.Update();
                        OnUpdated(_currentTransitionName);
                    }
                } // �S�J�ڂ������B
            }

            /// <summary> ANY�X�e�[�g����̑J�ڂ̏������X�V </summary>
            /// <param name="name"></param>
            /// <param name="condition2transist"></param>
            /// <param name="equalsTo"></param>
            public void UpdateTransitionFromAnyState(string name, ref bool condition2transist, bool equalsTo = true, bool isTrigger = false)
            {
                if (_bIsPausing) return; // �����ꎞ��~���Ȃ�X�V�����͂��Ȃ��B
                foreach (var t in _transitionsFromAny)
                {
                    // �����J�ڏ����𖞂����Ă��đJ�ږ�����v����Ȃ�
                    if ((condition2transist == equalsTo) && t.Name == name)
                    {
                        _currentPlayingState.Exit(); // �E�X�e�[�g�ւ̑J�ڏ����𖞂������̂Ŕ�����
                        OnExited(_currentTransitionName);
                        if (isTrigger) condition2transist = !equalsTo; // �J�ڏ�����������
                        _currentPlayingState = t.STo; // ���݂̃X�e�[�g���E�X�e�[�g�ɍX�V�A�J�ڂ͂��̂܂�
                        _currentPlayingState.Entry(); // ���݂̃X�e�[�g�̏���N���������Ă�
                        OnEntered(_currentTransitionName);
                        _currentTransitionName = name; // ���݂̑J�ڃl�[�����X�V
                    }
                    // �J�ڂ̏����𖞂����Ă͂��Ȃ����A�J�ڃl�[������v�i�X�V����Ă��Ȃ��Ȃ�j���݂̃X�e�[�g�̍X�V�������Ă�
                    else if (t.Name == name)
                    {
                        _currentPlayingState.Update();
                        OnUpdated(_currentTransitionName);
                    }
                } // �S�J�ڂ������B
            }
            #endregion

            #region �N������
            /// <summary> �X�e�[�g�}�V�����N������ </summary>
            public void PopStateMachine()
            {
                _bIsPausing = false;
                _currentPlayingState.Entry();
            }
            #endregion

            #region �ꎞ��~����
            /// <summary> �X�e�[�g�}�V���̏����� </summary>
            public void PushStateMachine()
            {
                _bIsPausing = true;
            }
            #endregion
        }
        // �e�g�����W�V�����͖��O�����蓖�ĂĂ���
        /// <summary> �X�e�[�g�ԑJ�ڂ̏����i�[���Ă��� </summary>
        public class StateMachineTransition
        {
            IState _from;
            public IState SFrom => _from;
            IState _to;
            public IState STo => _to;
            string _name;
            public string Name => _name;
            public StateMachineTransition(IState from, IState to, string name)
            {
                _from = from;
                _to = to;
                _name = name;
            }
        }

        /// <summary> �X�e�[�g�Ƃ��ēo�^������N���X���p������ׂ��C���^�[�t�F�[�X </summary>
        public interface IState
        {
            public void Entry();
            public void Update();
            public void Exit();
        }

        /// <summary> �_�~�[�̃X�e�[�g�̃N���X </summary>
        class DummyStateClass : IState
        {
            public void Entry()
            {
            }

            public void Exit()
            {
            }

            public void Update()
            {
            }
        }

        /// <summary> �X�e�[�g�J�ڂ̃^�C�v </summary>
        enum StateMachineTransitionType
        {
            StandardState,      // �ʏ� 
            AnyState,           // ��t���[���̂ݑJ�� 
        }

        #region �X�e�[�g�}�V���A���p���\�z
        // �C�j�V�����C�Y����
        // �X�e�[�g�}�V���C���X�^���X���X�e�[�g�̓o�^
        // �g�����W�V�����̓o�^
        // �X�e�[�g�}�V���̍X�V

        // ���t���[������
        // �g�����W�V�����̏�Ԃ̍X�V
        #endregion
    }
}