using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// �쐬 ����
// ���p�� �ܓ�
public class SimpleConversation : MonoBehaviour
{
    [SerializeField, Header("�ǂ܂���Z���t")]
    List<string> _texts = new List<string>();

    [SerializeField, Header("�v���C���[�̃��C���[")] 
    LayerMask _playerLayer; // �v���C���[�̃��C���[

    [SerializeField, Header("��b�\����"), Range(1f, 10f)] 
    float _conversationRange; // ��b�\����

    [SerializeField, Header("��������͈͂ɓ������ۂ̃C�x���g")]
    UnityEvent _evtOnEntry;

    Text _convText; // ��b�̃e�L�X�g�\��
    CanvasGroup _convPanel; // �e�L�X�g�p�l����CanvasGroup
    DialogueFeeder _dFeeder; // �g�p����DialogueFeeder

    bool _isConversible = false; // ��b�X�^�[�g�����t���O

    public void OnStartConversation()
    {
        _convPanel.gameObject.SetActive(true);
        _isConversible = true;
    }

    private void Start()
    {
        _dFeeder = GameObject.FindObjectOfType<DialogueFeeder>();
        _convPanel = _dFeeder.gameObject.GetComponent<CanvasGroup>();
        _convText = _dFeeder.gameObject.transform.GetChild(0).GetComponent<Text>();

        _convPanel.alpha = 0;
        _convPanel.blocksRaycasts = false;
        _convPanel.interactable = false;
        _isConversible = false;
    }

    private void FixedUpdate()
    {
        bool canSpeak = Physics.CheckSphere(transform.position, _conversationRange, _playerLayer); // ��b�\�����ɂ���Ȃ�

        // ��b�\�͈͂̏o����𔻒�
        if (!_isConversible && canSpeak)
        {
            _isConversible = true;

            if (canSpeak && _convPanel) // ��b�J�n�������b�{�b�N�X���J��
            {
                _convPanel.alpha = 1f;
                _convPanel.blocksRaycasts = true;
                _convPanel.interactable = true;

                _dFeeder.OverrideScenarios(_texts);
                _dFeeder.TextStart();
                _evtOnEntry.Invoke();
            }
        }

        // ��b�I���܂��͉�b�\�͈͂̊O�ɏo�����b�{�b�N�X�����
        if ((!canSpeak || !_dFeeder.IsUpdatingText) && _isConversible)
        {
            _convPanel.alpha = 0;
            _convPanel.blocksRaycasts = false;
            _convPanel.interactable = false;

            _isConversible = false;

            _dFeeder.OverrideScenarios(null);
            _dFeeder.StopFeedText();
        }
    }

#if UNITY_EDITOR_64
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _conversationRange);
    }
#endif
}