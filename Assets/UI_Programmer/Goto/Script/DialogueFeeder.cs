using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// テキストの文字送りを行う
/// </summary>
public class DialogueFeeder : MonoBehaviour
{
    // おそらくここにシナリオデータが入るため変更される
    [SerializeField, Header("シナリオを格納する")] string[] _scenarios;
    [SerializeField, Header("表示させるTextUI")] Text _uiText;
    [SerializeField, Range(0.001f, 0.3f), Header("1文字の表示にかかる時間")] float _intervalForCharacterDisplay = 0.05f;
    [SerializeField, Range(0.1f, 5f), Header("テキストの切り替えにかかる時間")] float _switchScenarioTime = 1f;

    string _currentText = string.Empty;      // 現在の文字列
    float _timeUntilDisplay = 0;             // 表示にかかる時間
    float _switchScenarioTimer = 0;          // テキストの切り替え用タイマー
    float _timeElapsed = 1;                  // 文字列の表示を開始した時間
    int _currentLine = 0;                    // 現在の行番号
    int _lastUpdateCharacter = -1;           // 表示中の文字数

    /// <summary>文字の表示が完了しているかどうか</summary>
    public bool IsCompleteDisplayText => Time.time > _timeElapsed + _timeUntilDisplay;

    /// <summary>
    /// 文字送りを始める
    /// </summary>
    public void TextStart()
    {
        _currentLine = 0;
        _switchScenarioTimer = 0;
        SetNextLine();
        StartCoroutine(TextUpdate());
    }

    /// <summary>
    /// 文字送りを行う
    /// </summary>
    /// <returns></returns>
    IEnumerator TextUpdate()
    {
        while (_currentText != string.Empty)
        {
            if (IsCompleteDisplayText && _currentText != string.Empty)
            {
                //Debug.Log((int)_switchScenarioTimer);
                // TODO:時間のカウントの仕方がおかしいので直す
                //時間経過で次のテキストに切り替わるようにする
                _switchScenarioTimer += Time.deltaTime;
            }

            // 文字の表示が完了してるかつ切り替え時間に達したなら次の行を表示する
            if (IsCompleteDisplayText && _switchScenarioTimer > _switchScenarioTime && _currentText != string.Empty)
            {
                // 現在の行番号がラストまで行ってない状態でクリックすると、テキストを更新する
                //if (_currentLine < _scenarios.Length && Input.GetMouseButtonDown(0))
                //{
                SetNextLine();
                _switchScenarioTimer = 0;
                //}
            }
            //else
            //{
                // 完了してないなら文字をすべて表示する
                //if (Input.GetMouseButtonDown(0))
                //{
                //    _timeUntilDisplay = 0;
                //}
            //}

            // クリックから経過した時間が想定表示時間の何%か確認し、表示文字数を出す
            int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - _timeElapsed) / _timeUntilDisplay) * _currentText.Length);

            // 表示文字数が前回の表示文字数と異なるならテキストを更新する
            if (displayCharacterCount != _lastUpdateCharacter)
            {
                _uiText.text = _currentText.Substring(0, displayCharacterCount);
                _lastUpdateCharacter = displayCharacterCount;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// テキストを更新する
    /// </summary>
    void SetNextLine()
    {
        // 配列の最後に達していないなら時刻等をキャッシュする
        if (_currentLine < _scenarios.Length)
        {
            _currentText = _scenarios[_currentLine];
            // 想定表示時間と現在の時刻をキャッシュ
            _timeUntilDisplay = _currentText.Length * _intervalForCharacterDisplay;
            _timeElapsed = Time.time;
            _currentLine++;
            // 文字カウントを初期化
            _lastUpdateCharacter = -1;
        }
        else
        {
            // シナリオデータがなくなったらテキストは表示しない
            _currentText = string.Empty;
            // 文字カウントを初期化
            _lastUpdateCharacter = -1;
        }
    }
}