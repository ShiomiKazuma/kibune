using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    string _currentText = string.Empty;      // ���݂̕�����
    float _timeUntilDisplay = 0;             // �\���ɂ����鎞��
    float _switchScenarioTimer = 0;          // �e�L�X�g�̐؂�ւ��p�^�C�}�[
    float _timeElapsed = 1;                  // ������̕\�����J�n��������
    int _currentLine = 0;                    // ���݂̍s�ԍ�
    int _lastUpdateCharacter = -1;           // �\�����̕�����

    /// <summary>�����̕\�����������Ă��邩�ǂ���</summary>
    public bool IsCompleteDisplayText => Time.time > _timeElapsed + _timeUntilDisplay;

    /// <summary>
    /// ����������n�߂�
    /// </summary>
    public void TextStart()
    {
        _currentLine = 0;
        _switchScenarioTimer = 0;
        SetNextLine();
        StartCoroutine(TextUpdate());
    }

    /// <summary>
    /// ����������s��
    /// </summary>
    /// <returns></returns>
    IEnumerator TextUpdate()
    {
        while (_currentText != string.Empty)
        {
            if (IsCompleteDisplayText && _currentText != string.Empty)
            {
                //Debug.Log((int)_switchScenarioTimer);
                // TODO:���Ԃ̃J�E���g�̎d�������������̂Œ���
                //���Ԍo�߂Ŏ��̃e�L�X�g�ɐ؂�ւ��悤�ɂ���
                _switchScenarioTimer += Time.deltaTime;
            }

            // �����̕\�����������Ă邩�؂�ւ����ԂɒB�����Ȃ玟�̍s��\������
            if (IsCompleteDisplayText && _switchScenarioTimer > _switchScenarioTime && _currentText != string.Empty)
            {
                // ���݂̍s�ԍ������X�g�܂ōs���ĂȂ���ԂŃN���b�N����ƁA�e�L�X�g���X�V����
                //if (_currentLine < _scenarios.Length && Input.GetMouseButtonDown(0))
                //{
                SetNextLine();
                _switchScenarioTimer = 0;
                //}
            }
            //else
            //{
                // �������ĂȂ��Ȃ當�������ׂĕ\������
                //if (Input.GetMouseButtonDown(0))
                //{
                //    _timeUntilDisplay = 0;
                //}
            //}

            // �N���b�N����o�߂������Ԃ��z��\�����Ԃ̉�%���m�F���A�\�����������o��
            int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - _timeElapsed) / _timeUntilDisplay) * _currentText.Length);

            // �\�����������O��̕\���������ƈقȂ�Ȃ�e�L�X�g���X�V����
            if (displayCharacterCount != _lastUpdateCharacter)
            {
                _uiText.text = _currentText.Substring(0, displayCharacterCount);
                _lastUpdateCharacter = displayCharacterCount;
            }

            yield return new WaitForEndOfFrame();
        }
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
            _currentText = string.Empty;
            // �����J�E���g��������
            _lastUpdateCharacter = -1;
        }
    }
}