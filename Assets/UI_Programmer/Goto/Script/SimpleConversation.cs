using UnityEngine;
using UnityEngine.UI;

// 作成 菅沼
// 加工 五島
public class SimpleConversation : MonoBehaviour
{
    [SerializeField] Text _convText; // 会話のテキスト表示
    [SerializeField] CanvasGroup _convPanel; // テキストのBGパネル
    //[SerializeField] Button _convStartButton; // ”会話をする”ボタン
    //[SerializeField] Button _convNextButton; // ”次へ”ボタン
    [SerializeField] Transform _speakerTransform; // 会話者
    [SerializeField] DialogueFeeder _dialogueFeeder; // 使用するDialogueFeeder

    [SerializeField] LayerMask _playerLayer; // プレイヤーレイヤ

    [SerializeField, Range(1f, 5f)] float _conversationRange; // 会話可能距離

    bool _isConversible = false; // 会話スタートしたフラグ

    public void OnStartConversation()
    {
        _convPanel.gameObject.SetActive(true);
        _isConversible = true;
    }

    private void Awake()
    {
        //_convStartButton.gameObject.SetActive(false); // みえなくする
        //_convPanel.gameObject.SetActive(false); // みえなくする
        _convPanel.alpha = 0;
        _convPanel.blocksRaycasts = false;
        _convPanel.interactable = false;
        _isConversible = false;
    }

    private void FixedUpdate()
    {
        bool canSpeak = Physics.CheckSphere(_speakerTransform.position, _conversationRange, _playerLayer); // 会話可能圏内にいるなら
        //_convPanel.gameObject.SetActive(canSpeak);

        // 会話可能範囲の出入りを判定
        if (_isConversible ^ canSpeak)
        {
            _isConversible = canSpeak;

            if (canSpeak && _convPanel) // 会話開始したら会話ボックスを開く
            {
                _convPanel.alpha = 1f;
                _convPanel.blocksRaycasts = true;
                _convPanel.interactable = true;
                _dialogueFeeder.TextStart();
            }
            //else if (!_dialogueFeeder.IsUpdatingText)　// 会話終了したら会話ボックスを閉じる
            //{
            //    _convPanel.alpha = 0;
            //    _convPanel.blocksRaycasts = false;
            //    _convPanel.interactable = false;
            //    _isConversible = false;
            //    _dialogueFeeder.StopFeedText();
            //}
        }

        // 会話終了または会話可能範囲の外に出たら会話ボックスを閉じる
        if (!canSpeak|| !_dialogueFeeder.IsUpdatingText)
        {
            _convPanel.alpha = 0;
            _convPanel.blocksRaycasts = false;
            _convPanel.interactable = false;
            _dialogueFeeder.StopFeedText();
        }

        //_convStartButton.gameObject.SetActive(canSpeak); // 可視化
        //if (!canSpeak && _isConversible)
        //{
        //    _convPanel.gameObject.SetActive(false); // みえなくする
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