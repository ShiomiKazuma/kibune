using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopLogController : MonoBehaviour
{
    [SerializeField] float _fadeoutTime = 1f; // ���S�ɓ����ɂȂ�܂łɂ����鎞��(�b)
    [SerializeField] float _fadeoutStartTime = 5f; // ���������n�܂�܂łɂ����鎞��(�b)

    //Text _text = null;
    private int _textCount; // �q�I�u�W�F�N�g(Text)�̐�
    private Text[] _logTexts;
    private TextProperty[] _textProperty;

    private void Start()
    {
        _textCount = transform.childCount;
        _logTexts = new Text[_textCount];
        _textProperty = new TextProperty[_textCount];
        for (int i = 0; i < _textCount; i++)
        {
            _logTexts[i] = transform.GetChild(i).GetComponent<Text>();
            _logTexts[i].color = new Color(_logTexts[i].color.r, _logTexts[i].color.g, _logTexts[i].color.b, 0f);
            _textProperty[i].AlfaSet = 0f;
            _textProperty[i].ElapsedTime = 0f;
        }
    }

    void Update()
    {
        // ��ԏ�̃e�L�X�g�͋����I�ɓ������J�n������
        if (_textProperty[0].AlfaSet == 1)
        {
            _textProperty[0].ElapsedTime = _fadeoutStartTime;
        }

        for (int i = _textCount - 1; i >= 0; i--)
        {
            if (_textProperty[i].AlfaSet > 0)
            {
                // �o�ߎ��Ԃ�fadeoutStartTime�����Ȃ玞�Ԃ��J�E���g
                // �����łȂ���Γ����x��������
                if (_textProperty[i].ElapsedTime < _fadeoutStartTime)
                {
                    _textProperty[i].ElapsedTime += Time.deltaTime;
                }
                else
                {
                    _textProperty[i].AlfaSet -= Time.deltaTime / _fadeoutTime;
                    _logTexts[i].color = new Color(_logTexts[i].color.r, _logTexts[i].color.g, _logTexts[i].color.b,
                                       _textProperty[i].AlfaSet);
                }
            }
            else
            {
                break;
            }
        }
    }

    // ���O�o��
    public void OutputLog()
    {
        if (_textProperty[_textCount - 1].AlfaSet > 0)
        {
            UpLogText();
        }
       // _logTexts[_textCount - 1].text = item.ItemName + "����ɓ��ꂽ"; // �����̕������ς���΃��O�̕��͂��ς��܂�
        ResetTextPropety();
    }

    // ���O�����ɂ��炷
    private void UpLogText()
    {
        // �Â��ق����炸�炷
        for (int i = 0; i < _textCount - 1; i++)
        {
            _logTexts[i].text = _logTexts[i + 1].text;
            _textProperty[i].AlfaSet = _textProperty[i + 1].AlfaSet;
            _textProperty[i].ElapsedTime = _textProperty[i + 1].ElapsedTime;
            _logTexts[i].color = new Color(_logTexts[i].color.r, _logTexts[i].color.g, _logTexts[i].color.b,
                               _textProperty[i].AlfaSet);
        }
    }

    // ���O�̏�����
    private void ResetTextPropety()
    {
        _textProperty[_textCount - 1].AlfaSet = 1f;
        _textProperty[_textCount - 1].ElapsedTime = 0f;
        _logTexts[_textCount - 1].color = new Color(_logTexts[_textCount - 1].color.r, _logTexts[_textCount - 1].color.g, _logTexts[_textCount - 1].color.b,
                                       _textProperty[_textCount - 1].AlfaSet);
    }

    struct TextProperty
    {
        private float _alfa;
        public float AlfaSet// �����x�A0�����Ȃ�0�ɂ���
        {
            get
            {
                return _alfa;
            }
            set
            {
                _alfa = value < 0 ? 0 : value;
            }
        }
        public float ElapsedTime { get; set; } // ���O���o�͂���Ă���̌o�ߎ���
    }
}
