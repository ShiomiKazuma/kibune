using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// �쐬 ����
// ���H �ܓ�
public class SimpleConversation : MonoBehaviour
{
    [SerializeField] Text _convText; // ��b�̃e�L�X�g�\��
    [SerializeField] CanvasGroup _convPanel; // �e�L�X�g��BG�p�l��
    //[SerializeField] Button _convStartButton; // �h��b������h�{�^��
    //[SerializeField] Button _convNextButton; // �h���ցh�{�^��
    [SerializeField] Transform _speakerTransform; // ��b��
    [SerializeField] DialogueFeeder _dialogueFeeder;

    [SerializeField] LayerMask _playerLayer; // �v���C���[���C��

    [SerializeField, Range(1f, 5f)] float _conversationRange; // ��b�\����

    bool _isConversible = false; // ��b�X�^�[�g�����t���O
    bool _isTextEnd = false;

    public void OnStartConversation()
    {
        _convPanel.gameObject.SetActive(true);
        _isConversible = true;
    }

    private void Awake()
    {
        //_convStartButton.gameObject.SetActive(false); // �݂��Ȃ�����
        //_convPanel.gameObject.SetActive(false); // �݂��Ȃ�����
        _convPanel.alpha = 0;
        _convPanel.blocksRaycasts = false;
        _convPanel.interactable = false;
        _isConversible = false;
    }

    private void FixedUpdate()
    {
        bool canSpeak = Physics.CheckSphere(_speakerTransform.position, _conversationRange, _playerLayer); // ��b�\�����ɂ���Ȃ�
        //_convPanel.gameObject.SetActive(canSpeak);

        if (_isConversible ^ canSpeak)   // �n�b�g�ƓǂށB�r���I�_���a OR�ł͂�������Ă΂�Ă��܂����ߗǂ��Ȃ�
        {
            _isConversible = canSpeak;

            if (canSpeak && _convPanel)
            {
                _convPanel.alpha = 1f;
                _convPanel.blocksRaycasts = true;
                _convPanel.interactable = true;
                _isTextEnd = false;
                _dialogueFeeder.TextStart();
            }
            else if (!_dialogueFeeder.IsUpdatingText)
            {
                _convPanel.alpha = 0;
                _convPanel.blocksRaycasts = false;
                _convPanel.interactable = false;
                _isConversible = false;
                _dialogueFeeder.StopFeedText();
            }
        }

        if (!canSpeak || (_isTextEnd ^ !_dialogueFeeder.IsUpdatingText))
        {
            _isTextEnd = !_dialogueFeeder.IsUpdatingText;

            if (!_dialogueFeeder.IsUpdatingText)
            {
                _convPanel.alpha = 0;
                _convPanel.blocksRaycasts = false;
                _convPanel.interactable = false;
                _dialogueFeeder.StopFeedText();
            }
        }

        //_convStartButton.gameObject.SetActive(canSpeak); // ����
        //if (!canSpeak && _isConversible)
        //{
        //    _convPanel.gameObject.SetActive(false); // �݂��Ȃ�����
        //    _isConversible = false;
        //}
    }

#if UNITY_EDITOR_64
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_speakerTransform.position, _conversationRange);
    }
#endif
}