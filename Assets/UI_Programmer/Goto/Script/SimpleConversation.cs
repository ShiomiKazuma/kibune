using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// 作成 菅沼
// 引継ぎ 五島
public class SimpleConversation : MonoBehaviour
{
    [SerializeField, Header("読ませるセリフ")]
    List<string> _texts = new List<string>();

    [SerializeField, Header("プレイヤーのレイヤー")] 
    LayerMask _playerLayer; // プレイヤーのレイヤー

    [SerializeField, Header("会話可能距離"), Range(1f, 10f)] 
    float _conversationRange; // 会話可能距離

    [SerializeField, Header("文字送り範囲に入った際のイベント")]
    UnityEvent _evtOnEntry;

    Text _convText; // 会話のテキスト表示
    CanvasGroup _convPanel; // テキストパネルのCanvasGroup
    DialogueFeeder _dFeeder; // 使用するDialogueFeeder

    bool _isConversible = false; // 会話スタートしたフラグ

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
        bool canSpeak = Physics.CheckSphere(transform.position, _conversationRange, _playerLayer); // 会話可能圏内にいるなら

        // 会話可能範囲の出入りを判定
        if (!_isConversible && canSpeak)
        {
            _isConversible = true;

            if (canSpeak && _convPanel) // 会話開始したら会話ボックスを開く
            {
                _convPanel.alpha = 1f;
                _convPanel.blocksRaycasts = true;
                _convPanel.interactable = true;

                _dFeeder.OverrideScenarios(_texts);
                _dFeeder.TextStart();
                _evtOnEntry.Invoke();
            }
        }

        // 会話終了または会話可能範囲の外に出たら会話ボックスを閉じる
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