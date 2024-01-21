using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// �쐬�@�ܓ�
/// <summary>
/// �e�L�X�g�̕���������s��
/// </summary>
public class DialogueFeeder : MonoBehaviour
{
    // �����炭�����ɃV�i���I�f�[�^�����邽�ߕύX�����
    [SerializeField, Header("�V�i���I���i�[����")] string[] _scenarios;
    [SerializeField, Header("�\��������TextUI")] Text _uiText;
    [SerializeField, Range(0.001f, 0.3f), Header("1�����̕\���ɂ����鎞��")] float _intervalForCharacterDisplay = 0.05f;
    [SerializeField, Range(0.1f, 5f), Header("�e�L�X�g�̐؂�ւ��ɂ����鎞��")] float _switchScenarioTime = 1f;
    [SerializeField, Header("���荞�݂̉�")] bool _canInterrupt = false;

    string _currentText = string.Empty;      // ���݂̕�����
    float _timeUntilDisplay = 0;             // �\���ɂ����鎞��
    float _switchScenarioTimer = 0;          // �e�L�X�g�̐؂�ւ��p�^�C�}�[
    float _timeElapsed = 1;                  // ������̕\�����J�n��������
    float _pauseTime = 0;                    // �ꎞ��~���̃^�C�}�[
    int _currentLine = 0;                    // ���݂̍s�ԍ�
    int _lastUpdateCharacter = -1;           // �\�����̕�����
    bool _isUpdatingText = false;            // �e�L�X�g�X�V�����ǂ���
    bool _isPause = false;                   // �ꎞ��~�����ǂ���
    Coroutine _coroutine = null;             // �N������TextUpdate���i�[����

    /// <summary>�����̍X�V�����ǂ���</summary>
    public bool IsUpdatingText => _isUpdatingText;

    /// <summary>�����̕\�����������Ă��邩�ǂ���</summary>
    public bool IsCompleteDisplayText => Time.time > _timeElapsed + _timeUntilDisplay;

    /// <summary>1�����̕\���ɂ����鎞�Ԃ�ݒ肵�܂�</summary>
    public void SetIntervalForCharacterDisplay(float time) => _intervalForCharacterDisplay = time;

    /// <summary>�e�L�X�g�̐؂�ւ��ɂ����鎞�Ԃ�ݒ肵�܂�</summary>
    public void SetSwitchScenarioTime(float time) => _switchScenarioTime = time;

    /// <summary>
    /// ����������n�߂�
    /// </summary>
    public void TextStart()
    {
        if (_canInterrupt && _coroutine != null)
        {
            PauseFeedText();
        }

        if (!_isUpdatingText)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            // �q�I�u�W�F�N�g�̍Ō���ɔz�u���邱�Ƃŕ��������Ă���ԏ�Ɍ����܂��B
            this.gameObject.transform.SetAsLastSibling();
            _currentLine = 0;
            _switchScenarioTimer = 0;
            _pauseTime = 0;
            _isPause = false;
            SetNextLine();
            _coroutine = StartCoroutine(TextUpdate());
        }
    }

    /// <summary>
    /// ����������s��
    /// </summary>
    /// <returns></returns>
    IEnumerator TextUpdate()
    {
        _isUpdatingText = true;

        while (_currentText != string.Empty)
        {
            while (_isPause == true)
            {
                _pauseTime += Time.deltaTime;
                yield return null;
            }

            if (IsCompleteDisplayText && _currentText != string.Empty)
            {
                //Debug.Log((int)_switchScenarioTimer);
                //���Ԍo�߂Ŏ��̃e�L�X�g�ɐ؂�ւ��悤�ɂ���
                _switchScenarioTimer += Time.deltaTime;
            }

            // �����̕\�����������Ă邩�؂�ւ����ԂɒB�����Ȃ玟�̍s��\������
            if (IsCompleteDisplayText && _switchScenarioTimer > _switchScenarioTime && _currentText != string.Empty)
            {
                SetNextLine();
                _switchScenarioTimer = 0;
                _pauseTime = 0;
            }

            // �N���b�N����o�߂������Ԃ��z��\�����Ԃ̉�%���m�F���A�\�����������o��
            int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - _timeElapsed - _pauseTime) / _timeUntilDisplay) * _currentText.Length);

            // �\�����������O��̕\���������ƈقȂ�Ȃ�e�L�X�g���X�V����
            if (displayCharacterCount != _lastUpdateCharacter)
            {
                _uiText.text = _currentText.Substring(0, displayCharacterCount);
                _lastUpdateCharacter = displayCharacterCount;
            }

            yield return new WaitForEndOfFrame();
        }

        StopFeedText();
    }

    /// <summary>
    /// �e�L�X�g���X�V����
    /// </summary>
    void SetNextLine()
    {
        // �z��̍Ō�ɒB���Ă��Ȃ��Ȃ玞�������L���b�V������
        if (_currentLine < _scenarios.Length)
        {
            _currentText = _scenarios[_currentLine];
            // �z��\�����Ԃƌ��݂̎������L���b�V��
            _timeUntilDisplay = _currentText.Length * _intervalForCharacterDisplay;
            _timeElapsed = Time.time;
            _currentLine++;
            // �����J�E���g��������
            _lastUpdateCharacter = -1;
        }
        else
        {
            // �V�i���I�f�[�^���Ȃ��Ȃ�����e�L�X�g�͕\�����Ȃ�
            //_currentText = string.Empty;
            // �����J�E���g��������
            _lastUpdateCharacter = -1;
        }
    }

    /// <summary>
    /// ����������ꎞ��~���܂��B
    /// </summary>
    public void PauseFeedText()
    {
        if (_coroutine != null)
        {
            _isUpdatingText = false;
            _isPause = true;
        }
    }

    /// <summary>
    /// ����������ĊJ���܂��B
    /// </summary>
    public void RestartFeedText()
    {
        if (_coroutine != null)
        {
            _isUpdatingText = true;
            _isPause = false;
        }
    }

    /// <summary>
    /// ����������~���܂��B
    /// </summary>
    public void StopFeedText()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _isUpdatingText = false;
    }
}