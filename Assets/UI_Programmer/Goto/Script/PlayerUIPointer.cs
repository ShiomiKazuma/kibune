using UnityEngine;

public class PlayerUIPointer : MonoBehaviour
{
    /// <summary>�}�E�X�|�C���^�[�𓊉e����Canvas�̎Q��</summary>
    [SerializeField] private Canvas _canvas;
    /// <summary>�}�E�X�|�C���^�[�𓊉e����Canvas��RectTransform�̎Q��</summary>
    [SerializeField] private RectTransform _canvasTransform;
    /// <summary>�}�E�X�|�C���^�[��RectTransform�̎Q��</summary>
    [SerializeField] private RectTransform _cursorTransform;

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
