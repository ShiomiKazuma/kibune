using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] DialogueFeeder _dialogueFeeder;

    [SerializeField] LayerMask _playerLayer; // プレイヤーレイヤ

    [SerializeField, Range(1f, 5f)] float _conversationRange; // 会話可能距離

    bool _isConversible = false; // 会話スタートしたフラグ
    bool _isTextEnd = false;

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

        if (_isConversible ^ canSpeak)   // ハットと読む。排他的論理和 ORではたくさん呼ばれてしまうため良くない
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