using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLib;
// �쐬�� ����
namespace SLib
{
    namespace Inputs
    {
        /// <summary> ���͒l�̍\���́B�Q�[���ɉ����Ă�������������� </summary>
        public struct DeviceInputContext
        {
            // �Q�[���ɉ����Ă����̃t�B�[���h�����ς��� �ǉ���팸�Ȃ�
            public Vector2 _move;
            public Vector2 _look;
            public bool _fire;
            public bool _toggle;

            public DeviceInputContext(Vector2 move, Vector2 look, bool fire, bool toggle)   // �����̃R���X�g���N�^�����l�Q�[���ɉ����ď���������
            {
                _move = move;
                _look = look;
                _fire = fire;
                _toggle = toggle;
            }
        }

        /// <summary> ���̓o�b�t�@�̋@�\��񋟂���B�����Ŗ��t���[�����͂����߂Ă����Ă��� </summary>
        public class InputBuffer : MonoBehaviour
        {
            List<DeviceInputContext> _inputHistory = new List<DeviceInputContext>();

            PlayerInputBinder _inputBinder;
            InputBuffer _inputBuffer;
            Vector2 _mInput, _lInput;
            bool _fire;
            bool _toggle;

            /// <summary> ���͂̃L���[������ </summary>
            /// <param name="context"></param>
            public void QueueInput(DeviceInputContext context)
            {
                _inputHistory.Add(context);
            }

            /// <summary> ���͂����o�� </summary>
            /// <returns></returns>
            public (Vector2 move, Vector2 look, bool fire, bool toggle) EnQueueInputContext()
            {
                Vector2 move = Vector2.zero;
                Vector2 look = Vector2.zero;
                bool fire = false;
                bool toggle = false;

                DeviceInputContext context;

                if (_inputHistory.Count > 0)
                {
                    context = _inputHistory[0];
                    _inputHistory.RemoveAt(0);

                    move = context._move;
                    look = context._look;
                    fire = context._fire;
                    toggle = context._toggle;
                }

                return (move, look, fire, toggle);
            }

            private void OnEnable()
            {
                GameObject.DontDestroyOnLoad(this);
            }

            private void Start()
            {
                _inputBinder = GetComponent<PlayerInputBinder>();
                _inputBuffer = GetComponent<InputBuffer>();
            }

            private void Update()
            {
                _mInput = _inputBinder.GetActionValueAs<Vector2>("Player", "Move");
                _lInput = _inputBinder.GetActionValueAs<Vector2>("PLayer", "Look");
                _fire = _inputBinder.GetActionValueAsButton("Player", "Fire");

                DeviceInputContext context = new DeviceInputContext(_mInput, _lInput, _fire, false);

                _inputBuffer.QueueInput(context);
            }
        }
    }
}
