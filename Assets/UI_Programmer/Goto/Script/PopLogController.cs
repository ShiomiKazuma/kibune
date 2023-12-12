using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopLogController : MonoBehaviour
{
    [SerializeField] float _fadeoutTime = 1f; // 完全に透明になるまでにかかる時間(秒)
    [SerializeField] float _fadeoutStartTime = 5f; // 透明化が始まるまでにかかる時間(秒)

    //Text _text = null;
    private int _textCount; // 子オブジェクト(Text)の数
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
        // 一番上のテキストは強制的に透明化開始させる
        if (_textProperty[0].AlfaSet == 1)
        {
            _textProperty[0].ElapsedTime = _fadeoutStartTime;
        }

        for (int i = _textCount - 1; i >= 0; i--)
        {
            if (_textProperty[i].AlfaSet > 0)
            {
                // 経過時間がfadeoutStartTime未満なら時間をカウント
                // そうでなければ透明度を下げる
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

    // ログ出力
    public void OutputLog()
    {
        if (_textProperty[_textCount - 1].AlfaSet > 0)
        {
            UpLogText();
        }
       // _logTexts[_textCount - 1].text = item.ItemName + "を手に入れた"; // ここの文字列を変えればログの文章が変わります
        ResetTextPropety();
    }

    // ログを一つ上にずらす
    private void UpLogText()
    {
        // 古いほうからずらす
        for (int i = 0; i < _textCount - 1; i++)
        {
            _logTexts[i].text = _logTexts[i + 1].text;
            _textProperty[i].AlfaSet = _textProperty[i + 1].AlfaSet;
            _textProperty[i].ElapsedTime = _textProperty[i + 1].ElapsedTime;
            _logTexts[i].color = new Color(_logTexts[i].color.r, _logTexts[i].color.g, _logTexts[i].color.b,
                               _textProperty[i].AlfaSet);
        }
    }

    // ログの初期化
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
        public float AlfaSet// 透明度、0未満なら0にする
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
        public float ElapsedTime { get; set; } // ログが出力されてからの経過時間
    }
}
