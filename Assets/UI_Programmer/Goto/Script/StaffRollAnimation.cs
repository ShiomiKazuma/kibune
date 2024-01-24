using DG.Tweening;
using UnityEngine;

public class StaffRollAnimation : MonoBehaviour
{
    [SerializeField, Header("�X�^�b�t���[���p�l����CanvasGroup")] CanvasGroup _staffRollCanvasGroup;
    [SerializeField, Header("�X�^�b�t���[���p�l���̍��W")] RectTransform _staffRollPanelRectTransform;
    [SerializeField, Header("�I�����̃X�^�b�t���[���p�l���̍��W��Y���W")] float _endPositionY = 0;
    [SerializeField, Header("�A�j���[�V�����̍Đ�����")] float _animationTime = 1f;

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
        _staffRollPanelRectTransform.DOAnchorPos(new Vector2(0, _endPositionY), _animationTime).SetLink(this.gameObject);
    }
}