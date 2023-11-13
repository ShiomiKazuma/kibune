using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RSEngine
{
    namespace Event
    {
        public class SimpleConversation : MonoBehaviour
        {
            [SerializeField] Text _convText; // ��b�̃e�L�X�g�\��
            [SerializeField] Image _convPanel; // �e�L�X�g��BG�p�l��
            [SerializeField] Button _convStartButton; // �h��b������h�{�^��
            [SerializeField] Button _convNextButton; // �h���ցh�{�^��
            [SerializeField] Transform _speakerTransform; // ��b��

            [SerializeField] LayerMask _playerLayer; // �v���C���[���C��

            [SerializeField, Range(1f, 5f)] float _conversationRange; // ��b�\����

            bool _isConversible = false; // ��b�X�^�[�g�����t���O

            public void OnStartConversation()
            {
                _convPanel.gameObject.SetActive(true);
                _isConversible = true;
            }

            private void Awake()
            {
                _convStartButton.gameObject.SetActive(false); // �݂��Ȃ�����
                _convPanel.gameObject.SetActive(false); // �݂��Ȃ�����
            }

            private void FixedUpdate()
            {
                bool canSpeak = Physics.CheckSphere(_speakerTransform.position, _conversationRange, _playerLayer); // ��b�\�����ɂ���Ȃ�
                _convStartButton.gameObject.SetActive(canSpeak); // ����
                if (!canSpeak && _isConversible)
                {
                    _convPanel.gameObject.SetActive(false); // �݂��Ȃ�����
                    _isConversible = false;
                }
            }

#if UNITY_EDITOR_64
            private void OnDrawGizmos()
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(_speakerTransform.position, _conversationRange);
            }
#endif
        }
    }
}