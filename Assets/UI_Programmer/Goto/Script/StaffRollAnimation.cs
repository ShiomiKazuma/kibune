using DG.Tweening;
using SLib.Systems;
using UnityEngine;

public class StaffRollAnimation : MonoBehaviour
{
    [SerializeField, Header("スタッフロールパネルのCanvasGroup")] CanvasGroup _staffRollCanvasGroup;
    [SerializeField, Header("スタッフロールパネルの座標")] RectTransform _staffRollPanelRectTransform;
    [SerializeField, Header("終了時のスタッフロールパネルの座標のY座標")] float _endPositionY = 0;
    [SerializeField, Header("アニメーションの再生時間")] float _animationTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _staffRollCanvasGroup.alpha = 0;
        _staffRollCanvasGroup.interactable = false;
        _staffRollCanvasGroup.blocksRaycasts = false;
    }

    public void StartStaffRollAnimation()
    {
        _staffRollCanvasGroup.alpha = 1;
        _staffRollCanvasGroup.interactable = true;
        _staffRollCanvasGroup.blocksRaycasts = true;
        _staffRollPanelRectTransform.DOAnchorPos(new Vector2(0, _endPositionY), _animationTime).SetLink(this.gameObject).OnComplete(() => 
        {
            SceneLoader loader = GameObject.FindObjectOfType<SceneLoader>();
            loader.LoadSceneByName(GameObject.FindAnyObjectByType<GameInfo>().TitleSceneName);
            var hud = GameObject.FindFirstObjectByType<HUDManager>();
            hud.ToFront(0);
        });
    }
}
