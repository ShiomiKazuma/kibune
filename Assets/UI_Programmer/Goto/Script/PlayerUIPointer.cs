using UnityEngine;

public class PlayerUIPointer : MonoBehaviour
{
    /// <summary>UIPointer�𓊉e����Canvas�̎Q��</summary>
    [SerializeField, Header("UIPointer�𓊉e����Canvas")] private Canvas _canvas;
    /// <summary>UIPointer�𓊉e����Canvas��RectTransform�̎Q��</summary>
    [SerializeField, Header("UIPointer�𓊉e����Inventory��RectTransform")] private RectTransform _canvasTransform;
    /// <summary>UIPointer��RectTransform�̎Q��</summary>
    [SerializeField, Header("UIPointer��RectTransform")] private RectTransform _cursorTransform;

    void Update()
    {
        // Canvas��RectTransform���ɂ���}�E�X�̍��W�����[�J�����W�ɕϊ�����
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasTransform,
            Input.mousePosition,
            _canvas.worldCamera,
            out var mousePosition);

        // �|�C���^�[���}�E�X�̍��W�Ɉړ�����
        _cursorTransform.anchoredPosition = new Vector2(mousePosition.x, mousePosition.y);
    }
}
