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
    [SerializeField] DialogueFeeder _dialogueFeeder; // �g�p����DialogueFeeder

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

        // ��b�\�͈͂̏o����𔻒�
        if (_isConversible ^ canSpeak)
        {
            _isConversible = canSpeak;

            if (canSpeak && _convPanel) // ��b�J�n�������b�{�b�N�X���J��
            {
                _convPanel.alpha = 1f;
                _convPanel.blocksRaycasts = true;
                _convPanel.interactable = true;
                _dialogueFeeder.TextStart();
            }
            //else if (!_dialogueFeeder.IsUpdatingText)�@// ��b�I���������b�{�b�N�X�����
            //{
            //    _convPanel.alpha = 0;
            //    _convPanel.blocksRaycasts = false;
            //    _convPanel.interactable = false;
            //    _isConversible = false;
            //    _dialogueFeeder.StopFeedText();
            //}
        }

        // ��b�I���܂��͉�b�\�͈͂̊O�ɏo�����b�{�b�N�X�����
        if (!canSpeak|| !_dialogueFeeder.IsUpdatingText)
        {
            _convPanel.alpha = 0;
            _convPanel.blocksRaycasts = false;
            _convPanel.interactable = false;
            _dialogueFeeder.StopFeedText();
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